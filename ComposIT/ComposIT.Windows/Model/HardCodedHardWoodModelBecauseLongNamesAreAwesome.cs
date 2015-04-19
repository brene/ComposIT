using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ComposIT.Model
{
    public class HardCodedHardWoodModelBecauseLongNamesAreAwesome : ModelManager
    {
        public HardCodedHardWoodModelBecauseLongNamesAreAwesome() : base()
        {
            DrawableClassObject c1 = CreateNewClass(new Point(80, 150), new SolidColorBrush(Colors.AliceBlue), VisualShape.Rectangle, new Point(40, 20), 1);
            DrawableClassObject c2 = CreateNewClass(new Point(40, 40), new SolidColorBrush(Colors.Red), VisualShape.Rectangle, new Point(60, 10), 1);
            DrawableClassObject c3 = CreateNewClass(new Point(190, 50), new SolidColorBrush(Colors.Olive), VisualShape.Ellipse, new Point(120, 90), 1);

            AddInstanceToClass(c2.Instances.First(), c1.Instances.First());
            CreateInstanceOfClass(c2, new Point(350, 140), 1);
            CreateInstanceOfClass(c2, new Point(350, 140), 0.8);
        }
    }
}
