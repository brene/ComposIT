using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.EventHandlers
{
    /// <summary>
    /// Event that will be fired when style properties of a class have been changed
    /// </summary>
    public class ClassStyleChangeEvent
    {
        /// <summary>
        /// The class that has been modified
        /// </summary>
        public ClassObject ChangedClass { get; set; }
        /// <summary>
        /// The instances that need to be redrawn / updated
        /// </summary>
        public List<InstanceObject> Instances { get; set; }
    }
}
