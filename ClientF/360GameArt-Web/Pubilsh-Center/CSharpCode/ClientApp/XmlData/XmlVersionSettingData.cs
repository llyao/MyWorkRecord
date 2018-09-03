using System;
using System.Collections.Generic;
using System.Xml;
using ClientCore.Config.Reader;

namespace ClientApp.XmlData
{
	public class XmlVersionSettingData : IXmlParser,IXmlExporter
	{
		public List<ToggleNode> ToggleNodes;
		public List<InputNode> InputNodes;
		public List<ResNode> ResNodes;

		public void Parse(XmlDocument document)
		{
			ToggleNodes = new List<ToggleNode>();
			InputNodes = new List<InputNode>();
			ResNodes = new List<ResNode>();
			var nodeList = document.SelectSingleNode("root").ChildNodes;
			foreach(XmlNode node in nodeList)
			{
				try
				{
					// 将节点转换为元素，便于得到节点的属性值
					var element = (XmlElement)node;
					if(node.Name.Equals("toggle"))
					{
						var toggleNode = new ToggleNode();
						toggleNode.Desc = element.GetAttribute("desc");
						toggleNode.Name = element.GetAttribute("name");
						toggleNode.Selected = node.InnerText.ToLower().Equals("true");
						ToggleNodes.Add(toggleNode);
					}
					else if(element.Name.Equals("input"))
					{
						var inpuNode = new InputNode();
						inpuNode.Desc = element.GetAttribute("desc");
						inpuNode.Name = element.GetAttribute("name");
						inpuNode.Value = node.InnerText;
						InputNodes.Add(inpuNode);
					}
					else if(element.Name.Equals("res"))
					{
						var resNode = new ResNode();
						resNode.Type = element.GetAttribute("type");
						resNode.TypeName = element.GetAttribute("typeName");
						resNode.Paths = new List<string>();
						var pathNodeList = element.SelectNodes("path");
						foreach(var pathNode in pathNodeList)
						{
							resNode.Paths.Add(((XmlElement)pathNode).InnerText);
						}

						ResNodes.Add(resNode);
					}
				}
				catch(Exception)
				{
				}
			}
		}

		public class ToggleNode
		{
			public string Desc;
			public string Name;
			public bool Selected;
		}

		public class InputNode
		{
			public string Desc;
			public string Name;
			public string Value;
		}

		public class ResNode
		{
			public string Type;
			public string TypeName;
			public List<string> Paths;
		}

		public XmlDocument Export()
		{
			XmlDocument document = new XmlDocument();
			//创建类型声明节点    
			XmlNode declaration = document.CreateXmlDeclaration("1.0", "utf-8", "");
			document.AppendChild(declaration);

			//创建根节点    
			XmlNode root = document.CreateElement("root");
			document.AppendChild(root);

			for(int i = 0; i < ToggleNodes.Count; i++)
			{
				XmlElement element = document.CreateElement("toggle");
				element.InnerText = ToggleNodes[i].Selected.ToString().ToLower();
				element.SetAttribute("desc", ToggleNodes[i].Desc);
				element.SetAttribute("name", ToggleNodes[i].Name);
				root.AppendChild(element);
			}

			for (int i = 0; i < InputNodes.Count; i++)
			{
				XmlElement element = document.CreateElement("input");
				element.InnerText = InputNodes[i].Value;
				element.SetAttribute("desc", InputNodes[i].Desc);
				element.SetAttribute("name", InputNodes[i].Name);
				root.AppendChild(element);
			}

			for (int i = 0; i < ResNodes.Count; i++)
			{
				XmlElement element = document.CreateElement("res");
				element.SetAttribute("type", ResNodes[i].Type);
				element.SetAttribute("typeName", ResNodes[i].TypeName);
				for(int j = 0; j < ResNodes[i].Paths.Count; j++)
				{
					XmlElement pathElement = document.CreateElement("path");
					pathElement.InnerText = ResNodes[i].Paths[j];
					element.AppendChild(pathElement);
				}
				root.AppendChild(element);
			}

			return document;
		}
	}
}