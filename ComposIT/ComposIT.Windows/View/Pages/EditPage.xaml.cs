using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ComposIT.View.ViewControls;
using ComposIT.View.ViewStates;
using ComposIT.Model.ClassStructure;
using ComposIT.View.Rendering;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ComposIT.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPage : Page
    {
        /// <summary>
        /// The currently focused class
        /// </summary>
        private DrawableClassObject _currentClass;
        /// <summary>
        /// Singleton saving the current page instance
        /// </summary>
        private static EditPage _instance;

        /// <summary>
        /// Saved the current state of the page according to the STATE design pattern
        /// </summary>
        private ToolState _toolState;
                 
        /// <summary>
        /// The panel containing the tool bar on the left
        /// </summary>
        public ToolPanel ToolPanel { get { return toolPanel; } }

        /// <summary>
        /// The panel containing the properties panel on the right-hand lower seid
        /// </summary>
        public PropertiesPanel PropertiesPanel { get { return propPanel; } }

        /// <summary>
        /// The panel containing the class gallery at the bottom 
        /// </summary>
        public ClassGallery ClassGallery { get { return classGallery; } }

        /// <summary>
        /// The current instance of this page (SINGLETON design pattern)
        /// </summary>
        public static EditPage Instance { get { return _instance ?? new EditPage(); } }

        /// <summary>
        /// Saved whether the Drawing Board is currently opened
        /// </summary>
        public bool IsDrawingBoardOpen { get; private set; }

        /// <summary>
        /// The current State of the Page (STATE design pattern)
        /// </summary>
        public ToolState ToolState 
        {
            get
            {
                return _toolState; 
                
            }
            set
            {
                if (_toolState != null)
                {
                    _toolState.Exit();
                }
                _toolState = value;
            } 
        }

        /// <summary>
        /// Manages access to the top canvas according to the current state of the drawing board
        /// </summary>
        public DrawingCanvas CurrentCanvas
        {
            get
            {
                if (IsDrawingBoardOpen)
                {
                    return DrawCanvas;
                }
                else
                {
                    return ClassCanvas;
                }

            }
        }


        /// <summary>
        /// Constructor which sets Event Handlers and design elements
        /// </summary>
        public EditPage()
        {
            this.InitializeComponent();
            DrawCanvas.PointerEntered += (sender, e) =>
            {
                Window.Current.CoreWindow.PointerCursor =
                    new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Cross, 1);

            };
            DrawCanvas.PointerExited += (sender, e) =>
            {
                Window.Current.CoreWindow.PointerCursor =
                    new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            };

            ClassCanvas.Fill = new SolidColorBrush(Colors.Wheat);
            DrawCanvas.Fill = new SolidColorBrush(Colors.DarkGray);

            this.Loaded += (sender, e) => { ToolState = new CreateClassState(); };

            IsDrawingBoardOpen = true;
            DrawCanvas.AddDrawingBoard();
            _instance = this;
        }


        /// <summary>
        /// Event Handler for a tap on the canvas
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Fired event</param>
        private void DrawCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ToolState.OnClick(sender, e);
        }

        /// <summary>
        /// Event Handler for a moved pointer
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Fired event</param>
        private void DrawCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            ToolState.OnPointerMoved(sender, e);
        }

        /// <summary>
        /// Opens the Drawing Board
        /// </summary>
        public void OpenDrawingBoardBegin()
        {
            if (IsDrawingBoardOpen) return;
            IsDrawingBoardOpen = true;
            OpenDrawingBoard.Begin();
            DrawCanvas.Tapped += DrawCanvas_Tapped;
        }

        /// <summary>
        /// Closes the Drawing Board
        /// </summary>
        public void CloseDrawingBoardBegin()
        {
            if (!IsDrawingBoardOpen) return;
            IsDrawingBoardOpen = false;
            CloseDrawingBoard.Begin();
            DrawCanvas.Tapped -= DrawCanvas_Tapped;
            
        }


        /// <summary>
        /// Redraws the Class view
        /// </summary>
        /// <param name="clObject">Main object of the class view</param>
        public void DrawClassView(DrawableClassObject clObject)
        {
       //     if (IsDrawingBoardOpen) CloseDrawingBoardBegin();
            ClassCanvas.DrawClass(clObject);

            _currentClass = clObject;
        }

        /// <summary>
        /// Redraws the Instance view
        /// </summary>
        public void DrawInstancesView()
        {
            DrawCanvas.DrawInstances();
        }

        /// <summary>
        /// Redraws the complete class tree
        /// </summary>
        public void Redraw()
        {
            ClassCanvas.Children.Clear();
            DrawInstancesView();
            classGallery.FillClasses();
            List<DrawableClassObject> classes = Model.ModelManager.GetInstance().GetClasses();
            if (_currentClass != null)
            {
                DrawClassView(_currentClass);
            }
            else
            {
                DrawEmptyClassView();
            }
        }

        /// <summary>
        /// Draws an empty Class view
        /// </summary>
        public void DrawEmptyClassView()
        {
            ClassCanvas.Children.Clear();
            CloseDrawingBoardBegin();
        }
    }
}
