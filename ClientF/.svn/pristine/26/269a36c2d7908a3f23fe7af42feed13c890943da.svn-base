using UnityEngine;
using GameILruntime.Core;

public class CanvasTestUIMediator : UIMediator
{
    [View]
    public CanvasTestUIView view;

	public CanvasTestUIMediator()
	{
	}
	public override void OnLoadedCompleted(bool success_)
	{
		//view = go.AddComponent<CanvasTestUIView>();
        // 加载完成，逻辑代码写在下面
        TimeManager.Instance.LateUpdateAction += update;
       //TimeManager.Instance.AddInvoke(1.0f, update);
    }
    public void update()
    {
        Debug.Log("444444444444");
    }
}