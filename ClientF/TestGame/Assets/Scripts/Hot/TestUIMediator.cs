using System.Collections.Generic;
using UnityEngine;
using GameILruntime.Core;
using ILRuntime.Runtime;
using PlatformCore;
using Event = PlatformCore.Event;

public class TestUIMediator : UIMediator
{
    [View]
    public TestUIView view;

    public TestUIMediator()
    {
    }
    public override void OnLoadedCompleted(bool success_)
    {
        Debug.Log("OnLoadedCompleted begin");
        view.button.AddListener(UIButton.EVENT_BUTTON_CLICK , OnClickButton, 0);
	    view.RadioGroup.AddListener(UIRadioGroup.EVENT_RADIO_GROUP_SELECTED, OnSelected, 0);

        Debug.Log("PageList:" + view.PageList.ToString());

        view.PageList.AutoCell = UIPageList.AutoCellLength.HEIGHT;
		view.PageList.Count = 15;
		view.PageList.SetLineWH(4,3);

		var list = new List<UIPageListCellData>();
	    list.Add(new UIPageListCellData(){ LabelText = "aaaaa"});
	    list.Add(new UIPageListCellData() { LabelText = "bbbb" });

		view.PageList.SetCellList(list);
        Debug.Log("OnLoadedCompleted end");
    }

	private void OnSelected(PlatformCore.Event e_)
	{
		var index = (int)e_.Data;
		//view.RadioGroup.SetLabel((uint)index, "aaaa");
	}

	protected void OnClickButton(PlatformCore.Event e_)
    {
        Debug.Log("OnClickButton");
        view.button.SetIcon("/data/Image/btn_support.png");

        AssetResource ar = Core.AssetManager.GetResource(true, "/all/avatar/cube", AssetResource.DataTypeE.ASSETBUNDLE);
        ar.AddListener(Event.COMPLETE , (Event e) =>
        {
            GameObject go = ar.InstantiatePrefab() as GameObject;
            go.name = "TTTTTTT";
        } , 1);
        ar.Load();
    }

}
