using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientApp.Response;
using ClientApp.XmlData;
using ClientCore.Config;
using ClientCore.Platform;
using ClientCore.Response;
using ClientCore.Utils;

namespace WebApp.Controllers
{
    public class ProtocolController : ProjectCenterBaseController
    {
		// GET: ClassManager
		public ActionResult ClassificationManagement()
		{
			SetProjectVersionData();
			
	        ViewData["protoClassList"] = _GetProtoList();
			return View();
        }

	    public ActionResult AddProtoIndex()
		{
			SetProjectVersionData();
			ProtocolClassificationResponse response = new ProtocolClassificationResponse(true);
		    response.Items = _GetProtoList().Items;
		    return View();
	    }

	    public ActionResult AddProtoDetail()
	    {
		    int id = int.Parse(Request.QueryString["id"]);
			SetProjectVersionData();
			XmlProtoClassificationData protoClassList = _GetProtoList();
			ProtocolClassificationItem item = null;
			for (int i = 0; i < protoClassList.Items.Count; i++)
			{
				if (protoClassList.Items[i].UID == id)
				{
					item = protoClassList.Items[i];
					break;
				}
			}

			if(item == null)
			{
				return Content("ID非法！");
			}

			ViewData["classID"] = id;
			ViewData["item"] = item;
			return View();
	    }

	    public ActionResult GetProtoList()
	    {
		    ProtocolClassificationResponse response = new ProtocolClassificationResponse(true);
		    response.Items = _GetProtoList().Items;
		    return response.Json();
	    }

	    public ActionResult GetProtoByID(int id)
	    {
		    ProtocolClassificationResponse response = new ProtocolClassificationResponse(true);
		    response.Items = new List<ProtocolClassificationItem>();
			XmlProtoClassificationData protoClassList = _GetProtoList();
		    foreach(var item in protoClassList.Items)
		    {
			    if(item.UID == id)
			    {
				    response.Items.Add(item);
				    break;
			    }
		    }
		    if(response.Items.Count == 0)
		    {
			    response.Success = false;
			    response.Message = "找不到分类信息";
		    }
		    return response.Json();
	    }

	    public ActionResult AddClassification(int id, string name, string desc, int startID, int endID)
	    {
			ResponseData response = new ResponseData(true);
		    if(name == null || name.Trim().Length == 0 || startID < 0 || startID >= endID)
		    {
			    response.Success = false;
			    response.Message = "参数不合法！";
			    return response.Json();
		    }
		    XmlProtoClassificationData protoClassList = _GetProtoList();
		    for(int i = 0; i < protoClassList.Items.Count; i++)
		    {
			    if(protoClassList.Items[i].UID == id)
			    {
					continue;
			    }
			    if ((endID >= protoClassList.Items[i].StartID && endID <= protoClassList.Items[i].EndID)
			       || (startID >= protoClassList.Items[i].StartID && startID <= protoClassList.Items[i].EndID))
			    {
					response.Success = false;
				    response.Message = "起始ID/终止ID 在【" + protoClassList.Items[i].Name + "】范围中间！";
				    return response.Json();
				}
		    }

		    if(id < 0)
		    {
				ProtocolClassificationItem newItem = new ProtocolClassificationItem();
			    newItem.UID = ++protoClassList.maxUID;
			    newItem.Name = name;
			    newItem.Desc = desc;
			    newItem.StartID = startID;
			    newItem.EndID = endID;
			    protoClassList.Items.Add(newItem);
			}
		    else
		    {
				foreach (var item in protoClassList.Items)
				{
					if (item.UID == id)
					{
						item.Name = name;
						item.Desc = desc;
						item.StartID = startID;
						item.EndID = endID;
						break;
					}
				}
			}
			

		    string protocolURL = string.Format(ConfigManager.Instance.GetSetting("PROTO_CLASSIFICATION"),
			    GetCurrentSelectedVersionID());

			ConfigManager.Instance.UpdateXML<XmlProtoClassificationData>(PathUtil.GetPath(protocolURL), protoClassList);
		    try
		    {
			    if(System.IO.File.Exists(PathUtil.GetPath(protocolURL)) == false)
				{
					PlatformManager.Instance.CreateParentDirectoryIfNeed(PathUtil.GetPath(protocolURL));
					FileStream stream = new FileStream(PathUtil.GetPath(protocolURL), FileMode.OpenOrCreate, FileAccess.ReadWrite);
					stream.Close();
				}
			    protoClassList.Export().Save(PathUtil.GetPath(protocolURL));
		    }
		    catch (Exception e)
		    {
			    response.Message = e.ToString();
			    response.Success = false;
		    }

		    return response.Json();
	    }

	    public ActionResult DeleteClass(int id)
	    {
			ResponseData response = new ResponseData(true);
		    XmlProtoClassificationData protoClassList = _GetProtoList();
		    for (int i = 0; i < protoClassList.Items.Count; i++)
		    {
			    if (protoClassList.Items[i].UID == id)
			    {
					protoClassList.Items.RemoveAt(i);

				    string protocolURL = string.Format(ConfigManager.Instance.GetSetting("PROTO_CLASSIFICATION"),
					    GetCurrentSelectedVersionID());
					ConfigManager.Instance.UpdateXML<XmlProtoClassificationData>(PathUtil.GetPath(protocolURL), protoClassList);
				    try
				    {
					    protoClassList.Export().Save(PathUtil.GetPath(protocolURL));
				    }
				    catch (Exception e)
				    {
					    response.Message = e.ToString();
					    response.Success = false;
					}
				    return response.Json();
				}
			}
		    response.Message = "找不到要删除的类别";
		    response.Success = false;
			return response.Json();
		}

	    private XmlProtoClassificationData _GetProtoList()
	    {
			string protocolURL = string.Format(ConfigManager.Instance.GetSetting("PROTO_CLASSIFICATION"),
			    GetCurrentSelectedVersionID());
		    XmlProtoClassificationData protoClassList = ConfigManager.Instance.GetXML<XmlProtoClassificationData>(PathUtil.GetPath(protocolURL));

		    if(protoClassList == null)
		    {
				protoClassList = new XmlProtoClassificationData();
			    protoClassList.maxUID = 1;
				protoClassList.Items = new List<ProtocolClassificationItem>();
		    }

		    return protoClassList;
	    }
    }
}