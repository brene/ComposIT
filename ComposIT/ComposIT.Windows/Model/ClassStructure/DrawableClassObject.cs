using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ComposIT.Model.ClassStructure
{
    /// <summary>
    /// Extends the ClassObject class by defining display information.
    /// </summary>
    public class DrawableClassObject : ClassObject
    {
        /// <summary>
        /// Size of the class object on the Canvas, saved as a vector
        /// </summary>
        public Point Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                ModelManager.GetInstance().ClassStyleChanged(this);
            }
        }
        private Point _size;

        /// <summary>
        /// Coloring information of the Class Object on the Canvas
        /// </summary>
        public Brush Brush
        {
            get
            {
                return _brush;
            }
            set
            {
                _brush = value;
                ModelManager.GetInstance().ClassStyleChanged(this);

            }
        }
        private Brush _brush;

        public VisualShape VisualShape { get; private set; }

       
        /// <summary>
        /// Constructor which initilizes the Class Object.
        /// </summary>
        public DrawableClassObject(Point size, Brush brush, VisualShape shape)
        {
            Size = size;
            VisualShape = shape;
            if (brush == null)
            {
                GradientStopCollection gradient = new GradientStopCollection();
                GradientStop top = new GradientStop();
                GradientStop down = new GradientStop();
                top.Color = Color.FromArgb(0, 38, 50, 56);
                down.Color = Color.FromArgb(0, 84, 110, 122);
                down.Offset = 1.0;
                gradient.Add(top);
                gradient.Add(down);
                Brush = new LinearGradientBrush(new GradientStopCollection(), 90);
            }
            else
            {
                Brush = brush;
            }
        }

    }
}
