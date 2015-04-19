using ComposIT.View.Pages;
using ComposIT.View.ViewControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using ComposIT.Model;
using ComposIT.Model.ClassStructure;
using Windows.Foundation;
using ComposIT.View.Rendering;
using ComposIT.View.ViewControls.ImageButtons;

namespace ComposIT.View.ViewStates
{
    public class CreateClassState : ToolState
    {
        Rectangle rect;
        bool clicked;
        double off_x;
        double off_y;
        Thickness thickness;
        private RandomColor _randomColor;

        public CreateClassState()
        {
            EditPage.Instance.ToolPanel.DisplayCreateClassButtonAsSelected();
            _randomColor = new RandomColor();
           // EditPage.Instance.CloseDrawingBoardBegin();
            EditPage.Instance.DrawEmptyClassView();
        }

        public override void OnClick(object sender, TappedRoutedEventArgs e)
        {
            if (sender is DrawingCanvas)
            {
                DrawingCanvas canvas = sender as DrawingCanvas;

                if (clicked)
                {
                    clicked = false;

                    Point pos = new Point()
                    {
                        X = rect.Margin.Left,
                        Y = rect.Margin.Top
                    };
                    DrawableClassObject clObject = ModelManager.GetInstance()
                        .CreateNewClass(new Point(rect.Width, rect.Height), rect.Fill, VisualShape.Rectangle, pos, 1);
                    EditPage.Instance.ClassGallery.FillClasses();
                    EditPage.Instance.DrawClassView(clObject);
                    EditPage.Instance.PropertiesPanel.FillWithClass(clObject);
                    return;
                }

                rect = new Rectangle();
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                rect.Fill = _randomColor.Shuffle();
                thickness = new Thickness(x, y, 0, 0);
                rect.Margin = thickness;
                off_x = x;
                off_y = y;
                canvas.AddUIControl(rect);

                clicked = true;
            }
            else if (sender is GalleryIcon) 
            {
                GalleryIcon icon = sender as GalleryIcon;
                EditPage.Instance.DrawClassView(icon.ClassObject);
            }
            
        }

        public override void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!clicked) return;
            if (!(sender is DrawingCanvas)) return;
            DrawingCanvas canvas = sender as DrawingCanvas;
            double x1 = e.GetCurrentPoint(canvas).Position.X;
            double x2 = off_x;

            double y1 = e.GetCurrentPoint(canvas).Position.Y;
            double y2 = off_y;

            if (x1 - x2 < 0)
            {
                thickness.Left = x1;
            }
            else
            {
                thickness.Left = x2;
            }

            if (y1 - y2 < 0)
            {
                thickness.Top = y1;
            }
            else
            {
                thickness.Top = y2;
            }
            rect.Margin = thickness;

            rect.Height = Math.Abs(y1 - y2);
            rect.Width = Math.Abs(x1 - x2);
            
        }
    }
}
