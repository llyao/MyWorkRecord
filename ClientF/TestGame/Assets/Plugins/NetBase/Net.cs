
using System;
using UnityEngine;
public class Net {


    public static Net Instance;
    private string ip;
    private UInt16 port;
    private KcpProject.UDPRandomCon.UdpSocket client;

    private Action<byte[]> receCallback;
    static Net()
    {
        Instance = new Net();

    }

    void StartConnect(string host, UInt16 port)
    {

        client = null;

        // 创建一个实例
        client = new KcpProject.UDPRandomCon.UdpSocket((byte[] buf) =>
        {
            receCallback(buf);
        });

        // 绑定端口
        client.Connect(host, port);

        // 发送消息
        client.Send("Hello KCP.");

    }
   public void SetIPAndPortConnect(string _ip, UInt16 _port)
    {
        ip = _ip;
        port = _port;
        StartConnect(ip, port);
    }
    public void SetReceCallback(Action<byte[]> _receCallback)
    {
        receCallback = _receCallback;
    }
   public void Update()
    {

        client.Update();
    }
    public void SendNetMessage(byte[] data)
    {
        if(data==null||data.Length==0)
        {
            Debug.Log("Send to Server Data is null or length==0,no send!!!");
            return;
        }
        client.Send(data);
    }
}
