using ComposIT.Model.ClassStructure;
using ComposIT.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace ComposIT.View.Rendering
{
    public class SceneNode
    {
        public Point AbsolutePosition { get; private set; }

        public double AbsoluteScale { get; private set; }

        public Point Position { get; private set; }

        public Point Size { get; private set; }

        public double Scale { get; private set; }

        public Shape Face { get; private set; }

        public List<SceneNode> Children { get; private set; }

        public SceneNode Parent { get; set; }

        public InstanceObject Instance { get; private set; }

        public SceneNode(List<InstanceObject> baseInstances)
        {
            Face = new Rectangle();
            Face.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Position = new Point(0, 0);
            Scale = 1;
            Size = new Point(0, 0);
            Children = new List<SceneNode>();
            foreach (InstanceObject inst in baseInstances)
            {
                Children.Add(new SceneNode(inst, 1));
            }
        }

        public SceneNode(InstanceObject instance, int depth)
        {
            switch (instance.ClassObject.VisualShape)
            {
                case VisualShape.Ellipse: Face = new Ellipse();
                    break;
                case VisualShape.Rectangle: Face = new Rectangle();
                    break;
            }
            Face.Fill = instance.ClassObject.Brush;
            Position = instance.Position;
            Scale = instance.Scale;
            Size = new Point()
            {
                X = instance.ClassObject.Size.X,
                Y = instance.ClassObject.Size.Y
            };
            Children = new List<SceneNode>();
            foreach (InstanceObject inst in instance.ClassObject.Attributes)
            {
                if (depth > 20) break;
                Children.Add(new SceneNode(inst, depth + 1));
            }
            Face.Tapped += (object sender, TappedRoutedEventArgs args) => {
                OnTapped(sender, args);
            };

            Instance = instance;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs args)
        {
            EditPage.Instance.ToolState.OnClick(sender, args);
        }

        public void AdoptChildren(SceneNode parent)
        {
            Face.Tag = this;
            // if parent is root element
            if (parent == this)
            {
                AbsolutePosition = new Point()
                {
                    X = Position.X * Scale,
                    Y = Position.Y * Scale
                };
                AbsoluteScale = Scale;
            }
            else
            {
                Parent = parent;
                AbsoluteScale = Parent.AbsoluteScale * Scale;
                AbsolutePosition = new Point()
                {
                    X = Parent.AbsolutePosition.X + Position.X * AbsoluteScale,
                    Y = Parent.AbsolutePosition.Y + Position.Y * AbsoluteScale
                };
                
            }
                            
            foreach (SceneNode child in Children)
            {
                child.AdoptChildren(this);
            }

        }

        public void Emmigrate(Canvas canvas)
        {
            Face.Margin = new Windows.UI.Xaml.Thickness(AbsolutePosition.X, AbsolutePosition.Y, 0, 0);
            Face.Width = AbsoluteScale * Size.X;
            Face.Height = AbsoluteScale * Size.Y;
            canvas.Children.Add(Face);
            foreach (SceneNode child in Children)
            {
                child.Emmigrate(canvas);
            }
        }
    }
}
