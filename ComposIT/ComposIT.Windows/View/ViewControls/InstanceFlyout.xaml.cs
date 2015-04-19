using ComposIT.Model.ClassStructure;
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

namespace ComposIT.View.ViewControls
{
    public partial class InstanceFlyout : UserControl
    {
        private InstanceObject _instance;
        private Flyout _flyout;

        public InstanceFlyout(InstanceObject instance, Flyout flyout)
        {
            InitializeComponent();
            _instance = instance;
            _flyout = flyout;
            this.AttributeNameField.Text = instance.AttributeName ?? "";
        }

        private void AttributeSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string newValue = this.AttributeNameField.Text;
            if (!String.IsNullOrWhiteSpace(newValue))
            {
                _instance.AttributeName = newValue;
                _flyout.Hide();
            }
        }
    }
}
