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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ComposIT.View.ViewControls.ImageButtons
{
    public partial class AbstractImageButton : UserControl
    {
        private bool _selected = true;

        protected Image ButtonFace { get { return BaseImage; }}

        private bool _clicking;
        public bool Selected
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

        public AbstractImageButton()
        {
            this.InitializeComponent();

            PointerPressed += OnPointerPressed;
            PointerReleased += OnPointerReleased;
            PointerEntered += OnPointerEntered;
            PointerExited += OnPointerExited;
            Tapped += OnTapped;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            ToggleSelection();
        }

        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (Selected && !_clicking) return;

            DisplayAsUnselected();
        }

        private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (Selected) return;
            DisplayAsHovered();
        }

        private void ToggleSelection()
        {
            Selected = !Selected;
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

        protected virtual void OnSelection()
        {
            throw new InvalidOperationException();
        }

        private void DisplayAsSelected()
        {
            BaseImage.Opacity = 0.5;
        }

        private void DisplayAsHovered() 
        {
            BaseImage.Opacity = 0.75;
        }

        public void DisplayAsUnselected()
        {
            BaseImage.Opacity = 1;
        }

    }
    
}
