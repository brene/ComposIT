using ComposIT.Model.ClassStructure;
using ComposIT.Model.EventHandlers;
using ComposIT.Model.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ComposIT.Model
{
    /// <summary>
    /// Facade of the subsystem Model that implements the facade interface IModel
    /// </summary>
    public partial class ModelManager : IModel
    {
        /// <summary>
        /// Root object of the class tree (can't be drawn)
        /// </summary>
        private MainClassObject _rootObject;
        /// <summary>
        /// Contains the current instance of the class to allow static access
        /// </summary>
        private static ModelManager _instance;
        /// <summary>
        /// The translator translating the class tree to a code file
        /// </summary>
        private Translator _translator;
        /// <summary>
        /// A list of all classes that have been created
        /// </summary>
        private List<DrawableClassObject> _classes;


        /// <summary>
        /// Constructor of a new ModelManager that initializes all attributes
        /// </summary>
        public ModelManager()
        {
            _instance = this;
            _rootObject = new MainClassObject();
            _translator = new Translator();
            _classes = new List<DrawableClassObject>();
        }


        public static ModelManager GetInstance() {
            return _instance ?? new ModelManager();
        }

        public DrawableClassObject CreateNewClass(Point size, Brush brush, VisualShape shape, Point position, double scale)
        {
            DrawableClassObject newClass = new DrawableClassObject(size, brush, shape);
            _classes.Add(newClass);
            InstanceObject newInstance = new InstanceObject(newClass, position, scale);
            newClass.Instances.Add(newInstance);
            StartTranslation();
            return newClass;
        }

        public InstanceObject CreateInstanceOfClass(DrawableClassObject classObject, Point position, double scale)
        {
            InstanceObject newInstance = new InstanceObject(classObject, new Point(position.X, position.Y), scale);
            classObject.Instances.Add(newInstance);
 
            // Check for collisions?
            AddInstanceToRoot(newInstance);

            StartTranslation();

            return newInstance;
        }

        public void AddInstanceToClass(InstanceObject attribute, InstanceObject parent)
        {
            parent.ClassObject.AddAttribute(attribute);
            if (_rootObject.Attributes.Contains(attribute))
            {
                _rootObject.RemoveAttribute(attribute);
            }
            StartTranslation();
        }

        public void StartTranslation()
        {
            _translator.Translate(_rootObject);
        }

        public List<DrawableClassObject> GetClasses()
        {
            return _classes;
        }

        public List<string> GetAllLanguages()
        {
            return _translator.GetLanguages();
        }

        public void SetCurrentLanguage(string language)
        {
            _translator.SetLanguage(language);
            StartTranslation();
        }

        public List<InstanceObject> GetBaseInstances()
        {
            return _rootObject.Attributes;
        }

        public void AddInstanceToRoot(InstanceObject instance)
        {
            _rootObject.AddAttribute(instance);
        }

        public void ExportCode(StorageFolder folder)
        {
            _translator.Export(folder);
        }

        public void RemoveClass(DrawableClassObject classObject)
        {
            if (_classes.Contains(classObject)) {
                _classes.Remove(classObject);
            }
            foreach (InstanceObject instance in classObject.Instances) {
                if (instance.Parent == instance.ClassObject)
                {
                    Debug.WriteLine("Instance is standalone. Should not be happening");
                    break;
                }
                instance.Parent.RemoveAttribute(instance);
            }
            StartTranslation();
        }
    }
}
