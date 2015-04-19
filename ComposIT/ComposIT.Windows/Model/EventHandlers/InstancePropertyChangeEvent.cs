using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.EventHandlers
{
    /// <summary>
    /// Event that will fire when a code property of an Instance has been changed
    /// </summary>
    public class InstancePropertyChangeEvent
    {
        /// <summary>
        /// The instance object that has been modified
        /// </summary>
        public InstanceObject ChangedInstance { get; set; }
    }
}
