using ComposIT.Model;
using ComposIT.Model.ClassStructure;
using ComposIT.Model.EventHandlers;
using ComposIT.View.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ComposIT.View.ViewControls
{
    public sealed partial class PropertiesPanel : UserControl
    {
        private ClassObject _cObject;

        public PropertiesPanel()
        {
            
            this.InitializeComponent();
            ModelManager.GetInstance().AddOnPrimitiveAttributesChangedHandler((ClassPropertyChangeEvent ev) =>
            {
                FillWithClass(ev.ChangedClass);
            }); 
        }

        public void FillWithClass(ClassObject cObject) {
            _cObject = cObject;
            //PrimitiveListViewType = new ListView();
            if (cObject == null)
            {
                return;
            }

            this.ClassNameTextInput.Text = _cObject.ClassName;
            ColorTextInput.Text = ((_cObject as DrawableClassObject).Brush as SolidColorBrush).Color.ToString();
            ColorRepresenter.Background = (_cObject as DrawableClassObject).Brush;
            PrimitiveListViewType.Items.Clear();
            PrimitiveListViewName.Items.Clear();

            foreach (string identifier in _cObject.PrimitiveAttributes.Keys.ToList()) {
                TextBlock tIdent = new TextBlock();
                tIdent.Text = identifier;
                tIdent.Height = 30;

                string type;
                switch (_cObject.PrimitiveAttributes[identifier])
                {
                    case PrimitiveTypes.Boolean:
                        type = "boolean";
                        break;
                    case PrimitiveTypes.Float:
                        type = "float";
                        break;
                    case PrimitiveTypes.Integer:
                        type = "int";
                        break;
                    case PrimitiveTypes.String:
                        type = "String";
                        break;
                    default:
                        type = "type";
                        break;
                }

                TextBlock tType = new TextBlock();
                tType.IsTextSelectionEnabled = false;
                tIdent.IsTextSelectionEnabled = false;
                tType.Text = type;
                tType.Height = 30;

                PrimitiveListViewType.Items.Add(tType);
                PrimitiveListViewName.Items.Add(tIdent);
            }
            
        }

        private void PrimitivesAddButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement control = sender as FrameworkElement;
            Flyout fly = new Flyout();
            AttributeFlyout content = new AttributeFlyout(_cObject, fly);
            fly.Content = content;
            fly.ShowAt(control);
        }

        private void ClassNameTextInput_LostFocus(object sender, RoutedEventArgs e)
        {
            string newValue = (sender as TextBox).Text;
            if (!String.IsNullOrWhiteSpace(newValue))
            {
                _cObject.ClassName = newValue;
                EditPage.Instance.ClassGallery.FillClasses();
            }
        }

        private void ColorTextInput_LostFocus(object sender, RoutedEventArgs e)
        {
            string newValue = (sender as TextBox).Text.Substring(3);
            if (!String.IsNullOrWhiteSpace(newValue) && newValue.Length == 6)
            {
                bool fail = false;
                byte[] nums = new byte[3];

                for (int i = 0; i < 3; i++) {
                    string pair = newValue.Substring(2 * i, 2);
                    int num = Convert.ToInt32(pair, 16);
                    if (num > 255) {
                        fail = true;
                        break;
                    }
                    nums[i] = (byte) num;
                }

                if (!fail)
                {
                    Brush brush = new SolidColorBrush(Color.FromArgb(255, nums[0], nums[1], nums[2]));
                    ((DrawableClassObject) _cObject).Brush = brush;
                    Debug.WriteLine("Brush set");
                    this.ColorRepresenter.Background = brush;

                    EditPage.Instance.Redraw();
                }

                
            }

        }
    }
}
