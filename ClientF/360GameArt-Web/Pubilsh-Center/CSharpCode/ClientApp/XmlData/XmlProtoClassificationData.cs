using System;
using System.Collections.Generic;
using System.Xml;
using ClientCore.Config.Reader;

namespace ClientApp.XmlData
{
	public class XmlProtoClassificationData : IXmlParser, IXmlExporter
	{
		public int maxUID;
		public List<ProtocolClassificationItem> Items;

		public void Parse(XmlDocument document)
		{
			XmlNode rootNode = document.SelectSingleNode("root");
			var nodeList = rootNode.ChildNodes;
			maxUID = int.Parse(((XmlElement)rootNode).GetAttribute("maxUID"));
			Items = new List<ProtocolClassificationItem>();
			foreach (XmlNode node in nodeList)
			{
				try
				{
					// 将节点转换为元素，便于得到节点的属性值
					var element = (XmlElement)node;
					var item = new ProtocolClassificationItem();
					item.UID = int.Parse(element.GetAttribute("uid"));
					item.StartID = int.Parse(element.GetAttribute("startID"));
					item.EndID = int.Parse(element.GetAttribute("endID"));
					item.Name = element.GetAttribute("name");
					item.Desc = element.GetAttribute("desc");
					Items.Add(item);
				}
				catch (Exception)
				{
				}
			}
		}

		public XmlDocument Export()
		{
			XmlDocument document = new XmlDocument();
			//创建类型声明节点    
			XmlNode declaration = document.CreateXmlDeclaration("1.0", "utf-8", "");
			document.AppendChild(declaration);

			//创建根节点    
			XmlElement root = document.CreateElement("root");
			root.SetAttribute("maxUID", maxUID.ToString());
			document.AppendChild(root);

			for (int i = 0; i < Items.Count; i++)
			{
				XmlElement element = document.CreateElement("item");
				element.SetAttribute("uid", Items[i].UID.ToString());
				element.SetAttribute("startID", Items[i].StartID.ToString());
				element.SetAttribute("endID", Items[i].EndID.ToString());
				element.SetAttribute("name", Items[i].Name);
				element.SetAttribute("desc", Items[i].Desc);
				root.AppendChild(element);
			}

			return document;
		}
	}
}