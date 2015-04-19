using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.EventHandlers
{
    /// <summary>
    /// Event that will fire when a code property of a class has been changed
    /// </summary>
    public class ClassPropertyChangeEvent
    {
        /// <summary>
        /// The class that has been modified
        /// </summary>
        public ClassObject ChangedClass { get; set; }
    }
}
