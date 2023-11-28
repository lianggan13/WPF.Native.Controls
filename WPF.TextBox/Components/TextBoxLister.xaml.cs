using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.TextBox.Components
{
    /// <summary>
    /// TextBoxLister.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxLister : ContentControl
    {
        public TextBoxLister()
        {
            InitializeComponent();
        }

        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        ArrayList list = new ArrayList();
        int indexSelected = -1;
        private void ListControl_Loaded(object sender, RoutedEventArgs e)
        {
            var lister = sender as ContentControl;
            lister?.AddHandler(TextBlock.MouseLeftButtonDownEvent,
                new MouseButtonEventHandler(TextBlockOnMouseLeftButtonDown));

            // Scroll the selected item into view when Lister is first displayed.
            ScrollIntoView();
        }

        // Public properties involving the TextBox item.
        public string Text
        {
            get { return txtbox.Text; }
            set { txtbox.Text = value; }
        }

        bool isReadOnly;
        public bool IsReadOnly
        {
            set { isReadOnly = value; }
            get { return isReadOnly; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        // SelectedIndex property is responsible for displaying selection bar.
        public int SelectedIndex
        {
            set
            {
                if (value < -1 || value >= Count)
                    throw new ArgumentOutOfRangeException("SelectedIndex");

                if (value == indexSelected)
                    return;

                if (indexSelected != -1)
                {
                    TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.WindowBrush;
                    txtblk.Foreground = SystemColors.WindowTextBrush;
                }

                indexSelected = value;

                if (indexSelected > -1)
                {
                    TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.HighlightBrush;
                    txtblk.Foreground = SystemColors.HighlightTextBrush;
                }
                ScrollIntoView();

                // Trigger SelectionChanged event.
                OnSelectionChanged(EventArgs.Empty);
            }
            get
            {
                return indexSelected;
            }
        }

        // SelectedItem property makes use of SelectedIndex.
        public object SelectedItem
        {
            set
            {
                SelectedIndex = list.IndexOf(value);
            }
            get
            {
                if (SelectedIndex > -1)
                    return list[SelectedIndex];

                return null;
            }
        }

        // On a mouse click, set the keyboard focus.
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);
            Focus();
        }

        // When the keyboard focus comes, pass it to the TextBox.
        protected override void OnGotKeyboardFocus(
                                        KeyboardFocusChangedEventArgs args)
        {
            base.OnGotKeyboardFocus(args);

            if (args.NewFocus == this)
            {
                txtbox.Focus();
                if (SelectedIndex == -1 && Count > 0)
                    SelectedIndex = 0;
            }
        }

        // When a letter key is typed, pass it to GoToLetter method of Lister.
        protected override void OnPreviewTextInput(TextCompositionEventArgs args)
        {
            base.OnPreviewTextInput(args);

            if (IsReadOnly)
            {
                GoToLetter(args.Text[0]);
                args.Handled = true;
            }
        }

        // Handling of cursor movement keys to change selected item.
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            if (SelectedIndex == -1)
                return;

            switch (args.Key)
            {
                case Key.Home:
                    if (Count > 0)
                        SelectedIndex = 0;
                    break;
                case Key.End:
                    if (Count > 0)
                        SelectedIndex = Count - 1;
                    break;
                case Key.Up:
                    if (SelectedIndex > 0)
                        SelectedIndex--;
                    break;
                case Key.Down:
                    if (SelectedIndex < Count - 1)
                        SelectedIndex++;
                    break;
                case Key.PageUp:
                    PageUp();
                    break;
                case Key.PageDown:
                    PageDown();
                    break;
                default:
                    return;
            }
            args.Handled = true;
        }

        // Public methods to add, insert, etc, items in Lister.
        public void Add(object obj)
        {
            list.Add(obj);
            TextBlock txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Add(txtblk);
        }
        public void Insert(int index, object obj)
        {
            list.Insert(index, obj);
            TextBlock txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Insert(index, txtblk);
        }
        public void Clear()
        {
            SelectedIndex = -1;
            stack.Children.Clear();
            list.Clear();
        }
        public bool Contains(object obj)
        {
            return list.Contains(obj);
        }

        // Public methods to page up and down through the list.
        public void PageUp()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            int index = SelectedIndex - (int)(Count *
                                scroll.ViewportHeight / scroll.ExtentHeight);
            if (index < 0)
                index = 0;

            SelectedIndex = index;
        }
        public void PageDown()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            int index = SelectedIndex + (int)(Count *
                                scroll.ViewportHeight / scroll.ExtentHeight);
            if (index > Count - 1)
                index = Count - 1;

            SelectedIndex = index;
        }


        // This method is called to select an item based on a typed letter.
        public void GoToLetter(char ch)
        {
            int offset = SelectedIndex + 1;

            for (int i = 0; i < Count; i++)
            {
                int index = (i + offset) % Count;

                if (Char.ToUpper(ch) == Char.ToUpper(list[index].ToString()[0]))
                {
                    SelectedIndex = index;
                    break;
                }
            }
        }

        // Private method to scroll selected item into view.
        void ScrollIntoView()
        {
            if (Count == 0 || SelectedIndex == -1 ||
                                scroll.ViewportHeight > scroll.ExtentHeight)
                return;

            double heightPerItem = scroll.ExtentHeight / Count;
            double offsetItemTop = SelectedIndex * heightPerItem;
            double offsetItemBot = (SelectedIndex + 1) * heightPerItem;

            if (offsetItemTop < scroll.VerticalOffset)
                scroll.ScrollToVerticalOffset(offsetItemTop);

            else if (offsetItemBot > scroll.VerticalOffset + scroll.ViewportHeight)
                scroll.ScrollToVerticalOffset(
                      offsetItemBot - scroll.ViewportHeight);
        }


        // Event handler and trigger.
        void TextBlockOnMouseLeftButtonDown(object sender,
                                            MouseButtonEventArgs args)
        {
            if (args.Source is TextBlock)
                SelectedIndex = stack.Children.IndexOf(args.Source as TextBlock);
        }

        public event EventHandler SelectionChanged;
        protected virtual void OnSelectionChanged(EventArgs args)
        {
            if (SelectedIndex == -1)
                Text = "";
            else
            {
                Text = SelectedItem.ToString();
            }

            SelectionChanged?.Invoke(this, args);
        }
    }
}
