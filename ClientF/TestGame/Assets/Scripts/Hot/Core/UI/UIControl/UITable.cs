using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Event = PlatformCore.Event;

namespace GameILruntime.Core
{
    public class UITable:UISkin
    {
	    public const string EVENT_TAB_SELECTED = "event_tab_selected";
	    [UIComponent("")] public UIRadioGroup RadioGroup;
	    private List<UIPanel> panelList;
	    private int currentIndex;

		protected override void bindComponent()
	    {
		    base.bindComponent();
			panelList = new List<UIPanel>();
		}

	    protected override void addEvent()
	    {
		    base.addEvent();
		    RadioGroup.AddListener(UIRadioGroup.EVENT_RADIO_GROUP_SELECTED, OnClickButton , 0);

		}

	    private void OnClickButton(Event e)
	    {
		    var button = e.Target as UIButton;

		    var index = (int)e.Data;

			if (index != -1)
		    {
			    CurrentIndex = index;
		    }

		    DispatchEvent(EVENT_TAB_SELECTED, index);
	    }

	    protected override void removeEvent()
	    {
		    base.removeEvent();
		    RadioGroup.RemoveListener(UIRadioGroup.EVENT_RADIO_GROUP_SELECTED, OnClickButton);
		}

	    public void SetPanel(int index, UIPanel panel)
	    {
			if(index >= panelList.Count) { 

				for(int i = 0; i <= index; i++)
				{
					if(i >= panelList.Count)
					{
						panelList.Add(null);
					}
				}
			}

		    panelList[index] = panel;
		    panel.Active = false;

			CurrentIndex = currentIndex;
	    }

	    private int CurrentIndex
	    {
		    get
		    {
			    return currentIndex;
		    }

		    set
		    {
			    panelList[currentIndex].Active = false;
			    currentIndex = value;
			    panelList[currentIndex].Active = true;

			}
	    }

	    public void SelectPanel(uint index)
	    {
		    RadioGroup.SetSelectedIndex(index);

	    }
	}
}
