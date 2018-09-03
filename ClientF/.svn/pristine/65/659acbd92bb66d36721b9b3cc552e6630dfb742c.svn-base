using UnityEngine;
using GameILruntime.Core;
using NetProto;
using Google.Protobuf;

public class NetReceiveCallBackImpl: NetReceiveCallBackBase
{
    public static NetReceiveCallBackImpl Instance;
    static NetReceiveCallBackImpl()
    {
        Instance = new NetReceiveCallBackImpl();
    }


   public override void OnReceiveTestNetData(IMessage data)
    {
        var Data = data as Test;
        Dumper.Dump(Data);
       // NetManager.Instance.SendNetDataToServer(data);
       // EventManager.Instance.EmitNetEvent(44444,data);
    }
 
}
