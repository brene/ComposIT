using ComposIT.View.Pages;
using ComposIT.View.ViewStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace ComposIT.View.ViewControls.ToolButtons
{
    public class CreateClassButton : AbstractButton
    {
        public CreateClassButton()
        {

            ButtonFace.Source = new BitmapImage(new Uri("ms-appx:///Assets/Document-Add.png"));
            ButtonName = "Create Square Class";
        }
        protected override void OnSelection()
        {
            EditPage.Instance.ToolState = new CreateClassState();
        }
    }
}
