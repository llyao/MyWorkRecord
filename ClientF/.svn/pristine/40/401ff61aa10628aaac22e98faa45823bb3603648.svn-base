using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{

    /// <summary>
    /// 整个游戏的最初配置文件
    /// </summary>
    public class CoreShellConfig : EventDispatcherMono, ICore
    {
        protected string mUpdateServerUrl = "";
        public string UpdateServerUrl { get { return mUpdateServerUrl; } }

        public CoreShellConfig()
        {
            
        }

        public void Load()
        {
            //先从本地读取更新服务器地址,暂时先写死
            mUpdateServerUrl = "http://10.10.10.200:8090";

            DispatchEvent(Event.COMPLETE, null);
        }
    }
}
