using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ComposIT.View.Rendering
{
    /// <summary>
    /// Base graph containing the instances that should be displayed as nodes
    /// </summary>
    public class SceneGraph
    {
        /// <summary>
        /// Root node of the graph
        /// </summary>
        public SceneNode Root { get; private set; }


        /// <summary>
        /// Constructor which will be initialized with the base instances of the class tree
        /// </summary>
        /// <param name="baseInstances">The top most instances</param>
        public SceneGraph(List<InstanceObject> baseInstances)
        {
            Root = new SceneNode(baseInstances);

            Root.Parent = Root;
            Root.AdoptChildren(Root);
        }
        
        /// <summary>
        /// Starts the graph traversion
        /// </summary>
        /// <param name="canvas">The base canvas</param>
        public void RescueAfricans(Canvas canvas)
        {
            Root.Emmigrate(canvas);
        }

    }
}
