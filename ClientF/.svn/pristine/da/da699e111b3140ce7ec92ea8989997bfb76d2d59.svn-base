using System;
using System.Collections.Generic;
using UnityEditor;

public class ProjectBuildSetting
{
    // Fields
    public const string ANIMS = "ani";
    public const string ASSETS = "a";
    public const string BITMAPS = "bi";
    public const string CONTROLLERS = "con";
    public const string FBXS = "fbx";
    public const string MATERIALS = "ma";
    public const string OBJS = "obj";
    public const string OTHERS = "ot";
    public const string PLAYABLES = "pl";
    public const string PREFABS = "pr";
    public static Dictionary<string, BuildTargetGroup> sBuildTargetGroupMap;
    public static Dictionary<string, BuildTarget> sBuildTargetMap;
    public const string SHADERS = "sh";
    public const string SOUNDS = "so";
    public const string U3DExt = ".unity3d";

    // Methods
    public static bool GetBoolFromArray(string[] arr_, string key_)
    {
        string strFromArray = GetStrFromArray(arr_, key_);
        if (string.IsNullOrEmpty(strFromArray))
        {
            return false;
        }
        return (strFromArray.ToLower() == "true");
    }

    public static int GetIntFromArray(string[] arr_, string key_)
    {
        string strFromArray = GetStrFromArray(arr_, key_);
        if (string.IsNullOrEmpty(strFromArray))
        {
            return 0;
        }
        int result = 0;
        int.TryParse(strFromArray, out result);
        return result;
    }

    public static string GetStrFromArray(string[] arr_, string key_)
    {
        int index = Array.IndexOf<string>(arr_, key_);
        if (index != -1)
        {
            return arr_[index + 1];
        }
        return "";
    }

    public static void Init()
    {
        sBuildTargetMap = new Dictionary<string, BuildTarget>();
        sBuildTargetMap.Add("android", BuildTarget.Android);
        sBuildTargetMap.Add("ios", BuildTarget.iOS);
        sBuildTargetGroupMap = new Dictionary<string, BuildTargetGroup>();
        sBuildTargetGroupMap.Add("android", BuildTargetGroup.Android);
        sBuildTargetGroupMap.Add("ios", BuildTargetGroup.iOS);
    }
}

