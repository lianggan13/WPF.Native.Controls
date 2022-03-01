using System;
using System.Linq;
using System.Windows.Input;

namespace WPFTextBox.Model
{
    public class SingleTextBoxModel : ValidatorBaseModel
    {
        private string name;
        private string value;
        private string unit;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                this.unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                //if (isEnabled != value)
                //{
                //    isEnabled = value;
                //    TriggerValidatorManually();
                //}
                isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled)); // Note:[SelectedIndex="0"] can trigger "PropertyChanged" evet
            }
        }


        private string toolTip;
        public string ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {

                if (value.Contains(Environment.NewLine))
                {
                    var tps = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    var ss = tps.Select(t => string.Format("· {0}{1}", t, Environment.NewLine)).ToList();
                    this.toolTip = string.Join("", ss).TrimEnd(new char[] { '\r', '\n' });
                }
                else
                {
                    this.toolTip = value;
                }

                OnPropertyChanged(nameof(ToolTip));
            }
        }



        public void SingleTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }



        public override string this[string columnName]
        {
            get
            {
                // ErrorMsg = string.Empty;

                Validator?.Invoke(value);

                return ErrorMsg;
            }
        }
    }
}
