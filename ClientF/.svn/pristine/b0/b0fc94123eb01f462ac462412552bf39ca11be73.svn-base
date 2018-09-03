//this file is auto created by QuickCode,you can edit it 
//do not need to care initialization of ui widget any more 
//------------------------------------------------------------------------------
/**
* @author :
* date    :
* purpose :
*/
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CanvasTestUIView : MonoBehaviour
{
	#region UI Variable Statement 
	[SerializeField] private Button button_Button; 
	[SerializeField] private Image image_CodePanel; 
	[SerializeField] private Image image_Icon; 
	[SerializeField] private Image image_Button; 
	[SerializeField] private Image image_Background; 
	[SerializeField] private Image image_Checkmark; 
	[SerializeField] private Image image_Background7; 
	[SerializeField] private Image image_Fill; 
	[SerializeField] private Image image_Handle; 
	[SerializeField] private Image image_Dropdown; 
	[SerializeField] private Image image_Arrow; 
	[SerializeField] private Image image_Template; 
	[SerializeField] private Image image_Viewport; 
	[SerializeField] private Image image_Item_Background; 
	[SerializeField] private Image image_Item_Checkmark; 
	[SerializeField] private Image image_Scrollbar; 
	[SerializeField] private Image image_Handle17; 
	[SerializeField] private Image image_InputField; 
	[SerializeField] private Text text_Title; 
	[SerializeField] private Text text_Text; 
	[SerializeField] private Text text_Label; 
	[SerializeField] private Text text_Label22; 
	[SerializeField] private Text text_Item_Label; 
	[SerializeField] private Text text_Placeholder; 
	[SerializeField] private Text text_Text25; 
	[SerializeField] private Toggle toggle_Toggle; 
	[SerializeField] private Toggle toggle_Item; 
	#endregion 

	#region UI Variable Assignment 
	private void Awake() 
	{
		button_Button = transform.Find("CodePanel/Button").GetComponent<Button>(); 
		image_CodePanel = transform.Find("CodePanel").GetComponent<Image>(); 
		image_Icon = transform.Find("CodePanel/Icon").GetComponent<Image>(); 
		image_Button = transform.Find("CodePanel/Button").GetComponent<Image>(); 
		image_Background = transform.Find("CodePanel/Toggle/Background").GetComponent<Image>(); 
		image_Checkmark = transform.Find("CodePanel/Toggle/Background/Checkmark").GetComponent<Image>(); 
		image_Background7 = transform.Find("CodePanel/Slider/Background").GetComponent<Image>(); 
		image_Fill = transform.Find("CodePanel/Slider/Fill Area/Fill").GetComponent<Image>(); 
		image_Handle = transform.Find("CodePanel/Slider/Handle Slide Area/Handle").GetComponent<Image>(); 
		image_Dropdown = transform.Find("CodePanel/Dropdown").GetComponent<Image>(); 
		image_Arrow = transform.Find("CodePanel/Dropdown/Arrow").GetComponent<Image>(); 
		image_Template = transform.Find("CodePanel/Dropdown/Template").GetComponent<Image>(); 
		image_Viewport = transform.Find("CodePanel/Dropdown/Template/Viewport").GetComponent<Image>(); 
		image_Item_Background = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item/Item Background").GetComponent<Image>(); 
		image_Item_Checkmark = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item/Item Checkmark").GetComponent<Image>(); 
		image_Scrollbar = transform.Find("CodePanel/Dropdown/Template/Scrollbar").GetComponent<Image>(); 
		image_Handle17 = transform.Find("CodePanel/Dropdown/Template/Scrollbar/Sliding Area/Handle").GetComponent<Image>(); 
		image_InputField = transform.Find("CodePanel/InputField").GetComponent<Image>(); 
		text_Title = transform.Find("CodePanel/Title").GetComponent<Text>(); 
		text_Text = transform.Find("CodePanel/Button/Text").GetComponent<Text>(); 
		text_Label = transform.Find("CodePanel/Toggle/Label").GetComponent<Text>(); 
		text_Label22 = transform.Find("CodePanel/Dropdown/Label").GetComponent<Text>(); 
		text_Item_Label = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item/Item Label").GetComponent<Text>(); 
		text_Placeholder = transform.Find("CodePanel/InputField/Placeholder").GetComponent<Text>(); 
		text_Text25 = transform.Find("CodePanel/InputField/Text").GetComponent<Text>(); 
		toggle_Toggle = transform.Find("CodePanel/Toggle").GetComponent<Toggle>(); 
		toggle_Item = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item").GetComponent<Toggle>(); 

		AddEvent();
	}
	#endregion 

	#region Events 
	public Action<GameObject> OnButtonClickedEvent;

	public Action<bool> OnToggleValueChangedEvent;

	public Action<bool> OnItemValueChangedEvent;
	#endregion 

	#region UI Event Register 
	private void AddEvent() 
	{

		button_Button.onClick.AddListener(()=> { OnButtonClicked(button_Button.gameObject);});
		toggle_Toggle.onValueChanged.AddListener(OnToggleValueChanged);
		toggle_Item.onValueChanged.AddListener(OnItemValueChanged);
	}

	private void OnButtonClicked(GameObject obj)
	{
        Debug.Log(obj.name);
		if(OnButtonClickedEvent!=null)
			OnButtonClickedEvent(obj);
	}

	private void OnToggleValueChanged(bool arg)
	{
		if(OnToggleValueChangedEvent!=null)
			OnToggleValueChangedEvent(arg);
	}

	private void OnItemValueChanged(bool arg)
	{
		if(OnItemValueChangedEvent!=null)
			OnItemValueChangedEvent(arg);
	}
	#endregion 

	#region UI Set Label 
	public void SetTitleText(string str)
	{
		text_Title.text=str;
	}
	public void SetTextText(string str)
	{
		text_Text.text=str;
	}
	public void SetLabelText(string str)
	{
		text_Label.text=str;
	}
	public void SetLabel22Text(string str)
	{
		text_Label22.text=str;
	}
	public void SetItem_LabelText(string str)
	{
		text_Item_Label.text=str;
	}
	public void SetPlaceholderText(string str)
	{
		text_Placeholder.text=str;
	}
	public void SetText25Text(string str)
	{
		text_Text25.text=str;
	}
	#endregion 

}
