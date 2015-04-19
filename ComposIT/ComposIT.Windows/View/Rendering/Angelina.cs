using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ComposIT.View.Rendering
{
    /// <summary>
    /// Base class of the custom graphics engine
    /// </summary>
    public class Angelina
    {
        /// <summary>
        /// Starts the exploration of the Graphica, ehm graph of classes
        /// </summary>
        /// <param name="graph">Base graph</param>
        /// <param name="canvas">Base canvas</param>
        public void ExploreAfrica(SceneGraph graph, Canvas canvas)
        {
            graph.RescueAfricans(canvas);
        }
    }
}
