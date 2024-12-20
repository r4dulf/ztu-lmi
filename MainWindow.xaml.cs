using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            richTextBox.Document.Blocks.Clear();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Files (*.rtf)|*.rtf|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    TextRange range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                    range.Load(fileStream, DataFormats.Rtf);
                }
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Files (*.rtf)|*.rtf|All Files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    TextRange range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                }
            }
        }

        private void SaveFileAs(object sender, RoutedEventArgs e)
        {
            SaveFile(sender, e); // Same as SaveFile for simplicity
        }

        private void UndoAction(object sender, RoutedEventArgs e)
        {
            richTextBox.Undo();
        }

        private void CutText(object sender, RoutedEventArgs e)
        {
            richTextBox.Cut();
        }

        private void CopyText(object sender, RoutedEventArgs e)
        {
            richTextBox.Copy();
        }

        private void PasteText(object sender, RoutedEventArgs e)
        {
            richTextBox.Paste();
        }

        private void DeleteText(object sender, RoutedEventArgs e)
        {
            richTextBox.Selection.Text = string.Empty;
        }

        private void InsertAlpha(object sender, RoutedEventArgs e)
        {
            richTextBox.AppendText("α");
        }

        private void InsertBeta(object sender, RoutedEventArgs e)
        {
            richTextBox.AppendText("β");
        }

        private void InsertMu(object sender, RoutedEventArgs e)
        {
            richTextBox.AppendText("µ");
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Text Editor v1.0\nDeveloped with WPF Ribbon.", "About");
        }


        private void ToggleBold(object sender, RoutedEventArgs e)
        {
            var weight = richTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty,
                weight.Equals(FontWeights.Bold) ? FontWeights.Normal : FontWeights.Bold);
        }

        private void ToggleItalic(object sender, RoutedEventArgs e)
        {
            var style = richTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                style.Equals(FontStyles.Italic) ? FontStyles.Normal : FontStyles.Italic);
        }

        private void ToggleUnderline(object sender, RoutedEventArgs e)
        {
            var textDecorations = richTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;
            
            if (textDecorations != null && textDecorations == TextDecorations.Underline)
            {
                richTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void ReplaceSymbols(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null)
            {
                // Отримання введених символів
                string oldSymbol = OldSymbolTextBox.Text;
                string newSymbol = NewSymbolTextBox.Text;

                if (string.IsNullOrEmpty(oldSymbol))
                {
                    MessageBox.Show("Please enter a symbol to replace.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Отримання всього тексту з RichTextBox
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string text = textRange.Text;

                // Заміна символів
                string updatedText = text.Replace(oldSymbol, newSymbol);

                // Оновлення тексту в RichTextBox
                textRange.Text = updatedText;

                MessageBox.Show($"Replaced '{oldSymbol}' with '{newSymbol}' in the text.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AlignLeft_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null)
            {
                TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
                textRange.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
            }
        }

        private void AlignCenter_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null)
            {
                TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
                textRange.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
            }
        }

        private void AlignRight_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null)
            {
                TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
                textRange.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
            }
        }

        private void AlignJustify_Click(object sender, RoutedEventArgs e)
        {
            if (richTextBox != null)
            {
                TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
                textRange.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
            }
        }

    }
}