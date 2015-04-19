using ComposIT.View.Pages;
using ComposIT.View.ViewStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ComposIT.View.ViewControls.ToolButtons
{
    public class AddPrimitiveDataTypeButton : AbstractButton
    {

        public AddPrimitiveDataTypeButton()
        {

            ButtonFace.Source = new BitmapImage(new Uri("ms-appx:///Assets/Text-Editor.png"));
            ButtonName = "Add Primitve Attribute";
        }

        protected override void OnSelection()
        {
            EditPage.Instance.ToolState = new AddPrimitiveDataTypeState();
        }
    }
}
