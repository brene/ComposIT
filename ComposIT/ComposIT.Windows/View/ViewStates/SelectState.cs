using ComposIT.Model.ClassStructure;
using ComposIT.View.Pages;
using ComposIT.View.ViewControls;
using ComposIT.View.ViewControls.ImageButtons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;
using ComposIT.View.Rendering;

namespace ComposIT.View.ViewStates
{
    public class SelectState : ToolState
    {
        public SelectState()
        {
            EditPage.Instance.ToolPanel.DisplaySelectButtonAsSelected();
        }

        public override void OnClick(object sender, TappedRoutedEventArgs e)
        {
            if (!(sender is GalleryIcon) && !(sender is Shape)) return;
            if (sender is GalleryIcon)
            {
                GalleryIcon icon = sender as GalleryIcon;
                EditPage.Instance.DrawClassView(icon.ClassObject);
            }
            else if (sender is Shape)
            {
                Shape shape = sender as Shape;
                Flyout fly = new Flyout();
                InstanceFlyout content = new InstanceFlyout((shape.Tag as SceneNode).Instance, fly);
                fly.Content = content;
                fly.ShowAt(shape);
                EditPage.Instance.PropertiesPanel.FillWithClass((shape.Tag as SceneNode).Instance.ClassObject);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
