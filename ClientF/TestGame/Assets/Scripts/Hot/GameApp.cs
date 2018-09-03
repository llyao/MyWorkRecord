using System.Collections;
using System.Collections.Generic;
using PlatformCore;
using UnityEngine;

public class GameApp : CoreApp
{
    protected override void OnInitialCompleted()
    {
        //
        GameILruntime.Core.UIManager.Init();

        //加载测试场景
        Core.SceneManager.AddListener(PlatformCore.Event.COMPLETE, OnLoadSceneComplete, 1);
        Core.SceneManager.Load("/all/scenes/1");
    }
}
