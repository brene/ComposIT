using ComposIT.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.View.ViewStates
{
    public class AddMethodState : ToolState
    {
        public AddMethodState()
        {
            EditPage.Instance.ToolPanel.DisplayAddMethodButtonAsSelected();
        }
    }
}
