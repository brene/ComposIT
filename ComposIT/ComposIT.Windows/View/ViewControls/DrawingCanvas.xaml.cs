using ComposIT.Model;
using ComposIT.Model.ClassStructure;
using ComposIT.View.Pages;
using ComposIT.View.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ComposIT.View.ViewControls
{
    public sealed partial class DrawingCanvas : UserControl
    {
        public const int MAX_LEVELS = 20;

        private SceneGraph _graph;


        public void AddDrawingBoard()
        {
            DrawingBoardTextBlock.Visibility = Visibility.Visible;
        }
 
        public DrawingCanvas()
        {
            this.InitializeComponent();

            DrawInstances();

            RightTapped += DrawingCanvas_RightTapped;
        }

        private void DrawingCanvas_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            if (EditPage.Instance.IsDrawingBoardOpen)
            {
                EditPage.Instance.CloseDrawingBoardBegin();
            }
            else
            {
                EditPage.Instance.OpenDrawingBoardBegin();
            }
        }

        public UIElementCollection Children 
        { 
            get { return MainCanvas.Children; }
        }

        public Brush Fill
        {
            set { MainCanvas.Background = value; }
        }

        public void AddUIControl(UIElement control)
        {
            MainCanvas.Children.Add(control);
        }

        public void DrawInstances()
        {
            List<InstanceObject> instances = Model.ModelManager.GetInstance().GetBaseInstances();
            //IModel model = new Model.HardCodedHardWoodModelBecauseLongNamesAreAwesome();
            //List<InstanceObject> instances = model.GetBaseInstances();
            MainCanvas.Children.Clear();
            _graph = new SceneGraph(instances);
            new Angelina().ExploreAfrica(_graph, MainCanvas);
        }

        public void DrawClass(DrawableClassObject clObject)
        {
            MainCanvas.Children.Clear();
            InstanceObject instance = clObject.Instances.First();
            List<InstanceObject> instances = new List<InstanceObject>();
            instances.Add(instance);
            new Angelina().ExploreAfrica(new SceneGraph(instances), MainCanvas);
        }
    }
}
