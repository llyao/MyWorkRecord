using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager
{
    public Dictionary<Type, Dictionary<uint, System.Object>> configDataDic = new Dictionary<Type, Dictionary<uint, object>>();
    /// <summary>
    /// 存储将要反序列的数据。
    /// </summary>
    public Dictionary<Type, byte[]> loadRecordCompletedDic = new Dictionary<Type, byte[]>();

    public Dictionary<Type, Action> deserializeActionDic = new Dictionary<Type, Action>();

    public static ConfigManager Instance;

    public Action OnLoadOneCompletedEvent { get; internal set; }

    static ConfigManager()
    {
        Instance = new ConfigManager();
    }
    // 定义T-》事件，事件自动生成，生成配置数据字典。

    public T GetInfo<T>(int id) where T : class
    {
        Dictionary<uint, System.Object> datas;
        var t = typeof(T);
        System.Object res;
        if (!configDataDic.TryGetValue(t, out datas))
        {
            configDataDic.Add(t, new Dictionary<uint, object>());
            Action act;
            if (deserializeActionDic.TryGetValue(t, out act))
            {
                act();
                if (!configDataDic.TryGetValue(t, out datas))
                {
                    Debug.LogError("not find key in:" + t.ToString() + id);

                }
            }
            else
            {
                Debug.LogError("not find serialize file byes" + t.ToString());
            }
        }

        if (datas.TryGetValue((uint)id, out res))
        {
            return res as T;
        }
        else
        {
            Debug.LogError("not find config" + t.ToString() + id);
        }
        T temp = default(T);
        return temp;
    }

    public void TestComplete()
    {
        if (ConfigManager.Instance.OnLoadOneCompletedEvent != null)
        {
            foreach (var item in loadRecordCompletedDic)
            {
                if (item.Value == null)
                {
                    return;
                }
            }
            ConfigManager.Instance.OnLoadOneCompletedEvent();
        }
    }

    public void Init()
	{
		//AccountInfo_Register.LoadFromDisk();
		
	
		AccountInfo_Register.LoadFromDisk();
	}
}