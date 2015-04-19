using ComposIT.View.ViewControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace ComposIT.Model.ClassStructure
{
    /// <summary>
    /// Represents an instance of a ClassObject.
    /// </summary>
    public class InstanceObject 
    {
        /// <summary>
        /// The Instance Object is an "instance" of this ClassObject.
        /// </summary>
        public DrawableClassObject ClassObject { get; private set; }

        /// <summary>
        /// Keeps track of the parent object of the current Instance. Will be set to "this" per default
        /// </summary>
        public ClassObject Parent { get; set; }

        /// <summary>
        /// The position in the View relative to the class this instance is contained in.
        /// </summary>
        public Point Position {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        private Point _position;

        /// <summary>
        /// The scale of the instance in the View relative to the parent
        /// </summary>
        public double Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }
        private double _scale;

        /// <summary>
        /// The name of the instance.
        /// </summary>
        public string AttributeName {
            get
            {
                return _attributeName;
            }
            set 
            {
                _attributeName = value;
                ModelManager.GetInstance().AttributeNameChanged(this);
                ModelManager.GetInstance().StartTranslation();
            }
        }
        private string _attributeName;


        /// <summary>
        /// Constructor which sets the attributes
        /// </summary>
        /// <param name="classObject">The class type of this instance</param>
        /// <param name="position">The position on the canvas</param>
        /// <param name="control">The control representing this instance on the canvas</param>
        public InstanceObject(DrawableClassObject classObject, Point position, double scale)
        {
            Parent = classObject;
            ClassObject = classObject;
            Position = position;
            Scale = scale;
        }
    }
}
