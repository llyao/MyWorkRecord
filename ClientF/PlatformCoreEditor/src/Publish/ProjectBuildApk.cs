using System;
using UnityEditor;
using UnityEngine;

public class ProjectBuildApk
{
    // Methods
    public static void BuildAPK()
    {
        Debug.Log("--------BuildAPK begin--------");
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        if (commandLineArgs.Length >= 0)
        {
            ProjectBuildSetting.Init();

            string scene = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-scene");

            string releasePath = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-releasePath");

            string displayName = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-displayName");

            string buildTarget = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-buildTarget").ToLower();

            BuildTarget targetPlatform = ProjectBuildSetting.sBuildTargetMap[buildTarget];

            string bundleVersion = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-bundleVersion");

            int buildNumber = ProjectBuildSetting.GetIntFromArray(commandLineArgs, "-buildNumber");

            string bundleIdentifier = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-bundleIdentifier");

            bool isDebug = ProjectBuildSetting.GetBoolFromArray(commandLineArgs, "-isDebug");

            string signFile = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-signFile");

            string keyaliasName = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-keyaliasName");

            string keyaliasPass = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-keyaliasPass");

            string keystorePass = ProjectBuildSetting.GetStrFromArray(commandLineArgs, "-keystorePass");

            switchToPlatform(ProjectBuildSetting.sBuildTargetGroupMap[buildTarget], targetPlatform);
            setPlayerSettings(targetPlatform, displayName, bundleVersion, buildNumber, bundleIdentifier, signFile, isDebug, keyaliasName, keyaliasPass, keystorePass);

            BuildOptions options = BuildOptions.None;
            if (isDebug)
            {
                options |= BuildOptions.Development;
                options |= BuildOptions.AllowDebugging;
                options |= BuildOptions.ConnectWithProfiler;
            }
            string[] sceneLst = new string[] { scene };
            BuildPipeline.BuildPlayer(sceneLst, releasePath, targetPlatform, options);
        }
        Debug.Log("--------BuildAPK end--------");
    }

    public static void setPlayerSettings(BuildTarget buildTarget, 
                                         string displayName, 
                                         string bundleVersion, 
                                         int buildNumber, 
                                         string bundleIdentifier, 
                                         string signFile, 
                                         bool isDebug, 
                                         string keyaliasName, 
                                         string keyaliasPass, 
                                         string keystorePass)
    {
        if (QualitySettings.GetQualityLevel() == 0)
        {
            QualitySettings.SetQualityLevel(3);
        }
        BuildTargetGroup group = BuildTargetGroup.Android;
        if (buildTarget == BuildTarget.Android)
        {
            group = BuildTargetGroup.Android ;
            PlayerSettings.productName = displayName ;
            PlayerSettings.bundleVersion = bundleVersion + "." + buildNumber;
            PlayerSettings.Android.bundleVersionCode = buildNumber;
            if (!string.IsNullOrEmpty(signFile))
            {
                PlayerSettings.Android.keystoreName = signFile;
            }
            if (!string.IsNullOrEmpty(keyaliasName))
            {
                Debug.Log("keyaliasName:" + keyaliasName);
                PlayerSettings.Android.keyaliasName = "sailing";
            }
            if (!string.IsNullOrEmpty(keyaliasPass))
            {
                Debug.Log("keyaliasPass:" + keyaliasPass);
                PlayerSettings.Android.keyaliasPass = "123456";
            }
            if (!string.IsNullOrEmpty(keystorePass))
            {
                Debug.Log("keystorePass:" + keystorePass);
                PlayerSettings.Android.keystorePass = "123456";
            }
        }
        else if (buildTarget == BuildTarget.iOS)
        {
            group = BuildTargetGroup.iOS;
            PlayerSettings.bundleVersion = bundleVersion ;
            PlayerSettings.iOS.buildNumber = buildNumber.ToString();
            PlayerSettings.iOS.applicationDisplayName = displayName ;
        }
        PlayerSettings.SetApplicationIdentifier(group, bundleIdentifier);
        if (isDebug)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, "DEBUG");
        }
        else
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, "RELEASE");
        }
    }

    protected static void switchToPlatform(BuildTargetGroup targetGroup, BuildTarget targetPlatform)
    {
        if (EditorUserBuildSettings.activeBuildTarget != targetPlatform)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, targetPlatform);
        }
    }
}


