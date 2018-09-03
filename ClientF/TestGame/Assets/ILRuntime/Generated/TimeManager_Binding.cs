using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class TimeManager_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(TimeManager);

            field = type.GetField("Instance", flag);
            app.RegisterCLRFieldGetter(field, get_Instance_0);
            app.RegisterCLRFieldSetter(field, set_Instance_0);
            field = type.GetField("LateUpdateAction", flag);
            app.RegisterCLRFieldGetter(field, get_LateUpdateAction_1);
            app.RegisterCLRFieldSetter(field, set_LateUpdateAction_1);


        }



        static object get_Instance_0(ref object o)
        {
            return TimeManager.Instance;
        }
        static void set_Instance_0(ref object o, object v)
        {
            TimeManager.Instance = (TimeManager)v;
        }
        static object get_LateUpdateAction_1(ref object o)
        {
            return ((TimeManager)o).LateUpdateAction;
        }
        static void set_LateUpdateAction_1(ref object o, object v)
        {
            ((TimeManager)o).LateUpdateAction = (System.Action)v;
        }


    }
}
