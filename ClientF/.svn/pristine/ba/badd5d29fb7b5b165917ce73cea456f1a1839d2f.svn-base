using System;
using System.Reflection;
using PlatformCore;
using UnityEngine ;

namespace GameILruntime.Core
{
    public class InjectBase : EventDispatcher
    {
        /// <summary>
        /// 对有属性的数据进行注入
        /// </summary>
        protected virtual void inject()
        {
            Type t = this.GetType();
            FieldInfo[] fLst = t.GetFields();
            foreach (FieldInfo f in fLst)
            {
                object[] attLst = f.GetCustomAttributes(true);
                foreach (object att in attLst)
                {
                    if (att is ViewAttribute == true)
                    {//对应的是view
                        //创建view实例
                        object obj = Activator.CreateInstance(f.FieldType);
                        f.SetValue(this, obj);
                        SetView(obj as UIView);
                    }
                    if (att is UIComponentAttribute == true)
                    {//绑定UI控件
                        SetUIComponent(f, att as UIComponentAttribute);
                    }
                }
            }
        }

        protected virtual void SetView(UIView view_) { }

        protected virtual void SetUIComponent(FieldInfo f_, UIComponentAttribute att_)
        {
        }
    }
}
