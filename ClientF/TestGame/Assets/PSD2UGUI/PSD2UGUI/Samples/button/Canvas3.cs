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

public class Canvas3 : MonoBehaviour
{

	#region UI Variable Statement 
	[SerializeField] private CanvasScaler canvasscaler_Canvas; 
	[SerializeField] private GraphicRaycaster graphicraycaster_Canvas; 
	[SerializeField] private Image image_CodePanel; 
	[SerializeField] private Text text_Title; 
	[SerializeField] private Image image_Icon; 
	[SerializeField] private Image image_Button; 
	[SerializeField] private Button button_Button; 
	[SerializeField] private Text text_Text; 
	[SerializeField] private Toggle toggle_Toggle; 
	[SerializeField] private Image image_Background; 
	[SerializeField] private Image image_Checkmark; 
	[SerializeField] private Text text_Label; 
	[SerializeField] private Slider slider_Slider; 
	[SerializeField] private Image image_Background14; 
	[SerializeField] private Image image_Fill; 
	[SerializeField] private Image image_Handle; 
	[SerializeField] private Image image_Dropdown; 
	[SerializeField] private Dropdown dropdown_Dropdown; 
	[SerializeField] private Text text_Label19; 
	[SerializeField] private Image image_Arrow; 
	[SerializeField] private Image image_Template; 
	[SerializeField] private ScrollRect scrollrect_Template; 
	[SerializeField] private Mask mask_Viewport; 
	[SerializeField] private Image image_Viewport; 
	[SerializeField] private Toggle toggle_Item; 
	[SerializeField] private Image image_Item_Background; 
	[SerializeField] private Image image_Item_Checkmark; 
	[SerializeField] private Text text_Item_Label; 
	[SerializeField] private Image image_Scrollbar; 
	[SerializeField] private Scrollbar scrollbar_Scrollbar; 
	[SerializeField] private Image image_Handle31; 
	[SerializeField] private Image image_InputField; 
	[SerializeField] private InputField inputfield_InputField; 
	[SerializeField] private Text text_Placeholder; 
	[SerializeField] private Text text_Text35; 
	#endregion 

	#region UI Variable Assignment 
	private void InitUI() 
	{
		canvasscaler_Canvas = transform.GetComponent<CanvasScaler>(); 
		graphicraycaster_Canvas = transform.GetComponent<GraphicRaycaster>(); 
		image_CodePanel = transform.Find("CodePanel").GetComponent<Image>(); 
		text_Title = transform.Find("CodePanel/Title").GetComponent<Text>(); 
		image_Icon = transform.Find("CodePanel/Icon").GetComponent<Image>(); 
		image_Button = transform.Find("CodePanel/Button").GetComponent<Image>(); 
		button_Button = transform.Find("CodePanel/Button").GetComponent<Button>(); 
		text_Text = transform.Find("CodePanel/Button/Text").GetComponent<Text>(); 
		toggle_Toggle = transform.Find("CodePanel/Toggle").GetComponent<Toggle>(); 
		image_Background = transform.Find("CodePanel/Toggle/Background").GetComponent<Image>(); 
		image_Checkmark = transform.Find("CodePanel/Toggle/Background/Checkmark").GetComponent<Image>(); 
		text_Label = transform.Find("CodePanel/Toggle/Label").GetComponent<Text>(); 
		slider_Slider = transform.Find("CodePanel/Slider").GetComponent<Slider>(); 
		image_Background14 = transform.Find("CodePanel/Slider/Background").GetComponent<Image>(); 
		image_Fill = transform.Find("CodePanel/Slider/Fill Area/Fill").GetComponent<Image>(); 
		image_Handle = transform.Find("CodePanel/Slider/Handle Slide Area/Handle").GetComponent<Image>(); 
		image_Dropdown = transform.Find("CodePanel/Dropdown").GetComponent<Image>(); 
		dropdown_Dropdown = transform.Find("CodePanel/Dropdown").GetComponent<Dropdown>(); 
		text_Label19 = transform.Find("CodePanel/Dropdown/Label").GetComponent<Text>(); 
		image_Arrow = transform.Find("CodePanel/Dropdown/Arrow").GetComponent<Image>(); 
		image_Template = transform.Find("CodePanel/Dropdown/Template").GetComponent<Image>(); 
		scrollrect_Template = transform.Find("CodePanel/Dropdown/Template").GetComponent<ScrollRect>(); 
		mask_Viewport = transform.Find("CodePanel/Dropdown/Template/Viewport").GetComponent<Mask>(); 
		image_Viewport = transform.Find("CodePanel/Dropdown/Template/Viewport").GetComponent<Image>(); 
		toggle_Item = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item").GetComponent<Toggle>(); 
		image_Item_Background = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item/Item Background").GetComponent<Image>(); 
		image_Item_Checkmark = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item/Item Checkmark").GetComponent<Image>(); 
		text_Item_Label = transform.Find("CodePanel/Dropdown/Template/Viewport/Content/Item/Item Label").GetComponent<Text>(); 
		image_Scrollbar = transform.Find("CodePanel/Dropdown/Template/Scrollbar").GetComponent<Image>(); 
		scrollbar_Scrollbar = transform.Find("CodePanel/Dropdown/Template/Scrollbar").GetComponent<Scrollbar>(); 
		image_Handle31 = transform.Find("CodePanel/Dropdown/Template/Scrollbar/Sliding Area/Handle").GetComponent<Image>(); 
		image_InputField = transform.Find("CodePanel/InputField").GetComponent<Image>(); 
		inputfield_InputField = transform.Find("CodePanel/InputField").GetComponent<InputField>(); 
		text_Placeholder = transform.Find("CodePanel/InputField/Placeholder").GetComponent<Text>(); 
		text_Text35 = transform.Find("CodePanel/InputField/Text").GetComponent<Text>(); 

	}
	#endregion 

	#region UI Event Register 
	private void AddEvent() 
	{
		button_Button.onClick.AddListener(OnButtonClicked);
		toggle_Toggle.onValueChanged.AddListener(OnToggleValueChanged);
		slider_Slider.onValueChanged.AddListener(OnSliderValueChanged);
		dropdown_Dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
		scrollrect_Template.onValueChanged.AddListener(OnTemplateValueChanged);
		toggle_Item.onValueChanged.AddListener(OnItemValueChanged);
		scrollbar_Scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
		inputfield_InputField.onValueChanged.AddListener(OnInputFieldValueChanged);
	}
 
	private void OnButtonClicked()
	{

	}

	private void OnToggleValueChanged(bool arg)
	{

	}

	private void OnSliderValueChanged(float arg)
	{

	}

	private void OnDropdownValueChanged(int arg)
	{

	}

	private void OnTemplateValueChanged(Vector2 arg)
	{

	}

	private void OnItemValueChanged(bool arg)
	{

	}

	private void OnScrollbarValueChanged(float arg)
	{

	}

	private void OnInputFieldValueChanged(string arg)
	{

	}
	#endregion 

}
