using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.IO;
using System.Text;

public class AutoRegisterConfigPB : EditorWindow {

    static void Replace(string replace)
    {
        string curPathName = "Assets/Scripts/Hot/Config/ConfigManager.cs";
        string context = File.ReadAllText(curPathName, Encoding.Default);

        int end = context.LastIndexOf("Init()");
       // int end = context.IndexOf("}", start);
        string sub = context.Substring(0, end+6);
        context = sub+replace;
        Debug.Log(context);
        File.WriteAllText(curPathName, context, Encoding.Default);

    }
    [MenuItem("360/AutoRegisterConfigPB")]
	static void _AutoRegisterConfigPB()
    {
        Debug.Log(Application.dataPath);
        var classes  =Assembly.Load("Assembly-CSharp").GetTypes();
        //var classes = Assembly.GetExecutingAssembly().GetTypes(); 
        string replace = "\n\t{\n\t\t";
        foreach (var item in classes)
        {
           var bookarr = item.GetCustomAttributes(false);
            if(bookarr!=null)
            {
                foreach(var item2 in bookarr)
                {
                    var xx = item2 as AutoGenProtoBufRegisterAttribute;
                    if(xx!=null)
                    {
                        replace+=item.Name+ ".LoadFromDisk();"+"\n\t\t";
                    }

                }
            }

        }
        replace += "\n\t}\n}";
        Replace(replace);
    }
}
