using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace GameILruntime.Core
{
	public class UIPageList : UISkin
	{
		public enum AutoCellLength
		{
			NONE,
			WIDTH,
			HEIGHT,
		}

		public const string CELL_UNIT_NAME = "Content/Cell";
		private uint mCount = 0;
		private List<UIPageCell> unitList;

		private uint lineNum =3;
		private uint columnNum = 3;
		private AutoCellLength autoCellLength = AutoCellLength.NONE;

		private GameObject modelCellGameObject;

		private float orgX;
		private float orgY;
		private float minX;
		private float maxX;
		private float minY;
		private float maxY;
		private float lineSpaceX;
		private float lineSpaceY;

//		public delegate UIPageCell CreateCell();
//		public Action CreateACell;
		private Func<UIPageCell> createCell;

		public UIPageList()
		{
		}

		protected override void bindComponent()
		{
			base.bindComponent();

			uint i = 0;
			unitList = new List<UIPageCell>();

			if(createCell == null)
			{
				createCell = DefaultCreate;
			}

			modelCellGameObject = FindGameObject(CELL_UNIT_NAME + "0");

			orgX = modelCellGameObject.transform.localPosition.x;
			orgY = modelCellGameObject.transform.localPosition.y;
			minX = modelCellGameObject.transform.localPosition.x;
			maxX = modelCellGameObject.transform.localPosition.x;
			minY = modelCellGameObject.transform.localPosition.y;
			maxY = modelCellGameObject.transform.localPosition.y;

			while (true)
			{
				GameObject go = FindGameObject(CELL_UNIT_NAME + i);

				if (go == null)
				{
					break;
				}

				i++;

				var cell = createCell();
				unitList.Add(cell);
				cell.Skin = go;
				GetBound(go);
			}

			InitLineSpace();
			Count = lineNum * columnNum;
			InitLayoutCell();
		}

		private UIPageCell DefaultCreate()
		{
			return new UIPageCell();
		}

		public void SetCreateCell(Func<UIPageCell> f)
		{

			createCell = f;

			for(int i = 0; i < unitList.Count; i++)
			{
				var go = unitList[i].Skin;
				unitList[i].Skin = null;
				var cell = createCell();
				cell.Skin = go;
				unitList[i] = cell;
			}
		}

		public uint Count
		{
			get
			{
				return mCount;
			}

			set
			{
				mCount = value;
				CountLine();

				for (int i = 0; i < lineNum * columnNum; i++)
				{
					if(i < unitList.Count)
					{
						if(i < mCount)
						{
							unitList[i].Active = true;
						}
						else
						{
							unitList[i].Active = false;
						}
					}
					else
					{
						var go = Object.Instantiate(modelCellGameObject, modelCellGameObject.transform.parent);
						var cell = new UIPageCell();
						unitList.Add(cell);
						cell.Skin = go;
						cell.Skin.name = "Cell" + i;
					}
				}

				InitLayoutCell();
			}
		}

		private void InitLineSpace()
		{
			int i = 0;

			for( i = 1; i < unitList.Count; i++)
			{
				if(Math.Abs(unitList[i].Transform.localPosition.x - minX)< 10)
				{
					columnNum = (uint)i;
					break;
				}
			}

			for (i = 1; i < unitList.Count; i++)
			{
				if (Math.Abs(unitList[i].Transform.localPosition.x - minX) < 10)
				{
					columnNum = (uint)i;
					break;
				}
			}

			LineSpaceX = (maxX - minX) / (columnNum - 1);
			LineSpaceY = maxY - unitList[(int)columnNum].Transform.localPosition.y;
		}

		public AutoCellLength AutoCell
		{
			get
			{
				return autoCellLength;
			}

			set
			{
				autoCellLength = value;
			}
		}

		public float LineSpaceX
		{
			get
			{
				return lineSpaceX;
			}

			set
			{
				lineSpaceX = value;
			}
		}

		public float LineSpaceY
		{
			get
			{
				return lineSpaceY;
			}

			set
			{
				lineSpaceY = value;
			}
		}

		private void GetBound(GameObject go)
		{
			if(go.transform.localPosition.x < minX)
			{
				minX = go.transform.localPosition.x;
			}

			if (go.transform.localPosition.x > maxX)
			{
				maxX = go.transform.localPosition.x;
			}

			if (go.transform.localPosition.y < minY)
			{
				minY = go.transform.localPosition.y;
			}

			if (go.transform.localPosition.y > maxY)
			{
				maxY = go.transform.localPosition.y;
			}
		}

		private void CountLine()
		{
			switch(autoCellLength)
			{
				case AutoCellLength.NONE:

					break;
				case AutoCellLength.HEIGHT:

					lineNum = (uint)Mathf.Ceil(Count / columnNum);
					break;
				case AutoCellLength.WIDTH:
					columnNum = (uint)Mathf.Ceil(Count / lineNum);
					break;
			}
		}

		private void InitLayoutCell()
		{
			for(int i = 0; i < lineNum; i++)
			{
				for (int j = 0; j < columnNum; j++)
				{
					var index = (int)(i * columnNum + j);

					var unit = unitList[index];
					unit.SetPos(orgX+ LineSpaceX*j, orgY - LineSpaceY * i);
				}
			}
		}

		public void SetLineWH(uint w,uint h)
		{
			columnNum = w;
			lineNum = h;
		}

		public void SetCellList(List<UIPageListCellData> data)
		{
			if(data == null)
			{
				for(int i = 0; i < Count; i++)
				{
					unitList[i].UserData = null;
				}

				return;
			}

			UserData = data;

			for(int i = 0; i < Count; i++)
			{
				if(i < data.Count)
				{
					unitList[i].UserData = data[i];
				}
				else
				{
					unitList[i].UserData = null;
				}
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
		}

		protected override void removeEvent()
		{
			base.removeEvent();
		}
	}
}