using GameILruntime.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial  class NetManager  {

    /// <summary>
    /// 注册所有网络回调消息
    /// </summary>
    public void RegisterReceiveCallBack()
    {

		Id2ClassNames.Add(101, "NetProto.Test");
        class2Id.Add("Test", 101);
        RegisterNetEvent(101, NetReceiveCallBackImpl.Instance.OnReceiveTestNetData);
		Id2ClassNames.Add(100, "NetProto.TestM");
        class2Id.Add("TestM", 100);
        RegisterNetEvent(100, NetReceiveCallBackImpl.Instance.OnReceiveTestMNetData);
    }
    public void CancelReceiveCallBack()
    {
		CancelNetEvent(101, NetReceiveCallBackImpl.Instance.OnReceiveTestNetData);
		CancelNetEvent(100, NetReceiveCallBackImpl.Instance.OnReceiveTestMNetData);
    }
}
