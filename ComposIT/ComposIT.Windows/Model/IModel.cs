using ComposIT.Model.ClassStructure;
using ComposIT.Model.EventHandlers;
using System;
using System.Collections.Generic;
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
    /// Facade interface of the subsystem Model that defines all methods and event handlers
    /// </summary>
    public interface IModel
    {

#region Interface methods

        /// <summary>
        /// Creates a new ClassObject in the model with the given style
        /// </summary>
        /// <param name="xSize">Horizontal size of the rectangle</param>
        /// <param name="ySize">Vertical size of the rectangle</param>
        /// <param name="brush">Color information</param>
        /// <param name="control">The control which displayes instances of the class</param>
        /// <returns>The newly created object</returns>
        DrawableClassObject CreateNewClass(Point size, Brush brush, VisualShape shape, Point position, double scale);

        /// <summary>
        /// Creates a new InstanceObject in the model with the given data
        /// </summary>
        /// <param name="classObject">Class type of the instance</param>
        /// <param name="position">Position relative to the class it's contained in</param>
        /// <param name="scale">The scale of the grid</param>
        /// <param name="grid">The grid displaying and positioning the visuals of the instance</param>
        /// <returns>The newly created object</returns>
        InstanceObject CreateInstanceOfClass(DrawableClassObject classObject, Point position, double scale);

        /// <summary>
        /// Creates a "attribute" relation between two instances on the canvas 
        /// (which will translate to a relation between their classes)
        /// </summary>
        /// <param name="attribute">The object which will be the attribute</param>
        /// <param name="parent">The object which will be the parent</param>
        void AddInstanceToClass(InstanceObject attribute, InstanceObject parent);

        /// <summary>
        /// Puts an instance into the root of the class tree
        /// </summary>
        /// <param name="instance">The object which will be added</param>
        void AddInstanceToRoot(InstanceObject instance);

        /// <summary>
        /// Removes a class from the internal class gallery (won't check that it isnt contained in the tree)
        /// </summary>
        /// <param name="classObject"></param>
        void RemoveClass(DrawableClassObject classObject);

        /// <summary>
        /// Can be called to redo the code translation of the current object structure
        /// </summary>
        void StartTranslation();

        /// <summary>
        /// Returns a list of all classes (ClassObjects) that have been created
        /// </summary>
        /// <returns>A list of all classes</returns>
        List<DrawableClassObject> GetClasses();

        /// <summary>
        /// Returns all instances of objects on the top layer of the canvas
        /// </summary>
        /// <returns>A list of the base instances</returns>
        List<InstanceObject> GetBaseInstances();

        /// <summary>
        /// Returns a list of all supported languages into which the class tree can be translates
        /// </summary>
        /// <returns>A list of all languages</returns>
        List<string> GetAllLanguages();

        /// <summary>
        /// Tells the model (and translator) to translate the class structure into the given language
        /// </summary>
        /// <param name="language">Identifier of the chosen language</param>
        void SetCurrentLanguage(string language);

        /// <summary>
        /// Exports the code to a predefined location
        /// </summary>
        void ExportCode(StorageFolder folder);

#endregion


#region EventHandlers

        /// <summary>
        /// Adds an event handler to the model that will be called whenever the attribute name of an InstanceObject has been changed
        /// </summary>
        /// <param name="handler">The handler that will be called</param>
        void AddOnAttributeNameChangedHandler(ChangeHandlers.OnInstancePropertyChangedHandler handler);

        /// <summary>
        /// Adds an event handler to the model that will be called whenever the class name of a ClassObject has been changed
        /// </summary>
        /// <param name="handler">The handler that will be called</param>
        void AddOnClassNameChangedHandler(ChangeHandlers.OnClassPropertyChangedHandler handler);

        /// <summary>
        /// Adds an event handler to the model that will be called whenever the primitive attributes of a ClassObject have been changed
        /// </summary>
        /// <param name="handler">The handler that will be called</param>
        void AddOnPrimitiveAttributesChangedHandler(ChangeHandlers.OnClassPropertyChangedHandler handler);

        /// <summary>
        /// Adds an event handler to the model that will be called whenever the methods of a ClassObject have been changed
        /// </summary>
        /// <param name="handler">The handler that will be called</param>
        void AddOnMethodsChangedHandler(ChangeHandlers.OnClassPropertyChangedHandler handler);

        /// <summary>
        /// Adds an event handler to the model that will be called whenever the display style of a ClassObject has been changed 
        /// (so that all shown instances can be updated)
        /// </summary>
        /// <param name="handler">The handler that will be called</param>
        void AddOnClassStyleChangedHandler(ChangeHandlers.OnClassStyleChangedHandler handler);

        /// <summary>
        /// Adds an event handler to the model that will be called whenever the translation of the class structure has been changed
        /// </summary>
        /// <param name="handler">The handler that will be called</param>
        void AddOnTranslationChangedHandler(ChangeHandlers.OnTranslationChangedHandler handler);

#endregion
    }
}
