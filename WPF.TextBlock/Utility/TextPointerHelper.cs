using System.Text;

namespace WPF.TextBlock.Utility
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public static class TextPointerHelper
    {
        public static string GetText(TextPointer textStart, TextPointer textEnd)
        {
            StringBuilder output = new StringBuilder();

            TextPointer tp = textStart;

            while (tp != null && tp.CompareTo(textEnd) < 0)
            {
                var pc = tp.GetPointerContext(LogicalDirection.Forward);
                if (pc == TextPointerContext.Text)
                {
                    output.Append(tp.GetTextInRun(LogicalDirection.Forward));
                }
                tp = tp.GetNextContextPosition(LogicalDirection.Forward);
            }
            return output.ToString();
        }

        public static void InsertText(RichTextBox richTextBox)
        {
            TextPointer insertionPoint =
              richTextBox.Selection.Start.GetInsertionPosition(LogicalDirection.Forward);
            insertionPoint.InsertTextInRun("Text added from code");
        }

        // This method returns the position just inside of the first text Run (if any) in a 
        // specified text container.
        public static TextPointer FindFirstRunInTextContainer(DependencyObject container)
        {
            TextPointer position = null;

            if (container != null)
            {
                if (container is FlowDocument)
                    position = ((FlowDocument)container).ContentStart;
                else if (container is TextBlock)
                    position = ((TextBlock)container).ContentStart;
                else
                    return position;
            }
            // Traverse content in forward direction until the position is immediately after the opening 
            // tag of a Run element, or the end of content is encountered.
            while (position != null)
            {
                // Is the current position just after an opening element tag?
                if (position.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.ElementStart)
                {
                    // If so, is the tag a Run?
                    if (position.Parent is Run)
                        break;
                }

                // Not what we're looking for; on to the next position.
                position = position.GetNextContextPosition(LogicalDirection.Forward);
            }

            // This will be either null if no Run is found, or a position just inside of the first Run element in the
            // specifed text container.  Because position is formed from ContentStart, it will have a logical direction
            // of Backward.
            return position;
        }

        // This method will search for a specified word (string) starting at a specified position.
        public static TextPointer FindWordFromPosition(TextPointer position, string word)
        {
            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = position.GetTextInRun(LogicalDirection.Forward);

                    // Find the starting index of any substring that matches "word".
                    int indexInRun = textRun.IndexOf(word);
                    if (indexInRun >= 0)
                    {
                        position = position.GetPositionAtOffset(indexInRun);
                        break;
                    }
                }
                else
                {
                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }
            }

            // position will be null if "word" is not found.
            return position;
        }
    }
}
