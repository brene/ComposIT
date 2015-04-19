using ComposIT.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;
using ComposIT.Model;
using ComposIT.Model.ClassStructure;
using ComposIT.View.Rendering;

namespace ComposIT.View.ViewStates
{
    public class DeleteState : ToolState
    {
        public DeleteState()
        {
            EditPage.Instance.ToolPanel.DisplayDeleteButtonAsSelected();
        }

        public override void OnClick(object sender, TappedRoutedEventArgs e)
        {
            if (!(sender is Shape))
            {
                return;
            }
            InstanceObject obj = ((sender as Shape).Tag as SceneNode).Parent.Instance;
            if (obj == null)
            {
                ModelManager.GetInstance().RemoveClass(((sender as Shape).Tag as SceneNode).Instance.ClassObject);
                EditPage.Instance.Redraw();
            }
            else
            {
                obj.ClassObject.RemoveAttribute(((sender as Shape).Tag as SceneNode).Instance);
                EditPage.Instance.Redraw();
            }
        }
    }
}
