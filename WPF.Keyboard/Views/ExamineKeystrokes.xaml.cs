namespace WPF.Keyboard.Views
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// ExamineKeystrokes.xaml 的交互逻辑
    /// </summary>
    public partial class ExamineKeystrokes : UserControl
    {
        string strHeader = "Event     Key                 Sys-Key   Text      " +
                         "Ctrl-Text Sys-Text  Ime       KeyStates      " +
                         "IsDown  IsUp   IsToggled IsRepeat ";
        string strFormatKey = "{0,-10}{1,-20}{2,-10}                          " +
                              "    {3,-10}{4,-15}{5,-8}{6,-7}{7,-10}{8,-10}";
        string strFormatText = "{0,-10}                              " +
                               "{1,-10}{2,-10}{3,-10}";

        public ExamineKeystrokes()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            //Keyboard.PrimaryDevice.IsKeyDown...
            base.OnKeyDown(args);
            DisplayKeyInfo(args);
        }
        protected override void OnKeyUp(KeyEventArgs args)
        {
            base.OnKeyUp(args);
            DisplayKeyInfo(args);
        }
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);
            string str =
                String.Format(strFormatText, args.RoutedEvent.Name, args.Text,
                                             args.ControlText, args.SystemText);
            DisplayInfo(str);
        }
        void DisplayKeyInfo(KeyEventArgs args)
        {
            string str =
                String.Format(strFormatKey, args.RoutedEvent.Name, args.Key,
                     args.SystemKey, args.ImeProcessedKey, args.KeyStates,
                     args.IsDown, args.IsUp, args.IsToggled, args.IsRepeat);
            DisplayInfo(str);
        }
        void DisplayInfo(string str)
        {
            TextBlock text = new TextBlock();
            text.Text = str;
            stack.Children.Add(text);
            scroll.ScrollToBottom();
        }



    }
}
