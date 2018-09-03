using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameILruntime.Core
{
    public class UIComponentAttribute:Attribute
    {
        public string PathName;
        public UIComponentAttribute(string pathName = "")
        {
            PathName = pathName;
        }
    }
}
