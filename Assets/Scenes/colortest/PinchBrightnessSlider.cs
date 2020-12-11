﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
public class PinchBrightnessSlider : MonoBehaviour
{
	public ColorPicker hsvpicker;

	/// <summary>
	/// Which value this slider can edit.
	/// </summary>
	public ColorValues type;

	private Slider slider;

	private bool listen = true;

	private void Awake()
	{
		slider = GetComponent<Slider>();

		hsvpicker.onValueChanged.AddListener(ColorChanged);
		hsvpicker.onHSVChanged.AddListener(HSVChanged);
		//slider.onValueChanged.AddListener(SliderChanged);
	}

	private void OnDestroy()
	{
		hsvpicker.onValueChanged.RemoveListener(ColorChanged);
		hsvpicker.onHSVChanged.RemoveListener(HSVChanged);
		//slider.onValueChanged.RemoveListener(SliderChanged);
	}

	private void ColorChanged(Color newColor)
	{
		listen = false;
		switch (type)
		{
			case ColorValues.R:
				slider.normalizedValue = newColor.r;
				break;
			case ColorValues.G:
				slider.normalizedValue = newColor.g;
				break;
			case ColorValues.B:
				slider.normalizedValue = newColor.b;
				break;
			case ColorValues.A:
				slider.normalizedValue = newColor.a;
				break;
			default:
				break;
		}
	}

	private void HSVChanged(float hue, float saturation, float value)
	{
		listen = false;
		switch (type)
		{
			case ColorValues.Hue:
				slider.normalizedValue = hue; //1 - hue;
				break;
			case ColorValues.Saturation:
				slider.normalizedValue = saturation;
				break;
			case ColorValues.Value:
				slider.normalizedValue = value;
				break;
			default:
				break;
		}
	}

	private void SliderChanged(float newValue)
	{
		if (listen)
		{
			newValue = slider.normalizedValue;
			//if (type == ColorValues.Hue)
			//	newValue = 1 - newValue;

			hsvpicker.AssignColor(type, newValue);
		}
		listen = true;
	}

    public void OnSliderUpdated(SliderEventData eventData)
    {
		/*if (listen)
		{
			//newValue = slider.normalizedValue;
			//if (type == ColorValues.Hue)
			//	newValue = 1 - newValue;

			hsvpicker.AssignColor(type, eventData.NewValue);
		}*/
		//hsvpicker.AssignColor(type, eventData.NewValue);
		//Color color = HSVUtil.ConvertHsvToRgb(eventData.NewValue * 360,  0.5, 1.0, 1.0f);
        Color color = hsvpicker.CurrentColor;
		Debug.Log(color);
		listen = true;
    }
}
