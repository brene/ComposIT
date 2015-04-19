using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.EventHandlers
{
    /// <summary>
    /// Event that will be fired when the translation text has been changed
    /// </summary>
    public class TranslationChangedEvent
    {
        /// <summary>
        /// The target language
        /// </summary>
        public string language;
        /// <summary>
        /// The newly translated text
        /// </summary>
        public string text;
    }
}
