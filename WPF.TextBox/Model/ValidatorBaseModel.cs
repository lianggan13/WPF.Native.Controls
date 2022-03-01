using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCommon.MVVMFoundation;

namespace WPFTextBox.Model
{
    public abstract class ValidatorBaseModel : NotifyPropertyChanged, IDataErrorInfo
    {
        private string errorMsg;
        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                this.errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
        public string Error => ErrorMsg;

        public virtual string this[string columnName] => throw new NotImplementedException();
        public virtual Action<string> Validator { get; set; }


        public Dictionary<string, string> ValidatorData;

        public string PreviewTextInputRegex { get; set; }
        public ValidatorBaseModel()
        {
            ValidatorData = new Dictionary<string, string>(); // Get over it,I'm a cool man.
        }

        public virtual void PreviewTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public virtual void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            Regex re = new Regex(PreviewTextInputRegex);

            bool match = false;
            string srcTxt = txtBox.Text;
            string selTxt = txtBox.SelectedText;
            string inChar = e.Text;

            if (string.IsNullOrEmpty(selTxt))
            {
                string txt = txtBox.Text.Insert(txtBox.CaretIndex, e.Text);
                match = re.IsMatch(txt);
            }
            else
            {
                int sl = txtBox.CaretIndex;
                int cl = selTxt.Length;
                int el = srcTxt.Length - cl - sl;
                string ss = srcTxt.Substring(0, sl);
                string es = srcTxt.Substring(sl + cl, el);

                string tryStr = ss + inChar + es;
                match = re.IsMatch(tryStr);
            }

            if (match)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
