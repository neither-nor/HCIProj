﻿
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HSVPicker
{
	[System.Serializable]
	public class ColorPickerSetup
	{
		public enum ColorHeaderShowing
		{
			Hide,
			ShowColor,
			ShowColorCode,
			ShowAll,
		}

		[System.Serializable]
		public class UiElements
		{
			public RectTransform[] Elements;


			public void Toggle(bool active)
			{
				for (int cnt = 0; cnt < Elements.Length; cnt++)
				{
					Elements[cnt].gameObject.SetActive(active);
				}
			}

		}

		public bool ShowRgb = false;
		public bool ShowHsv = true;
		public bool ShowAlpha = false;
		public bool ShowColorBox = true;
		public bool ShowColorSliderToggle = false;

		public ColorHeaderShowing ShowHeader = ColorHeaderShowing.ShowAll;

		public UiElements RgbSliders;
		public UiElements HsvSliders;
		public UiElements ColorToggleElement;
		public UiElements AlphaSlidiers;


		public UiElements ColorHeader;
		public UiElements ColorCode;
		public UiElements ColorPreview;

		public UiElements ColorBox;
		public Text SliderToggleButtonText;

		public string PresetColorsId = "default";
		public Color[] DefaultPresetColors;
	}
}
