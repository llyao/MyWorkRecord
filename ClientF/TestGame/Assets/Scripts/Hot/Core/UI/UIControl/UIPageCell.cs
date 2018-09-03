using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GameILruntime.Core
{
	public class UIPageCell : UISkin
	{
		[UIComponent("icon")] public Image IconImage;
		[UIComponent("nameText")] public Text NameText;

		public void SetPos(float orgX, float orgY)
		{
			Skin.transform.localPosition = new Vector3(orgX, orgY,0);
		}

		protected override void bindComponent()
		{
			base.bindComponent();
		}

		protected override void doData()
		{
			base.doData();

			if(GetData == null)
			{
				if (IconImage)
				{

				}

				if (NameText)
				{
					NameText.text = "";
				}
			}
			else
			{
				if (IconImage)
				{

				}

				if (NameText)
				{
					NameText.text = GetData.LabelText;
				}
			}
		}

		private UIPageListCellData GetData
		{
			get
			{
				return UserData as UIPageListCellData;
			}
		}
	}
}
