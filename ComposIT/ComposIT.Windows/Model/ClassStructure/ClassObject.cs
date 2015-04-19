using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ComposIT.Model.ClassStructure
{
    /// <summary>
    /// Represents a class modeled in the editor.
    /// Defines relationships to other classes, instances as well as properties of the class.
    /// </summary>
    public class ClassObject
    {
        /// <summary>
        /// A list of all object attributes (represented by InstanceObject) of the class
        /// </summary>
        public List<InstanceObject> Attributes;

        /// <summary>
        /// The id of this class.
        /// </summary>
        public string Id;

        /// <summary>
        /// The name of the class.
        /// </summary>
        public string ClassName {
            get
            {
                return _className;
            }
            set 
            {
                _className = value;
                ModelManager.GetInstance().ClassNameChanged(this);
                ModelManager.GetInstance().StartTranslation();
            }
        }
        private string _className;
        private static int _classCounter = 0;
       

        /// <summary>
        /// A list of instances that the class object owns.
        /// </summary>
        public List<InstanceObject> Instances;

        /// <summary>
        /// Dictionary of primitive attributes of the class with a string (as identifier, which
        /// is the attributes name) and a PrimitiveType which represents the typ of this
        /// primitive attribute.
        /// </summary>
        public Dictionary<string, PrimitiveTypes> PrimitiveAttributes;

        /// <summary>
        /// A list of all methods of the class defined by their name (string).
        /// </summary>
        public List<string> Methods;


        /// <summary>
        /// Constructor of the class object that initializes all attributes
        /// <param name="control">The control of the class which can be displayed</param>
        /// </summary>
        public ClassObject()
        {
            Attributes = new List<InstanceObject>();
            Instances = new List<InstanceObject>();
            PrimitiveAttributes = new Dictionary<string, PrimitiveTypes>();
            Methods = new List<string>();
            _className = "NewClass" + _classCounter++;
            Id = Guid.NewGuid().ToString();
        }


        /// <summary>
        /// Adds a primitive attribute to the class object.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="type">The tye of the attribute.</param>
        public void AddPrimitiveAttribute(string name, PrimitiveTypes type)
        {
            PrimitiveAttributes.Add(name, type);
            ModelManager.GetInstance().PrimitiveAttributesChanged(this);
            ModelManager.GetInstance().StartTranslation();
        }
        
        /// <summary>
        /// Removes a primitive attribute from the class object.
        /// Illegal calls are ignored.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public void RemovePrimitiveAttribute(string name)
        {
            if (!PrimitiveAttributes.ContainsKey(name))
            {
                return;
            }

            PrimitiveAttributes.Remove(name);
            ModelManager.GetInstance().PrimitiveAttributesChanged(this);
            ModelManager.GetInstance().StartTranslation();
        }

        /// <summary>
        /// Adds an attribute to the class object.
        /// </summary>
        /// <param name="iObject"> the attribute to be added </param>
        public void AddAttribute(InstanceObject iObject)
        {
            Attributes.Add(iObject);
            iObject.Parent = this;
            ModelManager.GetInstance().StartTranslation();

        }

        /// <summary>
        /// Removes an attribute from the class object.
        /// </summary>
        /// <param name="iObject"> the attribute to be removed </param>
        public void RemoveAttribute(InstanceObject iObject)
        {
            Attributes.Remove(iObject);
            ModelManager.GetInstance().StartTranslation();

        }
        /// <summary>
        /// Adds a method to the class object.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        public void AddMethod(string name)
        {
            Methods.Add(name);
            ModelManager.GetInstance().MethodsChanged(this);
            ModelManager.GetInstance().StartTranslation();
        }

        /// <summary>
        /// Removes a method from the class object.
        /// Illegal calls are ignored.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        public void RemoveMethod(string name)
        {
            if (!Methods.Contains(name))
            {
                return;
            }

            Methods.Remove(name);
            ModelManager.GetInstance().MethodsChanged(this);
            ModelManager.GetInstance().StartTranslation();
        }
    }
}
