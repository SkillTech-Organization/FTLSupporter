using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.IO.Compression;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using PMap.Common.Parse;
using System.Web.Script.Serialization;

namespace PMap.Common
{
    public static class Util
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
            string path,
            [MarshalAs(UnmanagedType.LPTStr)]
            StringBuilder shortPath,
            int shortPathLength
        );

        /// <summary>
        /// Wrapperfarmer a fenti fuggvenyhez
        /// </summary>
        /// <param name="p_longpath"></param>
        /// <returns></returns>
        public static string GetShortPathName(string p_longpath)
        {
            StringBuilder shortPath = new StringBuilder(255);
            Util.GetShortPathName(p_longpath, shortPath, shortPath.Capacity);
            return shortPath.ToString();
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetLongPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
            string path,
            [MarshalAs(UnmanagedType.LPTStr)]
            StringBuilder longPath,
            int longPathLength
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_tosplit"></param>
        /// <param name="p_splitby"></param>
        /// <returns></returns>
        public static string[] SplitByString(string p_tosplit, string p_splitby)
        {
            int offset = 0;
            int index = 0;
            int[] offsets = new int[p_tosplit.Length + 1];

            while (index < p_tosplit.Length)
            {
                int indexOf = p_tosplit.IndexOf(p_splitby, index);
                if (indexOf != -1)
                {
                    offsets[offset++] = indexOf;
                    index = (indexOf + p_splitby.Length);
                }
                else
                {
                    index = p_tosplit.Length;
                }
            }

            string[] final = new string[offset + 1];
            if (offset == 0)
            {
                final[0] = p_tosplit;
            }
            else
            {
                offset--;
                final[0] = p_tosplit.Substring(0, offsets[0]);
                for (int i = 0; i < offset; i++)
                {
                    final[i + 1] = p_tosplit.Substring(offsets[i] + p_splitby.Length, offsets[i + 1] - offsets[i] - p_splitby.Length);
                }
                final[offset + 1] = p_tosplit.Substring(offsets[offset] + p_splitby.Length);
            }
            return final;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_s"></param>
        /// <param name="p_file"></param>
        /// <returns></returns>
        public static string String2File(string p_s, string p_file)
        {
            return String2File(p_s, p_file, false);
        }

        /// <summary>
        /// Kiir egy stringet egy fajlba
        /// </summary>
        /// <param name="p_s">string</param>
        /// <param name="p_file">filename, ha ures tempfajlt csinal</param>
        /// <param name="p_append">hozzafuzze-e</param>
        /// <returns>fajlnevet visszaadja (tempfile miatt)</returns>
        public static string String2File(string p_s, string p_file, bool p_append)
        {
            using (GlobalLocker lockObj = new GlobalLocker(Global.lockObject))
            {
                if (p_file == "" || p_file == null)
                    p_file = Path.GetTempFileName();

                string path = Path.GetDirectoryName(p_file);
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                TextWriter tw = new StreamWriter(p_file, p_append);
                tw.Write(p_s);
                tw.Close();
                return p_file;
            }
        }

        public static void Log2File(string p_msg, bool p_sendToParse = true)
        {
            Log2File(p_msg, Global.LogFileName, p_sendToParse);
        }

        public static void Log2File(string p_msg, string p_logFileName, bool p_sendToCloud = true)
        {
            string dir = PMapIniParams.Instance.LogDir;
            if (dir == null || dir == "")
                dir = Path.GetDirectoryName(Application.ExecutablePath);

            string LogFileName = Path.Combine(dir, p_logFileName);
            string sMsg = String.Format("{0}: {1}", DateTime.Now.ToString(Global.DATETIMEFORMAT), p_msg);
            Console.WriteLine(sMsg);
            String2File(sMsg + Environment.NewLine, LogFileName, true);

            if (p_sendToCloud && PMapIniParams.Instance.ParseLog)
                ParseSynchLog.LogToParse(p_logFileName.Substring(p_logFileName.Length - 3, 3),  DateTime.Now, p_msg);

        }

        public static void ExceptionLog(Exception p_ecx)
        {
            string dir = PMapIniParams.Instance.LogDir;
            if (dir == null || dir == "")
                dir = Path.GetDirectoryName(Application.ExecutablePath);

            string ExcFileName = Path.Combine(dir, Global.ExcFileName);

            string sMsg = String.Format("{0}: {1} "+Environment.NewLine +"{2}", 
                DateTime.Now.ToString(Global.DATETIMEFORMAT), 
                GetExceptionText( p_ecx), p_ecx.StackTrace);

            Util.String2File(sMsg + Environment.NewLine, ExcFileName, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string FileToString(string p_file)
        {
            string s = "";
            TextReader tr = new StreamReader(p_file);
            s = tr.ReadToEnd();
            tr.Close();
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string filename)
        {
            FileStream fs = File.OpenRead(filename);
            BinaryReader br = new BinaryReader(fs);

            byte[] b = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();
            return b;
        }

        /// <summary>
        /// Exception formázott szövege
        /// </summary>
        /// <param name="p_ecx"></param>
        /// <returns></returns>
        public static string GetExceptionText(Exception p_ecx)
        {
            string innerMsg = "";
            if (p_ecx.InnerException != null)
                innerMsg = p_ecx.InnerException.Message;

            return String.Format("{0} {1}", p_ecx.Message, innerMsg).Trim();
        }



        /// <summary>
        /// YYYYMM formatu stringet DateTime-ra konvertal (YYYY.MM.01 nap)
        /// </summary>
        /// <param name="p_yearmonth">evhonap</param>
        /// <returns>DateTime ertek</returns>
        public static DateTime YearMonth2DateTime(string p_yearmonth)
        {
            return new DateTime(Int32.Parse(p_yearmonth.Substring(0, 4)), Int32.Parse(p_yearmonth.Substring(4, 2)), 1);
        }

        /// <summary>
        /// DateTime-bol YYYYMM string
        /// </summary>
        /// <param name="p_date">Datum</param>
        /// <returns>Evho</returns>
        public static string DateTime2YearMonth(DateTime p_date)
        {
            return p_date.Year.ToString() + p_date.Month.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Ev, honap -> YYYYMM
        /// </summary>
        /// <param name="p_year">Ev</param>
        /// <param name="p_month">ho</param>
        /// <returns>Evho</returns>
        public static string Yearmonth2YearMonth(int p_year, int p_month)
        {
            return p_year.ToString() + p_month.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Ketteoszt egy tombot: a,1,b,2,c,3 => a,b,c/1,2,3 
        /// </summary>
        /// <param name="input">Bemeneti tomb</param>
        /// <param name="output1">Kimeneti tomb1</param>
        /// <param name="output2">Kimeneti tomb2</param>
        public static void SplitArray(object[] p_input, out object[] _output1, out object[] _output2)
        {
            int l = p_input.Length;
            int l2 = l / 2;
            _output1 = new object[l - l2];
            _output2 = new object[l2];

            for (int i = 0; i < l; i++)
            {
                if (i % 2 == 0)
                    _output1[i / 2] = p_input[i];
                else
                    _output2[i / 2] = p_input[i];
            }

        }

        /// <summary>
        /// String -> Boolean
        /// </summary>
        /// <param name="i">0/1</param>
        /// <returns>false/true</returns>
        public static bool String2Bool(string p_i)
        {
            return (p_i != "0");
        }

        /// <summary>
        /// Grid filterezese
        /// </summary>
        /// <param name="t">Adatforras</param>
        /// <param name="rowindex">sor</param>
        /// <param name="filterstring">szurostring</param>
        /// <param name="filterdate_from">kezdodatum</param>
        /// <param name="filterdate_to">vegdatum</param>
        /// <param name="datefield">datummezo neve</param>
        /// <param name="stringfields">stringmezok</param>
        /// <returns>lathato-e az adott sor</returns>
        public static bool Gridfilter(DataTable p_t, int p_rowindex, string p_filterstring, DateTime p_filterdate_from, DateTime p_filterdate_to, string p_datefield, params string[] p_stringfields)
        {
            bool temp = false;

            if (p_filterstring != "")
                for (int i = 0; i < p_stringfields.Length; i++)
                {
                    if (p_t.Rows[p_rowindex][p_stringfields[i]].ToString().ToUpper().Contains(p_filterstring.ToUpper()))
                        temp = true;
                }
            else
                temp = true;

            if (p_datefield != "")
            {
                if (((DateTime)(p_t.Rows[p_rowindex][p_datefield])).CompareTo(p_filterdate_from) < 0) temp = false;
                if (((DateTime)(p_t.Rows[p_rowindex][p_datefield])).CompareTo(p_filterdate_to) > 0) temp = false;
            }

            return temp;
        }

        public static string SaveGridLayoutToString(GridView p_gw)
        {

            MemoryStream ms = new MemoryStream();
            p_gw.SaveLayoutToStream(ms);
            string retVal = Encoding.UTF8.GetString(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            ms.Dispose();
            return retVal;
        }

        public static void RestoreGridLayoutFromString(GridView p_gw, string p_XMLLayout)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(p_XMLLayout);
            MemoryStream ms = new MemoryStream(byteArray);
            p_gw.RestoreLayoutFromStream(ms);
            ms.Close();
        }

        /// <summary>
        /// Decimal-e egy string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDecimal(string p_s)
        {
            decimal temp;
            return Decimal.TryParse(p_s, out temp);
        }

        /// <summary>
        /// Integer-e
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInteger(string p_s)
        {
            int temp;
            return Int32.TryParse(p_s, out temp);
        }

        /// <summary>
        /// Visszaadja a telepitesi helyet
        /// </summary>
        /// <returns>telepitesi hely</returns>
        public static string GetBasePath()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        /// <summary>
        /// Van-e az ősök kozott ilyen
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool HasAncestor(Type p_t, Type p_ancestor)
        {
            if (p_t.BaseType == p_ancestor) return true;
            if (p_t.BaseType == null) return false;
            return HasAncestor(p_t.BaseType, p_ancestor);
        }


        /// <summary>
        /// Evhonapok intervallumanak listaja (pl. 200901-200903 = 200901,200902,200903)
        /// </summary>
        /// <param name="p_evho1"></param>
        /// <param name="p_evho2"></param>
        /// <returns></returns>
        public static List<string> IterateYearMonthInterval(string p_evho1, string p_evho2)
        {
            List<string> evhoz = new List<string>();

            int ev = Int32.Parse(p_evho1.Substring(0, 4));
            int ho = Int32.Parse(p_evho1.Substring(4, 2));
            int celev = Int32.Parse(p_evho2.Substring(0, 4));
            int celho = Int32.Parse(p_evho2.Substring(4, 2));

            while (true)
            {
                evhoz.Add(ev.ToString() + ho.ToString().PadLeft(2, '0'));
                ho++;
                if (ho == 13)
                {
                    ev++;
                    ho = 1;
                }

                if (ev == celev && ho > celho)
                    break;
            }

            return evhoz;
        }


        public static int CompareVer(string v1, string v2)
        {

            string[] ver1 = v1.Split('.');
            string[] ver2 = v2.Split('.');

            string Version1 = ver1[0].PadLeft(5, '0') + "." + ver1[1].PadLeft(5, '0') + "." + ver1[2].PadLeft(5, '0');
            string Version2 = ver2[0].PadLeft(5, '0') + "." + ver2[1].PadLeft(5, '0') + "." + ver2[2].PadLeft(5, '0');

            Version1 = Version1.ToUpper();
            Version2 = Version2.ToUpper();

            return (Version1.CompareTo(Version2));
        }



        public static Control FindControl(Control container, string name)
        {
            if (container.Name == name)
                return container;
            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = FindControl(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }

        public static List<Control> FindControlsByType(Control p_ctrl, Type p_type)
        {
            List<Control> res = new List<Control>();

            foreach (Control subCtrl in p_ctrl.Controls)
            {
                res.AddRange(FindControlsByType(subCtrl, p_type));
            }
            if (p_ctrl.GetType() == p_type)
                res.Add(p_ctrl);
            return res;
        }

        public static Control FindParentByType(Control ctrl, Type p_type)
        {
            if (ctrl.GetType() == p_type)
                return ctrl;

            if (ctrl.Parent != null)
                return FindParentByType(ctrl.Parent, p_type);
            else
                return null;
        }

        public static string DOS2WinText(string p_txt)
        {

            //╡RV╓ZTδRè TÜKÖRFΘRαGÉP
            p_txt = p_txt.Replace("╡", "Á");
            p_txt = p_txt.Replace("╓", "Í");
            p_txt = p_txt.Replace("δ", "Ű");
            p_txt = p_txt.Replace("è", "Ő");
            p_txt = p_txt.Replace("Θ", "Ú");
            p_txt = p_txt.Replace("α", "Ó");

            //árvízt√rï tükürfúrógép (852)
            p_txt = p_txt.Replace("√", "ű");
            p_txt = p_txt.Replace("ï", "ő");

            return p_txt;
        }

        public static string StrZero(int p_value, int p_len)
        {
            string sWrk = new string('0', p_len);
            sWrk += p_value.ToString();
            return sWrk.Substring(sWrk.Length - p_len, p_len);
        }

        public static string GetSysInfo()
        {
            try
            {
                /*
                First you have to create the 2 performance counters
                using the System.Diagnostics.PerformanceCounter class.
                */

                // PerformanceCounter cpuCounter;
                PerformanceCounter ramCounter;

                //                PerformanceCounterCategory[] categories;

                /*
                cpuCounter = new PerformanceCounter();

                cpuCounter.CategoryName = "Process";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";

                string sProcessName = Process.GetCurrentProcess().ProcessName;

                cpuCounter.InstanceName = sProcessName;
                */

                /*
                ramCounter = new PerformanceCounter();
                ramCounter.CategoryName = ".NET CLR Memory";
                ramCounter.CounterName = "# Total reserved Bytes";
                ramCounter.InstanceName = "_Global_";

                //           return "CPU:" + cpuCounter.NextValue() + "% RAM:" + (ramCounter.NextValue() / (1024 * 1024)) + " Mb";
                return (ramCounter.NextValue() / (1024 * 1024)) + " Mb";
                 */


                return "";
            }
            catch (Exception e)
            {

                Util.ExceptionLog(e);
                return "Exception has been thrown (see PMap.exc). Unable to read performance categories.";
                //throw e;
            }
        }

        public static RegistrySecurity CreateRegistrySecurity()
        {
            string sUser = Environment.UserDomainName + "\\" + Environment.UserName;
            RegistrySecurity oRegistrySecurity = new RegistrySecurity();
            RegistryAccessRule oRule = new RegistryAccessRule(sUser, RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow);
            oRegistrySecurity.AddAccessRule(oRule);
            return oRegistrySecurity;
        }

        public static Color GetRandomColor()
        {
            Random rnd = new Random((int)DateTime.Now.Millisecond);
            return Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }


        public static double DistanceBetweenLineAndPoint(double Xa, double Ya, double Xb, double Yb, double Xp, double Yp)
        {
            // Psuedocode for returning the absolute distance to a line segment from a point.
            //Xa,Ya is point 1 on the line segment.
            //Xb,Yb is point 2 on the line segment.
            //Xp,Yp is the point.

            double xu = Xp - Xa;
            double yu = Yp - Ya;
            double xv = Xb - Xa;
            double yv = Yb - Ya;
            if (xu * xv + yu * yv < 0)
                return Math.Sqrt(Math.Pow(Xp - Xa, 2) + Math.Pow(Yp - Ya, 2));

            xu = Xp - Xb;
            yu = Yp - Yb;
            xv = -xv;
            yv = -yv;
            if (xu * xv + yu * yv < 0)
                return Math.Sqrt(Math.Pow(Xp - Xb, 2) + Math.Pow(Yp - Yb, 2));

            return Math.Abs((Xp * (Ya - Yb) + Yp * (Xb - Xa) + (Xa * Yb - Xb * Ya))
                    / Math.Sqrt(Math.Pow(Xb - Xa, 2) + Math.Pow(Yb - Ya, 2)));
        }

        public static Color GetSemiTransparentColor(Color p_color)
        {
            return Color.FromArgb(128, p_color.R, p_color.G, p_color.B);
        }

        static public int ConvertColourToWindowsRGB(Color dotNetColour)
        {
            int winRGB = 0;

            // windows rgb values have byte order 0x00BBGGRR
            winRGB |= (int)dotNetColour.R;
            winRGB |= (int)dotNetColour.G << 8;
            winRGB |= (int)dotNetColour.B << 16;

            return winRGB;
        }

        static public Color ConvertWindowsRGBToColour(int windowsRGBColour)
        {
            int r = 0, g = 0, b = 0;

            // windows rgb values have byte order 0x00BBGGRR
            r = (windowsRGBColour & 0x000000FF);
            g = (windowsRGBColour & 0x0000FF00) >> 8;
            b = (windowsRGBColour & 0x00FF0000) >> 16;

            Color dotNetColour = Color.FromArgb(r, g, b);

            return dotNetColour;
        }

        public static string GetTempFileName()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);
            return filename;
        }

        public static byte[] ZipStr(String str)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (DeflateStream gzip =
                  new DeflateStream(output, CompressionMode.Compress))
                {
                    using (StreamWriter writer = new StreamWriter(gzip))
                    {
                        writer.Write(str);
                    }
                }

                return output.ToArray();
            }
        }

        public static string UnZipStr(byte[] input)
        {
            using (MemoryStream inputStream = new MemoryStream(input))
            {
                using (DeflateStream gzip =
                  new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(gzip))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static DateTimeFormatInfo GetDefauldDTFormat()
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy.MM.dd";
            dtfi.DateSeparator = ".";
            return dtfi;
        }

        public static bool IsBaseType(Type p_CtrlType, Type p_BaseType)
        {
            if (p_CtrlType == p_BaseType)
                return true;
            if (p_CtrlType.BaseType != null)
            {
                if (IsBaseType(p_CtrlType.BaseType, p_BaseType))
                    return true;
            }
            return false;
        }

        public static T getFieldValue<T>(this DataRow p_dr, string p_fieldName)
        {
            return getFieldValue<T>(p_dr, p_fieldName, null);
        }


        public static T getFieldValue<T>(this DataRow p_dr, string p_fieldName, object p_default)
        {
            //            if( p_fieldName == "RZN_ID_LIST")
            //                Console.WriteLine("p_fieldName=>" + p_fieldName + ", type:" + p_dr[p_fieldName].GetType().Name);
            if (p_dr.IsNull(p_fieldName) || p_dr.Field<object>(p_fieldName).GetType() == typeof(DBNull))
                if (p_default != null)
                    return (T)p_default;
                else
                    return default(T);
            else
                return (T)Convert.ChangeType(p_dr[p_fieldName], typeof(T));
        }


        public static string GetLocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        public static string RightString(this string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        public static object getObjFromJson(string p_JsonStr, Type p_targetType)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var obj = jss.Deserialize(p_JsonStr, p_targetType);
            return obj;
        }
    
    }
}
