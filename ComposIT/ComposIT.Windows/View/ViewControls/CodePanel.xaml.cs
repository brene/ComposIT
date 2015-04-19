using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ComposIT.Model.ClassStructure;
using ComposIT.Model.Translation;
using ComposIT.Model;
using ComposIT.Model.EventHandlers;
using Windows.UI;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ComposIT.View.ViewControls
{
    public sealed partial class CodePanel : UserControl
    {
        public CodePanel()
        {
            ModelManager m = new ModelManager();



            m.AddOnTranslationChangedHandler((TranslationChangedEvent ev) =>
            {
                TextBox.Document.SetText(TextSetOptions.FormatRtf, ev.text);
                TextBox.Document.ApplyDisplayUpdates();
            });

            this.InitializeComponent();

            foreach (string language in ModelManager.GetInstance().GetAllLanguages()) {
                Button btn = new Button();
                btn.Height = 45;
                btn.BorderThickness = new Thickness(0);
                btn.Background = new SolidColorBrush(new Color()
                {
                    A = 0xFF,
                    R = 0x4D,
                    G = 0x4D,
                    B = 0x4D
                });
                btn.Content = language;
                btn.Tapped += (sender, e) =>
                {
                    ModelManager.GetInstance().SetCurrentLanguage(language);
                };
                TabPanel.Children.Add(btn);
            }

            TextBox.Loaded += (sender, e) =>
            {
                ModelManager.GetInstance().SetCurrentLanguage(ModelManager.GetInstance().GetAllLanguages().First());
            };
            
        }

        private async void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FolderPicker picker = new FolderPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".java");
            picker.FileTypeFilter.Add(".cpp");
            picker.FileTypeFilter.Add(".cs");
            picker.FileTypeFilter.Add(".h");
            var folder = await picker.PickSingleFolderAsync();
            ModelManager.GetInstance().ExportCode(folder);
        }
    }
}
