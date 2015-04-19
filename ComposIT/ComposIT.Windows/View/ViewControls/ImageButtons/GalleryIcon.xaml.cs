using ComposIT.Model.ClassStructure;
using ComposIT.View.Pages;
using ComposIT.View.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ComposIT.View.ViewControls.ImageButtons
{
    public sealed partial class GalleryIcon : UserControl
    {
        public Canvas Icon { get; private set; }
        public DrawableClassObject ClassObject { get; private set; }

        private ClassGallery _parent;

        private bool _selected = true;

        private bool _clicking;
        public bool Selected2
        {
            get
            {
                return _selected;
            }
            set
            {
                // check if value was changed
                if (value == _selected) return;
                _selected = value;

                if (value)
                {
                    // mark as selected
                    DisplayAsSelected();
                    OnSelection();
                }
                else
                {
                    // mark as not selected
                    DisplayAsUnselected();
                }
                
            }
        }

        public GalleryIcon(ClassGallery parent, Canvas canvas, DrawableClassObject clObject)
        {
            this.InitializeComponent();

            ClassObject = clObject;

            RightTapped += GalleryIcon_RightTapped;

            _parent = parent;
            DrawingGrid.Children.Add(canvas);
            Icon = canvas;
            IconName.Text = ClassObject.ClassName;

            PointerPressed += OnPointerPressed;
            PointerReleased += OnPointerReleased;
            PointerEntered += OnPointerEntered;
            PointerExited += OnPointerExited;
            Tapped += OnTapped;
        }

        private void GalleryIcon_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            EditPage.Instance.DrawClassView(ClassObject);
        }

        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
          //  EditPage.Instance.DrawClassView(_classObject);
            EditPage.Instance.ToolState.OnClick(sender, e);
        }

        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            Close.Begin();
            if (Selected2 && !_clicking) return;

            DisplayAsUnselected();
        }

        private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Open.Begin();
            if (Selected2) return;
            DisplayAsHovered();
        }

        private void ToggleSelection()
        {
            Selected2 = !Selected2;
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _clicking = false;
            DisplayAsUnselected();   
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _clicking = true;
            DisplayAsSelected();
        }

        private void OnSelection()
        {
            _parent.SelectGalleryIcon(this);
        }

        private void DisplayAsSelected()
        {
            DrawingGrid.Opacity = 0.5;
        }

        private void DisplayAsHovered() 
        {
            DrawingGrid.Opacity = 0.75;
        }

        public void DisplayAsUnselected()
        {
            DrawingGrid.Opacity = 1;
        }
    }
}
