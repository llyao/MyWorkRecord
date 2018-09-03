using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using GameILruntime.Core;
using System.IO;
using Google.Protobuf;

public partial class NetManager : MonoBehaviour
{

    public static NetManager Instance;
    byte[] bufferUInt16 = new byte[2];
    // byte[] bufferData = new byte[1024 * 1024];

    Dictionary<int, string> Id2ClassNames = new Dictionary<int, string>();
    Dictionary<string, UInt16> class2Id = new Dictionary<string, UInt16>();
    Dictionary<int, Action<IMessage>> receiveCallBacks = new Dictionary<int, Action<IMessage>>();

    // MemoryStream mem = new MemoryStream();

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        Net.Instance.SetReceCallback(ReceiveCallback);
        Net.Instance.SetIPAndPortConnect("127.0.0.1", 10086);
        RegisterReceiveCallBack();
    }
    void Reset()
    {
        CancelReceiveCallBack();
    }
    private void Update()
    {
        Net.Instance.Update();
    }

    void ReceiveCallback(byte[] res)
    {
        Debug.Log(res.Length);
        Debug.Log(System.Text.ASCIIEncoding.ASCII.GetString(res));


        Buffer.BlockCopy(res, 0, bufferUInt16, 0, 2);
        //Buffer.BlockCopy(res, 2, bufferData, 0, res.Length-2);
        if (BitConverter.IsLittleEndian) // 若为 小端模式
        {
            Array.Reverse(bufferUInt16); // 转换为 小端模式 编码             
        }
        var id = BitConverter.ToUInt16(bufferUInt16, 0);
        Debug.Log("收到网络消息id是:" + id);

        string className;
        if (Id2ClassNames.TryGetValue(id, out className))
        {
            ReceiveParseNetData(className, res,id);
        }

    }

    public void SendNetDataToServer(IMessage sendData)
    {
        var className = sendData.GetType().Name;
        UInt16 id;
        if (class2Id.TryGetValue(className, out id))
        {
           var data2= sendData.ToByteArray();
           var data1= System.BitConverter.GetBytes(id);
            if (BitConverter.IsLittleEndian) // 若为 小端模式
            {
                Array.Reverse(data1); // 转换为 小端模式 编码             
            }
            byte[] data = new byte[2 + data2.Length];
            Buffer.BlockCopy(data1, 0, data, 0, 2);
            Buffer.BlockCopy(data2, 0, data, 2, data2.Length-2);
            Net.Instance.SendNetMessage(data);

        }
    }


    void ReceiveParseNetData(string clazzName, byte[] res,int id)
    {
        Type type = Type.GetType(clazzName);
        var parser = type.GetProperty("Parser").GetValue(null, null);
        MessageParser mparser = (MessageParser)parser;
        IMessage data = mparser.ParseFrom(res, 2, res.Length - 2);
        Action<IMessage> callback;
       if(receiveCallBacks.TryGetValue(id,out callback))
        {
            callback(data);
        }
    }
    void RegisterNetEvent(int id,Action<IMessage>callBack)
    {
        receiveCallBacks.Add(id,callBack);
    }
    void CancelNetEvent(int id, Action<IMessage> callBack)
    {
        receiveCallBacks.Remove(id);

    }


}
