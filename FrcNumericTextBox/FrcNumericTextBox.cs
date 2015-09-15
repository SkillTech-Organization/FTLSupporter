using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrcControls
{
    public partial class FrcNumericTextBox : TextBox
    {
        private String DecimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        private String GroupSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;

        private int m_Digits = 10;
        /// <summary>
        /// Number of digits on the left side
        /// </summary>
        [Description("Number of digits on the left side")]
        [DisplayName("Digits")]
        [Category("")]
        public int Digits
        {
            get { return m_Digits; }
            set
            {
                m_Digits = value >= 1 ? value : 1;
                Text = Sign + IntPart + DecimalSeparator + FracPart;
            }
        }

        private int m_Precision = 2;
        /// <summary>
        /// Number of digits on the right side
        /// </summary>
        [Description("Number of digits on the right side")]
        [DisplayName("Precision")]
        [Category("")]
        public int Precision
        {
            get { return m_Precision; }
            set
            {
                m_Precision = value >= 0 ? value : 0;
                FracPart = concat("0", Precision);
                if (Precision > 0)
                    DecimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                else
                    DecimalSeparator = "";

                Text = Sign + IntPart + DecimalSeparator + FracPart; 
            }
        }

        [Description("Box value")]
        [DisplayName("Value")]
        [Category("")]
        public decimal Value
        {
            get
            {
                return getValueFromText(Text);
            }
            set
            {
                decimal temp = value;
                if (temp < Minimum)
                    temp = Minimum;
                if (temp > Maximum)
                    temp = Maximum;


                if (temp < 0)
                {
                    Sign = "-";
                    temp = -temp;
                }
                else
                    Sign = "";

                decimal intp = Math.Floor(temp);
                IntPart = intp.ToString();

                // Ridiculously complicated way of setting FracPart in (hopefully) every case
                if (Precision > 0)
                {
                    decimal frac = temp - intp;
                    int len = frac.ToString().Length - 2;
                    for (int i = 0; i < len; i++)
                        frac *= 10m;
                    String fractemp;
                    if (frac != 0m)
                        fractemp = frac.ToString() + concat("0", Precision);
                    else
                        fractemp = concat("0", Precision);

                    FracPart = fractemp.Substring(0, Precision);
                }
                else
                    FracPart = "";

                Text = Sign + IntPart + DecimalSeparator + FracPart;     
            }
        }

        private decimal m_minimum = Decimal.MinValue;
        [Description("Minimum value")]
        [DisplayName("Minimum")]
        [Category("")]
        public decimal Minimum
        {
            get { return m_minimum; }
            set { m_minimum = value; }
        }

        private decimal m_maximum = Decimal.MaxValue;
        [Description("Maximum value")]
        [DisplayName("Maximum")]
        [Category("")]
        public decimal Maximum
        {
            get { return m_maximum; }
            set { m_maximum = value; }
        }

        private bool InIntegerPosition = true;
        private string IntPart;
        private string FracPart;
        private string Sign;
        private int FracPartPosition;

        public FrcNumericTextBox()
        {
            InitializeComponent();

            TextAlign = HorizontalAlignment.Right;
            IntPart = "0";
            FracPart = concat("0", Precision);
            Sign = "";
            FracPartPosition = 0;

            Text = Sign + IntPart + DecimalSeparator + FracPart;
            position(0);

            this.KeyPress += new KeyPressEventHandler(zKeyPress);
            this.KeyDown += new KeyEventHandler(zKeyDown);
            this.Enter += new EventHandler(FrcNumericTextBox_Enter);
        }

        /// <summary>
        /// Entering Textbox, defaulting positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrcNumericTextBox_Enter(object sender, EventArgs e)
        {
            InIntegerPosition = true;
            position(0);
            FracPartPosition = 0;
        }

        /// <summary>
        /// Repeats string n times
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private string concat(string s, int n)
        {
            string temp = "";

            for (int i = 0; i < n; i++)
                temp += s;

            return temp;
        }

        /// <summary>
        /// Positions cursor
        /// </summary>
        /// <param name="i"></param>
        private void position(int i)
        {
            if (InIntegerPosition)
            {
                if (DecimalSeparator.Length > 0)
                    i = Text.IndexOf(DecimalSeparator) - i;
                else
                    i = Text.Length;
            }
            else
                i = Text.IndexOf(DecimalSeparator) + i + 1;

            SelectionStart = i;
            SelectionLength = 0;
        }

        /// <summary>
        /// Format integer part (separation by 1000)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string formatka(string s)
        {
            string temp = "";

            int j = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                temp = s[i] + temp;
                j++;
                if (j == 3)
                {
                    j = 0;
                    temp = GroupSeparator + temp;
                }
            }

            return temp;
        }

        private decimal getValueFromText(string p_text)
        {
            decimal temp;
            string text = p_text.Replace(GroupSeparator, "");
            //temp = Decimal.Parse(text);
            if (decimal.TryParse(text, out temp))
                return temp;
            return 0;
        }

        /// <summary>
        /// Keypress handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            string NewIntPart = IntPart;
            string NewFracPart = FracPart;
            string NewSign = Sign;
            int NewFracPartPosition = FracPartPosition;

            // Toggle between int/frac positions by pressing space/decimalseparator
            // Only works when Precision > 0
            if (DecimalSeparator.Length > 0 &&  (e.KeyChar == ' ' || e.KeyChar == DecimalSeparator[0]))
            {
                InIntegerPosition = !InIntegerPosition;
                NewFracPartPosition = 0;
                position(0);
            }
            // Toggle sign
            else if (e.KeyChar == '-' || e.KeyChar == '+')
            {
                if (NewSign == "-")
                    NewSign = "";
                else 
                    NewSign = "-";
            }
            // Enter numbers
            else if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                // In integer position overwrites inital 0, then concatenates to the right side
                if (InIntegerPosition)
                {
                    if (NewIntPart == "0")
                        NewIntPart = "";

                    if (NewIntPart.Length < Digits)
                        NewIntPart += e.KeyChar;
                }
                // In fractional position overwrites 0-s and increments frac position
                else
                {
                    if (NewFracPartPosition < Precision)
                    {
                        NewFracPart = NewFracPart.Remove(NewFracPartPosition, 1).Insert(NewFracPartPosition, e.KeyChar.ToString());
                        NewFracPartPosition++;
                    }
                }
            }
            // Handling backspace
            else if ((Keys)(e.KeyChar) == Keys.Back)
            {
                // If in integer position, delete rightmost character, set it to 0 if length = 0
                if (InIntegerPosition)
                {
                    NewIntPart = NewIntPart.Substring(0, NewIntPart.Length - 1);
                    if (NewIntPart == "")
                        NewIntPart = "0";
                }
                else
                {
                    // In fractional position: overwrite current number to 0 and decrement position
                    if (NewFracPartPosition > 0)
                    {
                        NewFracPart = NewFracPart.Remove(NewFracPartPosition - 1, 1).Insert(NewFracPartPosition - 1, "0");
                        NewFracPartPosition--;
                    }
                    // If position = 0 start over from the rightmost character to avoid not being able to delete at all
                    else
                    {
                        NewFracPartPosition = Precision;
                        NewFracPart = NewFracPart.Remove(NewFracPartPosition - 1, 1).Insert(NewFracPartPosition - 1, "0");
                        NewFracPartPosition--;
                    }
                }
            }

            // if the number is 0, delete sign
            if (NewIntPart == "0" && NewFracPart == concat("0", Precision))
            {
                NewSign = "";
            }

            // Assemble text
            string NewText = NewSign + formatka(NewIntPart) + DecimalSeparator + NewFracPart;
            decimal NewValue = getValueFromText(NewText);
            if (NewValue >= Minimum && NewValue <= Maximum)
            {
                IntPart = NewIntPart;
                FracPart = NewFracPart;
                Sign = NewSign;
                FracPartPosition = NewFracPartPosition;
                Text = NewText;
            }

            // Position cursor 
            if(InIntegerPosition)
                position(0);
            else
                position(FracPartPosition);

        }

        /// <summary>
        /// Filters keydowns except Del (becomes backspace)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
                zKeyPress(sender, new KeyPressEventArgs((char)(Keys.Back)));

            e.Handled = true;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
        }

    }
}
