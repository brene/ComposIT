using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace ComposIT.View.ViewStates
{
    public abstract class ToolState
    {
        public ToolState()
        {

        }

        public virtual void OnClick(object sender, TappedRoutedEventArgs e)
        {

        }

        public virtual void OnRelease(object sender, RoutedEventArgs e)
        {

        }

        public virtual void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {

        }

        public virtual void Exit()
        {
            
        }
    }
}
