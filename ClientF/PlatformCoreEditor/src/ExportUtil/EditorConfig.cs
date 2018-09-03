using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine ;
using UnityEditor ;

namespace platformCoreEditor
{
    public class EditorConfig
    {
        public string mRootName = "";
        public List<ExportConfigItem> mConfigItemLst = new List<ExportConfigItem>(); 

        public EditorConfig()
        {
            string path = Application.dataPath + "/Editor/EditorConfig.xml";
            if (File.Exists(path) == false)
                return;

            mConfigItemLst.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode node = doc.SelectSingleNode("root/export");
            mRootName = node.Attributes["rootName"].InnerText;
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                ExportConfigItem item = new ExportConfigItem();
                item.Initial(node.ChildNodes[i]);
                mConfigItemLst.Add(item);
            }
        }
    }
}
