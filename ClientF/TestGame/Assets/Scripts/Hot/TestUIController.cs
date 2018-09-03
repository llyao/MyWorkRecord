using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameILruntime.Core;
public class TestUIController : UIMediator
{

   public TestUIController()
    {
    }

    public override void OnLoadedCompleted(bool success_)
    {
        
    }

    public void TestUpdate()
    {
        Debug.Log("TestUpdate999999999999999999999");
        //TimeManager.Instance.AddInvokeRepeating-= TestUpdate;

    }
    public void TestTimer()
    {
        Debug.Log("TestTimer--------------"+Time.time.ToString());
    }
}
