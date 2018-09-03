using System.Collections.Generic;
using System.Xml;

namespace platformCoreEditor
{
    public class ExportConfigItem
    {
        public string name;
        public List<string> resPathLst = new List<string>();
        public List<string> extLst = new List<string>();

        protected string GetSafeAttribute(XmlNode node_ , string name_)
        {
            if (node_.Attributes[name_] == null)
                return "";
            return node_.Attributes[name_].InnerText;
        }

        public void Initial(XmlNode node_)
        {
            this.resPathLst.Clear();
            this.extLst.Clear();

            this.name = GetSafeAttribute(node_, "name");

            for (int i = 0; i < node_.ChildNodes.Count; i++)
            {
                if(node_.ChildNodes[i].Name == "res")
                {
                    string resPath = GetSafeAttribute(node_.ChildNodes[i], "path");
                    if (string.IsNullOrEmpty(resPath) == false)
                    {
                        if (resPath.EndsWith("/") == false)
                            resPath += "/";
                        resPathLst.Add(resPath);
                    }
                }
            }

            string extStr = GetSafeAttribute(node_, "ext");
            if (string.IsNullOrEmpty(extStr) == false)
            {
                string[] strLst = extStr.Split(',');
                for(int i = 0 ; i < strLst.Length ; i ++)
                    extLst.Add("*." + strLst[i]) ;
            }
            extLst.Add("*.prefab");
        }
    }
}