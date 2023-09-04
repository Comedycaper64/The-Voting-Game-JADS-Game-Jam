using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField]
    private SliderStruct.SoundType sliderType;

    public static event EventHandler<SliderStruct> OnAnySoundSliderChanged;

    private void Start()
    {
        if (!SoundManager.Instance)
        {
            return;
        }

        switch (sliderType)
        {
            case SliderStruct.SoundType.Music:
                GetComponent<Slider>().value = SoundManager.Instance.GetMusicVolume();
                break;
            case SliderStruct.SoundType.SFX:
                GetComponent<Slider>().value = SoundManager.Instance.GetSoundEffectVolume();
                break;
        }
    }

    //Invokes an event when a slider is moved that the Sound Manager is subscribed to. Causes sound settings to be updated
    public void OnSliderChanged()
    {
        OnAnySoundSliderChanged?.Invoke(
            this,
            new SliderStruct(sliderType, GetComponent<Slider>().value)
        );
    }
}
