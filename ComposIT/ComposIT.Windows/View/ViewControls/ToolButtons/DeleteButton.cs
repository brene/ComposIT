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
    public class DeleteButton : AbstractButton
    {
        public DeleteButton()
        {

            ButtonFace.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cards Delete.png"));
            ButtonName = "Delete Instance";
        }
        protected override void OnSelection()
        {
            EditPage.Instance.ToolState = new DeleteState();
        }
    }
}
