using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameILruntime.Core
{
    public class UICheckGroup:UISkin
    {
	    public const string EVENT_CHECK_GROUP_SELECTED = "event_check_group_selected";

	    public const string CHECK_UNIT_NAME = "unit";

	    private uint Count = 0;
	    private bool[] checkValueList;
	    private string[] checkLabelList;
	    private List<GameObject> unitList;

		protected override void bindComponent()
	    {
		    base.bindComponent();

		    uint i = 0;
		    unitList = new List<GameObject>();

			while (true)
		    {
			    GameObject go = FindGameObject(CHECK_UNIT_NAME+ i.ToString());
			    i++;

				if (go == null)
				{
					Count = i;
					InitUnitData(Count);
					break;
			    }

			    unitList.Add(go);
			}
		}

	    private void InitUnitData(uint count)
	    {
			checkValueList = new bool[Count];
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

	    public bool SetLabal(uint index)
	    {
		    if(index >= Count)
		    {
			    return false;
		    }

		    return true;
	    }
	}
}
