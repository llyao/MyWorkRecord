using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameILruntime.Core
{
    public class UIRadioGroup:UISkin
    {
	    public const string EVENT_RADIO_GROUP_SELECTED = "event_radio_group_selected";

	    public const string RADIO_UNIT_NAME = "Toggle";

	    private uint Count = 0;
	    private uint selectedIndex;
	    private string[] checkLabelList;
	    private List<GameObject> unitList;

	    protected override void bindComponent()
	    {
		    base.bindComponent();

		    uint i = 0;
		    unitList = new List<GameObject>();

		    while (true)
		    {
			    GameObject go = FindGameObject(RADIO_UNIT_NAME + i.ToString());

			    if (go == null)
			    {
				    Count = i;
				    InitUnitData(Count);
				    break;
			    }

			    i++;
				unitList.Add(go);
		    }

		    SetSelectedIndex(0);
	    }

	    private void InitUnitData(uint count)
	    {
		    selectedIndex = 0;
		    checkLabelList = new string[Count];

		    for (int i = 0; i < Count; i++)
		    {
			    checkLabelList[i] = unitList[i].name;
		    }
	    }


	    protected override void unbindComponent()
	    {
		    base.unbindComponent();
	    }

	    protected override void doData()
	    {
		    base.doData();
	    }

	    protected override void addEvent()
	    {
		    base.addEvent();

		    for(int i = 0; i < Count; i++)
		    {
				var t = unitList[i].GetComponent<Toggle>();
				t.onValueChanged.AddListener((bool value) => OnToggleClick(t, value));
		    }
	    }

	    private void OnToggleClick(Toggle t ,bool value)
	    {
		    if(value == false)
		    {
			    return;;
		    }

		    for (int i = 0; i < Count; i++)
		    {
			    var toggle = unitList[i].GetComponent<Toggle>();

			    if(toggle == t)
			    {
				    DispatchEvent(EVENT_RADIO_GROUP_SELECTED, i);
			    }
		    }
		}


	    protected override void removeEvent()
	    {
		    base.removeEvent();

		    for (int i = 0; i < Count; i++)
		    {
			    var t = unitList[i].GetComponent<Toggle>();
			    t.onValueChanged.RemoveAllListeners();
		    }
		}

	    public bool SetLabel(uint index,string contect)
	    {
		    if (index >= Count)
		    {
			    return false;
		    }

		    var t = unitList[(int)index].GetComponentInChildren<Text>();

			if (t)
			{
			    t.text = contect;
				return true;
			}
		   
			return false;
	    }

	    public bool SetSelectedIndex(uint index)
	    {
		    if (index >= Count)
		    {
			    return false;
		    }

		    for(int i = 0; i < Count; i++)
		    {
				unitList[i].GetComponent<Toggle>().isOn = false;
			}

			selectedIndex = index;
		    unitList[(int)index].GetComponent<Toggle>().isOn = true;
			return true;
	    }

	    public uint SelectedIndex
	    {
			get
		    {
			    return selectedIndex;
		    }
	    }
	}
}
