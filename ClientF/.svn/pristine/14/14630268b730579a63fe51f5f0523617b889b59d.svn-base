using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlatformCore;
using UnityEngine;
using UnityEngine.UI;

namespace GameILruntime.Core
{
    public class UIImgSlot:UISkin
	{
		public const string EVENT_IMAGE_CHANGE = "event_image_change";
		public const string EVENT_IMAGE_LOADED = "event_image_loaded";
		[UIComponent("")] public Image Content;

		protected override void bindComponent()
		{
			base.bindComponent();

		}


		protected override void unbindComponent()
		{
			base.unbindComponent();
		}

		protected string mOldUrl = "";
		protected override void doData()
		{
			base.doData();

			string url = UserData as string;
			if (string.IsNullOrEmpty(url) == true)
				return;
			if (mOldUrl == url)
				return;

			mOldUrl = url;
			AssetResource ar = PlatformCore.Core.AssetManager.GetResource(false, url, AssetResource.DataTypeE.TEXTURE);
			PlatformCore.Event.AddCompleteAndFail(ar, OnLoadImageComplete);
			ar.Load();
		}

		protected void OnLoadImageComplete(PlatformCore.Event e_)
		{
			AssetResource ar = e_.Target as AssetResource;
			if (ar.Url == UserData as string)
			{
				Sprite s = ar.Sprite;
				if (Content != null)
					Content.sprite = s;
				DispatchEvent(EVENT_IMAGE_LOADED, UserData as string);
			}
		}

		public void SetIcon(string url)
		{
			UserData = url;
			DispatchEvent(EVENT_IMAGE_CHANGE, url);
		}
	}
}
