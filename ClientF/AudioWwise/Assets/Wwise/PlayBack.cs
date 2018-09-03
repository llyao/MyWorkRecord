using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class PlayBack : MonoBehaviour
{

	public string FileName;

	// Use this for initialization
	void Start ()
	{

//		string fileName;
		//"C:\\ClientFrame\\AudioWwise\\Assets\\StreamingAssets\\Audio\\GeneratedSoundBanks\\Windows\\"

		//	#if UNITY_IPHONE
		//		string fileNameBase = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
		//		fileName = fileNameBase.Substring(0, fileNameBase.LastIndexOf('/')) + "/Documents/" + FILE_NAME;
		//#elif UNITY_ANDROID
		//		fileName = Application.persistentDataPath + "/" + FILE_NAME ;
		//#else
		//		fileName = Application.dataPath + "/" + FILE_NAME;
		//	#endif

//#if UNITY_IPHONE
//	string fileNameBase = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
//	fileName = fileNameBase.Substring(0, fileNameBase.LastIndexOf('/')) + "/Documents/";
//#elif UNITY_ANDROID
//	fileName = Application.persistentDataPath;
//#else
//	fileName = Application.streamingAssetsPath;
//#endif
//		fileName = System.IO.Path.Combine(fileName,System.IO.Path.Combine(AkWwiseInitializationSettings.ActivePlatformSettings.SoundbankPath, AkBasePathGetter.GetPlatformName()));
//		//AkSoundEngine.AddBasePath(fileName+"\\");
//		AkSoundEngine.SetBasePath(fileName + "11\\");

		//		AKRESULT akresult = AkSoundEngine.SetBasePath("StreamingAsset/Audio/GeneratedSoundBanks/");

		//AudioEngine.LoadBank("MainSoundBank",false,false);
		AudioEngine.LoadBank("Init", false, false);
		//		AKRESULT result = AkSoundEngine.LoadFilePackage("xxx.pck", out in_uInMemoryBankSize, packageID);

		//AkBankManager.

		uint m_BankID;
		AKRESULT result = AudioEngine.LoadFilePackage(FileName, out m_BankID);
		AudioEngine.LoadBank("MainSoundBank",false,false);
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			AkSoundEngine.PostEvent("TTest", gameObject);
		}
	}
}
