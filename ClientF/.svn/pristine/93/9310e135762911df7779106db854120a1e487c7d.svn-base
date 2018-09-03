using NetProto;
using NetProto;
﻿using UnityEngine;
using Google.Protobuf;

public class NetReceiveCallBackBase
{

	public virtual void OnReceiveTestNetData(IMessage data)
    {
        var Data = data as Test;
        Debug.LogWarning("OnReceiveTestNetData not impl in NetReceiveCallBackImpl.cs");
    }
	public virtual void OnReceiveTestMNetData(IMessage data)
    {
        var Data = data as TestM;
        Debug.LogWarning("OnReceiveTestMNetData not impl in NetReceiveCallBackImpl.cs");
    }

}
