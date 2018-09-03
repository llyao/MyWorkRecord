using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class AutoNetWorkListener
    {
        public static float TIME = 3.0f;
        protected static Action ReachableAction;
        public static void Start(Action action)
        {
            ReachableAction = action;
            Core.Timer.CallLater(CheckNetWork, TIME);
        }

        private static void CheckNetWork()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                Action oldAction = ReachableAction;
                if (oldAction != null)
                {
                    ReachableAction = null;
                    oldAction();
                }
            }
            else
            {
                Core.Timer.CallLater(CheckNetWork, TIME);
            }
        }
    }
}
