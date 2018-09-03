using System.Collections.Generic;
using Config;
using PlatformCore;

[AutoGenProtoBufRegister]
public class AccountInfo_Register
{

    public static void LoadFromDisk()
    {
        ConfigManager.Instance.loadRecordCompletedDic.Add(typeof(AccountInfo), null);
        AssetResource bin = PlatformCore.Core.AssetManager.GetResource(false , "/data/config/" + "AccountInfo.bin", AssetResource.DataTypeE.BYTES);
        bin.AddListener(PlatformCore.Event.COMPLETE, OnLoadedCompleted , 1);
        bin.AddListener(PlatformCore.Event.FAILED, OnLoadedCompleted, 1);
        bin.Load();

    }
    public static void OnLoadedCompleted(PlatformCore.Event e)
    {
        AssetResource bin = e.Target as AssetResource;
        byte[] data = bin.Data as byte[];
        ConfigManager.Instance.loadRecordCompletedDic[typeof(AccountInfo)] = data;
        ConfigManager.Instance.deserializeActionDic.Add(typeof(AccountInfo),Deserialize);
        ConfigManager.Instance.TestComplete();
    }

    public static void Deserialize()
    {
        byte[] data;
        Dictionary<uint, System.Object> dic;
        ConfigManager.Instance.configDataDic.TryGetValue(typeof(AccountInfo),out dic);
        if (ConfigManager.Instance.loadRecordCompletedDic.TryGetValue(typeof(AccountInfo), out data))
        {
            AccountInfo_ARRAY arr = AccountInfo_ARRAY.Parser.ParseFrom(data);
            for (int i = 0; i < arr.Items.Count; i++)
            {
                AccountInfo itemInfo = arr.Items[i];
                dic.Add(itemInfo.ID,itemInfo);
            }

        }

    }

}
