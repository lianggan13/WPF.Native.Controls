using System.ComponentModel;
using System.Windows;
using System.Windows.Media;


namespace CustomDialogSample
{
    public partial class SettingsDialog : System.Windows.Window
    {

        // Data for the dialog that supports notification for data binding
        class DialogData : INotifyPropertyChanged
        {
            Color reportColor;
            public Color ReportColor
            {
                get { return reportColor; }
                set { reportColor = value; Notify("ReportColor"); }
            }

            string reportFolder;
            public string ReportFolder
            {
                get { return reportFolder; }
                set { reportFolder = value; Notify("ReportFolder"); }
            }

            // INotifyPropertyChanged Members
            public event PropertyChangedEventHandler PropertyChanged;
            void Notify(string prop) { if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); } }
        }

        DialogData data = new DialogData();

        public Color ReportColor
        {
            get { return data.ReportColor; }
            set { data.ReportColor = value; }
        }

        public string ReportFolder
        {
            get { return data.ReportFolder; }
            set { data.ReportFolder = value; }
        }

        public SettingsDialog()
        {
            InitializeComponent();

            // Allow binding to the data to keep UI bindings up to date
            DataContext = data;

            reportColorButton.Click += reportColorButton_Click;
            folderBrowseButton.Click += folderBrowseButton_Click;
            //cancelButton.Click += new RoutedEventHandler(cancelButton_Click);
            okButton.Click += new RoutedEventHandler(okButton_Click);
        }

        void okButton_Click(object sender, RoutedEventArgs e)
        {
            // the return from ShowDialog will be true
            DialogResult = true;

            // no need to explicitly call this method
            // when DialogResult transitions to non-null
            //Close();
        }

        // Don't need this with IsCancel set
        //void cancelButton_Click(object sender, RoutedEventArgs e) {
        //  // the return from ShowDialog will be false
        //  Close();
        //}

        void reportColorButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            Color color = ReportColor;
            dlg.Color = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportColor = Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B);
            }
        }

        void folderBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.SelectedPath = ReportFolder;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportFolder = dlg.SelectedPath;
            }
        }

    }
}