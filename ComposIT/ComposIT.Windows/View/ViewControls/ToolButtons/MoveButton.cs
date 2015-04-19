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
    public class MoveButton : AbstractButton
    {
        public MoveButton()
        {
            ButtonFace.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mouse-Drag.png"));
            ButtonName = "Create Circle Class";
        }
        protected override void OnSelection()
        {
            EditPage.Instance.ToolState = new MoveState();
        }
    }
}
