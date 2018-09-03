using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Audio
{
	public static class AudioEngine
	{
		public static void LoadBank(string name, bool decodeBank, bool saveDecodedBank)
		{
			AkBankManager.LoadBank(name, decodeBank, saveDecodedBank);
		}

		public static void LoadBankAsync(string name, AkCallbackManager.BankCallback callback = null)
		{
			AkBankManager.LoadBankAsync(name, callback);
		}

		public static AKRESULT LoadFilePackage(string in_pszFilePackageName, out uint out_uPackageID)
		{
			return AkSoundEngine.LoadFilePackage(in_pszFilePackageName, out out_uPackageID, AkSoundEngine.AK_DEFAULT_POOL_ID);
		}

		public static void UnloadBank(string name)
		{
			AkBankManager.UnloadBank(name);
		}

		public static uint PostEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_cExternals, AkExternalSourceInfo in_pExternalSources, uint in_PlayingID)
		{
			return AkSoundEngine.PostEvent(in_eventID, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie, in_cExternals, in_pExternalSources, in_PlayingID);
		}

		public static uint PostEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_cExternals, AkExternalSourceInfo in_pExternalSources)
		{
			return AkSoundEngine.PostEvent(in_eventID, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie, in_cExternals, in_pExternalSources);
		}

		public static uint PostEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_cExternals)
		{
			return AkSoundEngine.PostEvent(in_eventID, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie, in_cExternals);
		}

		public static uint PostEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie)
		{
			return AkSoundEngine.PostEvent(in_eventID, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie);
		}

		public static uint PostEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID, uint in_uFlags)
		{

			return AkSoundEngine.PostEvent(in_eventID, in_gameObjectID, in_uFlags);
		}

		public static uint PostEvent(uint in_eventID, UnityEngine.GameObject in_gameObjectID)
		{
			return AkSoundEngine.PostEvent(in_eventID, in_gameObjectID);
		}

		public static uint PostEvent(string in_pszEventName, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_cExternals, AkExternalSourceInfo in_pExternalSources, uint in_PlayingID)
		{

			return AkSoundEngine.PostEvent(in_pszEventName, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie, in_cExternals, in_pExternalSources, in_PlayingID);
		}

		public static uint PostEvent(string in_pszEventName, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_cExternals, AkExternalSourceInfo in_pExternalSources)
		{
			return AkSoundEngine.PostEvent(in_pszEventName, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie, in_cExternals, in_pExternalSources);
		}

		public static uint PostEvent(string in_pszEventName, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie, uint in_cExternals)
		{

			return AkSoundEngine.PostEvent(in_pszEventName, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie, in_cExternals);
		}

		public static uint PostEvent(string in_pszEventName, UnityEngine.GameObject in_gameObjectID, uint in_uFlags, AkCallbackManager.EventCallback in_pfnCallback, object in_pCookie)
		{
			return AkSoundEngine.PostEvent(in_pszEventName, in_gameObjectID, in_uFlags, in_pfnCallback, in_pCookie);
		}

		public static uint PostEvent(string in_pszEventName, UnityEngine.GameObject in_gameObjectID, uint in_uFlags)
		{
			return AkSoundEngine.PostEvent(in_pszEventName, in_gameObjectID, in_uFlags);
		}

		public static uint PostEvent(string in_pszEventName, UnityEngine.GameObject in_gameObjectID)
		{
			return AkSoundEngine.PostEvent(in_pszEventName, in_gameObjectID);
		}
	}
}
