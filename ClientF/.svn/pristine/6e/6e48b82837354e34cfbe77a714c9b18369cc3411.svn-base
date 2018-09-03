//this file is auto created by QuickCode,you can edit it 
//do not need to care initialization of ui widget any more 
//------------------------------------------------------------------------------
/**
* @author :
* date    :
* purpose :
*/
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using GameILruntime.Core;
using Quick.UI;
using UnityEngine.UI;

public class TestUIView : UIView
{

    public TestUIView()
    {
        mResName = "CanvasTest";
    }

    #region UI Variable Statement 

    [UIComponent("CodePanel")]public Image image;

    [UIComponent("CodePanel/Title")]public Text title;

    [UIComponent("CodePanel/Button")]public UIButton button;

	[UIComponent("CodePanel/TG")] public UIRadioGroup RadioGroup;

	[UIComponent("CodePanel/PageList")] public UIPageList PageList;

	[UIComponent("CodePanel/Panel0")] public UIPanel Panel1;

	[UIComponent("CodePanel/Panel1")] public UIPanel Panel2;

	[UIComponent("CodePanel/Tab")] public UITable Tab;

	#endregion

	#region UI Variable Assignment 

	protected override void inject()
	{
        base.inject();
		PageList.SetCreateCell( CreateCell);
    }

	override protected void bindComponent()
    {

	    Tab.SetPanel(0, Panel1);
	    Tab.SetPanel(1, Panel2);
	}

	private TestItemCell CreateCell()
	{
		return new TestItemCell();
	}

    override protected void unbindComponent()
    {
        
    }

    #endregion
}
