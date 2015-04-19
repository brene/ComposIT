using ComposIT.View.ViewControls.ToolButtons;
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
    public sealed partial class ToolPanel : UserControl
    {
        public ToolPanel()
        {
            this.InitializeComponent();
        }

        public void DisplaySelectButtonAsSelected()
        {
            DisplaySelectedButton(SelectButton);
        }

        public void DisplayMoveButtonAsSelected()
        {
            DisplaySelectedButton(MoveButton);
        }

        public void DisplayResizeButtonAsSelected()
        {
            DisplaySelectedButton(ResizeButton);
        }

        public void DisplayCreateClassButtonAsSelected()
        {
            DisplaySelectedButton(CreateClassButton);
        }

        public void DisplayAddInstanceButtonAsSelected()
        {
            DisplaySelectedButton(AddInstanceButton);
        }

        public void DisplayDeleteButtonAsSelected()
        {
            DisplaySelectedButton(DeleteButton);
        }

        public void DisplayAddMethodButtonAsSelected()
        {
            DisplaySelectedButton(AddMethodButton);
        }

        public void DisplayAddPrimitiveDataTypeButtonAsSelected()
        {
            DisplaySelectedButton(AddPrimitiveDataTypeButton);
        }

        private void DisplaySelectedButton(AbstractButton button)
        {
            AddInstanceButton.Selected = (button is AddInstanceButton);
            SelectButton.Selected = (button is SelectButton);
            MoveButton.Selected = (button is MoveButton);
            ResizeButton.Selected = (button is ResizeButton);
            CreateClassButton.Selected = (button is CreateClassButton);
            AddMethodButton.Selected = (button is AddMethodButton);
            AddPrimitiveDataTypeButton.Selected = (button is AddPrimitiveDataTypeButton);
            DeleteButton.Selected = (button is DeleteButton);
        }
    }
}
