using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.EventHandlers
{
    /// <summary>
    /// Defines and encapsulates all custom EventHandler delegates
    /// </summary>
    public class ChangeHandlers
    {
        /// <summary>
        /// Delegate of an event handler that manages style changed in a class
        /// </summary>
        /// <param name="ev">The fired event</param>
        public delegate void OnClassStyleChangedHandler(ClassStyleChangeEvent ev);

        /// <summary>
        /// Delegate of an event handler that manages code changes in an instance
        /// </summary>
        /// <param name="ev">The fired event</param>
        public delegate void OnInstancePropertyChangedHandler(InstancePropertyChangeEvent ev);

        /// <summary>
        /// Delegate of an event handler that manages code changes in a class
        /// </summary>
        /// <param name="ev">The fired event</param>
        public delegate void OnClassPropertyChangedHandler(ClassPropertyChangeEvent ev);

        /// <summary>
        /// Delegate of an event handler that manages changes in the code translation
        /// </summary>
        /// <param name="ev">The fired event</param>
        public delegate void OnTranslationChangedHandler(TranslationChangedEvent ev);
    }
}
