using ComposIT.Model.ClassStructure;
using ComposIT.View.Pages;
using ComposIT.View.Rendering;
using ComposIT.View.ViewControls.ImageButtons;
using ComposIT.View.ViewControls.ToolButtons;
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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ComposIT.View.ViewControls
{
    public sealed partial class ClassGallery : UserControl
    {
        private List<GalleryIcon> _icons = new List<GalleryIcon>();
        public ClassGallery()
        {
            this.InitializeComponent();
        }

        public void FillClasses()
        {



            List<DrawableClassObject> classes = Model.ModelManager.GetInstance().GetClasses();
            _icons = new List<GalleryIcon>();
            PreviewGridView.Items.Clear();
            foreach (DrawableClassObject cl in classes)
            {
                InstanceObject instance = cl.Instances.First();
                List<InstanceObject> instances = new List<InstanceObject>();
                instances.Add(instance);
                Canvas canvas = new Canvas();
                new Angelina().ExploreAfrica(new SceneGraph(instances), canvas);
                canvas.RenderTransform = new ScaleTransform() {ScaleX = 0.1, ScaleY = 0.1};
                canvas.HorizontalAlignment = HorizontalAlignment.Center;
                GalleryIcon icon = new GalleryIcon(this, canvas, cl);
                _icons.Add(icon);
                PreviewGridView.Items.Add(icon);
            }
            
        }

        public void SelectGalleryIcon(GalleryIcon selectedIcon)
        {
            foreach (GalleryIcon icon in _icons)
            {
                if (icon == selectedIcon)
                {
                    icon.Selected2 = true;
                }
                else
                {
                    icon.Selected2 = false;
                }
            }
        }
    }
}
