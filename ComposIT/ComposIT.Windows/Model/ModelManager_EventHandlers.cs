using ComposIT.Model.ClassStructure;
using ComposIT.Model.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model
{
    public partial class ModelManager
    {
        // List collecting all event handlers of the model
        private List<ChangeHandlers.OnInstancePropertyChangedHandler> _attributeNameChangedHandlers 
            = new List<ChangeHandlers.OnInstancePropertyChangedHandler>();
        private List<ChangeHandlers.OnClassPropertyChangedHandler> _classNameChangedHandlers
            = new List<ChangeHandlers.OnClassPropertyChangedHandler>();
        private List<ChangeHandlers.OnClassPropertyChangedHandler> _primitiveAttributesChangedHandlers
            = new List<ChangeHandlers.OnClassPropertyChangedHandler>();
        private List<ChangeHandlers.OnClassPropertyChangedHandler> _methodsChangedHandlers
            = new List<ChangeHandlers.OnClassPropertyChangedHandler>();
        private List<ChangeHandlers.OnClassStyleChangedHandler> _classStyleChangedHandlers
            = new List<ChangeHandlers.OnClassStyleChangedHandler>();
        private List<ChangeHandlers.OnTranslationChangedHandler> _translationChangedHandlers
            = new List<ChangeHandlers.OnTranslationChangedHandler>();


        public void AddOnAttributeNameChangedHandler(ChangeHandlers.OnInstancePropertyChangedHandler handler)
        {
            _attributeNameChangedHandlers.Add(handler);
        }

        public void AddOnClassNameChangedHandler(ChangeHandlers.OnClassPropertyChangedHandler handler)
        {
            _classNameChangedHandlers.Add(handler);
        }

        public void AddOnPrimitiveAttributesChangedHandler(ChangeHandlers.OnClassPropertyChangedHandler handler)
        {
            _primitiveAttributesChangedHandlers.Add(handler);
        }

        public void AddOnMethodsChangedHandler(ChangeHandlers.OnClassPropertyChangedHandler handler)
        {
            _methodsChangedHandlers.Add(handler);
        }

        public void AddOnClassStyleChangedHandler(ChangeHandlers.OnClassStyleChangedHandler handler)
        {
            _classStyleChangedHandlers.Add(handler);
        }

        public void AddOnTranslationChangedHandler(ChangeHandlers.OnTranslationChangedHandler handler)
        {
            _translationChangedHandlers.Add(handler);
        }


        public void AttributeNameChanged(InstanceObject changedObject)
        {
           InstancePropertyChangeEvent newEvent = new InstancePropertyChangeEvent() {
               ChangedInstance = changedObject
            };
            foreach (var handler in _attributeNameChangedHandlers) 
            {
                if (handler == null) continue;
                handler(newEvent);
            }
        }

        public void ClassNameChanged(ClassObject changedObject)
        {
            ClassPropertyChangeEvent newEvent = new ClassPropertyChangeEvent()
            {
                ChangedClass = changedObject
            };
            foreach (var handler in _classNameChangedHandlers)
            {
                if (handler == null) continue;
                handler(newEvent);
            }
        }

        public void PrimitiveAttributesChanged(ClassObject changedObject)
        {
            ClassPropertyChangeEvent newEvent = new ClassPropertyChangeEvent()
            {
                ChangedClass = changedObject
            };
            foreach (var handler in _primitiveAttributesChangedHandlers)
            {
                if (handler == null) continue;
                handler(newEvent);
            }
        }

        public void MethodsChanged(ClassObject changedObject)
        {
            ClassPropertyChangeEvent newEvent = new ClassPropertyChangeEvent()
            {
                ChangedClass = changedObject
            };
            foreach (var handler in _methodsChangedHandlers)
            {
                if (handler == null) continue;
                handler(newEvent);
            }
        }

        public void ClassStyleChanged(ClassObject changedObject)
        {
            ClassStyleChangeEvent newEvent = new ClassStyleChangeEvent()
            {
                ChangedClass = changedObject
            };
            foreach (var handler in _classStyleChangedHandlers)
            {
                if (handler == null) continue;
                handler(newEvent);
            }
        }

        public void TranslationChanged(string language, string text)
        {
            TranslationChangedEvent newEvent = new TranslationChangedEvent()
            {
                language = language,
                text = text
            };
            foreach (var handler in _translationChangedHandlers)
            {
                if (handler == null) continue;
                handler(newEvent);
            }
        }
    }
}
