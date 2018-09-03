using System;
using System.Collections.Generic;
using System.Xml;
using ClientApp.Constants;
using ClientCore.Config.Reader;

namespace ClientApp.XmlData
{
	public class XmlProtoClassDetail : IXmlParser, IXmlExporter
	{
		public int MaxUID;
		public int ClassUID;
		public string Package;
		public List<XmlProtoMessage> Messages;

		public void Parse(XmlDocument document)
		{
			XmlNode rootNode = document.SelectSingleNode("proto");
			var nodeList = rootNode.ChildNodes;
			MaxUID = int.Parse(((XmlElement)rootNode).GetAttribute("maxUID"));
			ClassUID = int.Parse(((XmlElement)rootNode).GetAttribute("classUID"));
			Package = ((XmlElement)rootNode).GetAttribute("ackage");
			Messages = new List<XmlProtoMessage>();
			foreach (XmlNode node in nodeList)
			{
				try
				{
					// 将节点转换为元素，便于得到节点的属性值
					var element = (XmlElement)node;
					var item = new XmlProtoMessage();
					item.UID = int.Parse(element.GetAttribute("uid"));
					item.ID = int.Parse(element.GetAttribute("id"));
					item.Name = element.GetAttribute("name");
					item.Comment = element.GetAttribute("comment");

					item.Fields = new List<ProtoMessageField>();
					var fieldNodeList = element.ChildNodes;
					foreach (var fieldNote in fieldNodeList)
					{
						var fieldElement = (XmlElement)fieldNote;
						if(ProtoType.IsDefined(fieldElement.Name))
						{
							ProtoMessageField field = new ProtoMessageField();
							if(fieldElement.Name == ProtoType.REPEATED)
							{
								field.Repeated = true;
								field.Type = fieldElement.GetAttribute("type");
							}
							if (fieldElement.Name == ProtoType.STRUCT)
							{
								field.Repeated = false;
								field.Type = ProtoType.STRUCT;
								field.StructClassUID = int.Parse(fieldElement.GetAttribute("structClassUID"));
								field.StructUID = int.Parse(fieldElement.GetAttribute("sructUID"));
							}
							else
							{
								field.Repeated = false;
								field.Type = fieldElement.Name;
							}
							field.Name = fieldElement.GetAttribute("name");
							field.Comment = fieldElement.GetAttribute("Comment");
							item.Fields.Add(field);
						}
					}

					Messages.Add(item);
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
			XmlElement root = document.CreateElement("proto");
			root.SetAttribute("maxUID", MaxUID.ToString());
			root.SetAttribute("classUID", ClassUID.ToString());
			root.SetAttribute("package", Package);
			document.AppendChild(root);

			for (int i = 0; i < Messages.Count; i++)
			{
				XmlElement element = document.CreateElement("message");
				element.SetAttribute("uid", Messages[i].UID.ToString());
				element.SetAttribute("id", Messages[i].ID.ToString());
				element.SetAttribute("name", Messages[i].Name);
				element.SetAttribute("comment", Messages[i].Comment);


				for (int j = 0; j < Messages[i].Fields.Count; j++)
				{
					XmlElement fieldElement = document.CreateElement("path");
					ProtoMessageField field = Messages[i].Fields[j];
					if (field.Repeated)
					{
						fieldElement = document.CreateElement(ProtoType.REPEATED);
					}
					else
					{
						fieldElement = document.CreateElement(field.Type);
					}
					if (field.Type == ProtoType.STRUCT)
					{
						fieldElement.SetAttribute("structClassUID", field.StructClassUID.ToString());
						fieldElement.SetAttribute("structUID", field.StructUID.ToString());
					}
					fieldElement.SetAttribute("name", field.Name);
					fieldElement.SetAttribute("comment", field.Comment);
					element.AppendChild(fieldElement);
				}

				root.AppendChild(element);
			}

			return document;
		}
	}

	public class XmlProtoMessage
	{
		public int UID;
		public int ID;
		public string Name;
		public string Comment;
		public List<ProtoMessageField> Fields;
	}

	public class ProtoMessageField
	{
		public string Type;
		public bool Repeated;
		public string Name;
		public string Comment;

		/// <summary>
		/// 引用的结构体ID，仅当Type为struct时有用
		/// </summary>
		public int StructUID;
		/// <summary>
		/// 引用的结构体的类ID，仅当Type为struct时有用
		/// </summary>
		public int StructClassUID;
	}
}