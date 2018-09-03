using PlatformCore;
using UnityEngine.UI;
using UnityEngine;

namespace GameILruntime.Core
{
    public class UIButton : UISkin
    {
        public const string EVENT_BUTTON_CLICK = "event_button_click";

        [UIComponent("")] public Button mBtn;
        [UIComponent("icon")] public UIImgSlot mIcon;

        protected override void bindComponent()
        {
            base.bindComponent();
            if(mBtn != null)
                mBtn.onClick.AddListener(OnClick);
        }

        protected void OnClick()
        {
            DispatchEvent(EVENT_BUTTON_CLICK);
        }

        protected override void unbindComponent()
        {
            base.unbindComponent();

            if(mBtn != null)
                mBtn.onClick.RemoveAllListeners();

				
		}

        protected override void doData()
        {
            base.doData();
	        mIcon.UserData = UserData;
        }

        public void SetIconSlot(string name_)
        {
            GameObject go = FindGameObject(name_);
            if (go == null)
                return;
	        mIcon.Skin = go;
        }

        public void SetIcon(string url)
        {
            UserData = url;
        }
    }
}
