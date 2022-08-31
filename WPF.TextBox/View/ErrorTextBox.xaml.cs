using System.Windows;
using System.Windows.Controls;
using WPF.TextBox.ViewModel;

namespace WPF.TextBox.View
{
    /// <summary>
    /// SingleTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorTextBox : Page
    {
        public ErrorTextBoxVM VM { get; }
        public ErrorTextBox()
        {
            InitializeComponent();
            VM = base.DataContext as ErrorTextBoxVM;
        }

        public void Value_ErrorEvent(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                VM.Errors.Add(e.Error);
            else
                VM.Errors.Remove(e.Error);
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            VM.AddValidator((string value) =>
            {
                string errorMsg = string.Empty;
                string toolTip = "Input Something";

                if (string.IsNullOrEmpty(value))
                {
                    errorMsg = "Empty Data";
                }

                VM.Model.ErrorMsg = errorMsg;
                VM.Model.ToolTip = toolTip;

            });


            VM.FillData("TextName", "TextValue",
            preRegex: @"^(^-?(([1-9]{1}[0-9]{0,1})(\.(\d){0,1})?)$)|(^-?([0]{1})(\.(\d){0,1})?$)|(-)$",
            unit: "dbM",
            save: () =>
            {
                MessageBox.Show("Save Done.");
            });
        }
    }
}
