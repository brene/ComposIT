using ComposIT.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.View.ViewStates
{
    public class AddPrimitiveDataTypeState : ToolState
    {
        public AddPrimitiveDataTypeState()
        {
            EditPage.Instance.ToolPanel.DisplayAddPrimitiveDataTypeButtonAsSelected();
        }
    }
}
