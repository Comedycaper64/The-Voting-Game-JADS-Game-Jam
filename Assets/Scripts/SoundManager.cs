using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;

    [SerializeField]
    [Range(0f, 1f)]
    private float sfxVolume;

    [SerializeField]
    [Range(0f, 1f)]
    private float musicVolume;

    [SerializeField]
    private float musicQuietFactor;

    private void Awake()
    {
        SetUpSingleton();
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GetMusicVolume();
        SoundSlider.OnAnySoundSliderChanged += SoundSlider_OnAnySliderChanged;
    }

    private void OnDisable()
    {
        SoundSlider.OnAnySoundSliderChanged -= SoundSlider_OnAnySliderChanged;
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public float GetSoundEffectVolume()
    {
        return sfxVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        sfxVolume = volume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume * musicQuietFactor;
        audioSource.volume = musicVolume;
    }

    private void SoundSlider_OnAnySliderChanged(object sender, SliderStruct changedSlider)
    {
        switch (changedSlider.GetSoundSlider())
        {
            case SliderStruct.SoundType.Music:
                SetMusicVolume(changedSlider.GetValue());
                break;
            case SliderStruct.SoundType.SFX:
                SetSoundEffectVolume(changedSlider.GetValue());
                break;
        }
    }
}
