using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace ComposIT.Model.ClassStructure
{
    /// <summary>
    /// Represents the root of the class tree
    /// </summary>
    public class MainClassObject : ClassObject
    {
        /// <summary>
        /// Background-color of the canvas
        /// </summary>
        public Brush BackgroundColor { get; set; }
    }
}
