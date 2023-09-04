using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderStruct : MonoBehaviour
{
    private SoundType soundSlider;
    private float value;

    public enum SoundType
    {
        Master,
        Music,
        SFX,
    }

    public SliderStruct(SoundType slider, float value)
    {
        soundSlider = slider;
        this.value = value;
    }

    public SoundType GetSoundSlider()
    {
        return soundSlider;
    }

    public float GetValue()
    {
        return value;
    }
}
