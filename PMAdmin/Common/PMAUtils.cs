using PMap;
using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PMAdmin.Common
{
    public static class PMAUtils
    {
        public static void SetDataGridColums<T>(DataGrid p_datagrid, bool? p_Partition = null)
        {

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (var col in p_datagrid.Columns.Where(c => c.Visibility == System.Windows.Visibility.Visible))
            {
                // var col = columns[i];
                var prop = props[col.SortMemberPath];
                bool bOkCol = false;
                if (p_Partition != null)
                {
                    if ((bool)p_Partition)
                        bOkCol = prop.Attributes.OfType<AzurePartitionAttr>().Any();
                    else
                        bOkCol = prop.Attributes.OfType<AzureRowAttr>().Any();
                }
                else
                    bOkCol = true;



                if (prop != null && prop.Attributes.OfType<DisplayNameAttributeX>().Any() && bOkCol)
                {
                    col.Visibility = System.Windows.Visibility.Visible;
                    /*
                    if (prop.PropertyType == typeof(System.Windows.Media.ImageSource))
                    {
                        Console.WriteLine("CCC");
                        DataGridTemplateColumn dgrTempleateCol = new DataGridTemplateColumn();
                        dgrTempleateCol.Header = prop.Name;
                        dgrTempleateCol.SortMemberPath = prop.Name;

                        FrameworkElementFactory fwElementFactory = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
                        Binding binding = new Binding(prop.Name);
                        binding.Mode = BindingMode.OneWay;
                        fwElementFactory.SetValue(System.Windows.Controls.Image.SourceProperty, binding);
                        DataTemplate cellTemplate1 = new DataTemplate();
                        cellTemplate1.VisualTree = fwElementFactory;
                        dgrTempleateCol.CellTemplate = cellTemplate1;
                        columns[i] = dgrTempleateCol;
                    }
                    else
                    */
                    {
                        //Formátum beállítása
                        var rightCellStyle = new Style(typeof(System.Windows.Controls.DataGridCell));
                        rightCellStyle.Setters.Add(new Setter(
                             System.Windows.Controls.Control.HorizontalContentAlignmentProperty,
                             System.Windows.HorizontalAlignment.Right));

                        DataGridTextColumn dataGridTextColumn = col as DataGridTextColumn;
                        if (dataGridTextColumn != null)
                        {

                            if (prop.PropertyType == typeof(DateTime))
                            {
                                dataGridTextColumn.Binding.StringFormat = "{0:g}";
                            }
                            else if (prop.PropertyType == typeof(int))
                            {
                                dataGridTextColumn.Binding.StringFormat = "{0:" + Global.INTFORMAT + "}";
                                dataGridTextColumn.CellStyle = rightCellStyle;

                            }
                            else if (prop.PropertyType == typeof(double))
                            {
                                dataGridTextColumn.Binding.StringFormat = "{0:" + Global.NUMFORMAT + "}";
                                dataGridTextColumn.CellStyle = rightCellStyle;
                            }
                        }
                    }

                    //sorrend
                    DisplayNameAttributeX da = prop.Attributes.OfType<DisplayNameAttributeX>().FirstOrDefault();
                    if (da != null)
                    {
                        col.DisplayIndex = da.Order;
                        col.Header = da.DisplayName;
                    }
                }
                else
                {
                    col.Visibility = System.Windows.Visibility.Hidden;

                }
            }
        }

        public static DataGridTemplateColumn SetDataGridImageColums(PropertyDescriptor p_propDescriptor)
        {
            // if (false && pd.PropertyType == typeof(System.Windows.Media.ImageSource))
            if (p_propDescriptor.PropertyType == typeof(System.Windows.Media.ImageSource))
            {

                DataGridTemplateColumn dgrTempleateCol = new DataGridTemplateColumn();
                dgrTempleateCol.Header = p_propDescriptor.Name;
                dgrTempleateCol.SortMemberPath = p_propDescriptor.Name;

                FrameworkElementFactory fwElementFactory = new FrameworkElementFactory(typeof(System.Windows.Controls.Image));
                Binding binding = new Binding(p_propDescriptor.Name);
                binding.Mode = BindingMode.OneWay;
                fwElementFactory.SetValue(System.Windows.Controls.Image.SourceProperty, binding);
                DataTemplate cellTemplate1 = new DataTemplate();
                cellTemplate1.VisualTree = fwElementFactory;
                dgrTempleateCol.CellTemplate = cellTemplate1;
                return dgrTempleateCol;
            }
            else
                return null;
        }

        public static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
