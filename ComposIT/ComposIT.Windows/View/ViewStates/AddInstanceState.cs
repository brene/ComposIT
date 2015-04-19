using ComposIT.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using ComposIT.View.ViewControls;
using ComposIT.View.ViewControls.ImageButtons;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using ComposIT.Model.ClassStructure;
using ComposIT.View.Rendering;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using System.Diagnostics;
using Windows.UI.Xaml.Shapes;

namespace ComposIT.View.ViewStates
{
    public class AddInstanceState : ToolState
    {
        private bool _clicked;

        private SceneNode _node;

        private DrawableClassObject _clObject;

        private Thickness thickness;

        private double scale;

        private double off_x;

        private double off_y;
        public AddInstanceState()
        {
            EditPage.Instance.ToolPanel.DisplayAddInstanceButtonAsSelected();
     //       EditPage.Instance.CurrentCanvas.Opacity = 0.5;
       //     EditPage.Instance.CurrentCanvas.IsHitTestVisible = false;
       //     EditPage.Instance.OpenDrawingBoardBegin();
            EditPage.Instance.CurrentCanvas.DrawInstances();
        }

        public override void OnClick(object sender, TappedRoutedEventArgs e)
        {
            if (sender is GalleryIcon)
            {
                GalleryIcon icon = sender as GalleryIcon;
 
                if (!_clicked)
                {
                    _clicked = true;
                    // select instance to add

                    EditPage.Instance.CurrentCanvas.Opacity = 1;
                    EditPage.Instance.CurrentCanvas.IsHitTestVisible = true;
                    
                    InstanceObject instance = icon.ClassObject.Instances.First();
 
                    _node = new SceneNode(instance, 1);

                   _clObject = icon.ClassObject;
                }
            }
            else if (sender is DrawingCanvas)
            {
                DrawingCanvas canvas = sender as DrawingCanvas;

                if (_clicked)
                {
                    // create new instance
                    

                    Point pos = new Point()
                    {
                        X = e.GetPosition(canvas).X - _clObject.Size.X / 2,
                        Y = e.GetPosition(canvas).Y - _clObject.Size.Y / 2
                    };

                    Model.ModelManager.GetInstance().CreateInstanceOfClass(_clObject, pos, 1);
                    EditPage.Instance.Redraw();
                    _clicked = false;
                    _node = null;
                    return;
                }
            }
            else if (sender is Shape && _clicked)
            {
            //    _node.Instance.Scale = 1;
                Point pos = new Point()
                {
                    X = e.GetPosition(sender as Shape).X - _clObject.Size.X / 2,
                    Y = e.GetPosition(sender as Shape).Y - _clObject.Size.Y / 2
                };
                InstanceObject inst = new InstanceObject(_node.Instance.ClassObject, pos, 1);
                ((sender as Shape).Tag as SceneNode).Instance.ClassObject.AddAttribute(inst);
                EditPage.Instance.Redraw();
                _clicked = false;
                _node = null;
                return;
            }

          //  Debug.WriteLine(_clicked + "");
        }

        public override void Exit()
        {
            EditPage.Instance.CurrentCanvas.Opacity = 1;
            EditPage.Instance.CurrentCanvas.IsHitTestVisible = true;
        }
    }
}
