using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;

    private float sfxChangeSampleCooldown = 1f;
    private float sfxSampleTimer = 0f;

    [SerializeField]
    private AudioClip sfxChangeSample;

    [SerializeField]
    private AudioClip uiButtonPressSound;

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
        SetMusicVolume(GetMusicVolume());
        SoundSlider.OnAnySoundSliderChanged += SoundSlider_OnAnySliderChanged;
    }

    private void OnDisable()
    {
        SoundSlider.OnAnySoundSliderChanged -= SoundSlider_OnAnySliderChanged;
    }

    private void Update()
    {
        if (sfxSampleTimer > 0f)
        {
            sfxSampleTimer -= Time.deltaTime;
        }
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
        if (sfxSampleTimer <= 0f)
        {
            AudioSource.PlayClipAtPoint(sfxChangeSample, Camera.main.transform.position, sfxVolume);
            sfxSampleTimer = sfxChangeSampleCooldown;
        }
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioSource.volume = musicVolume * musicQuietFactor;
    }

    public void SetMusicTrack(AudioClip musicTrack)
    {
        audioSource.clip = musicTrack;
        audioSource.Play();
    }

    public void PlayUIButtonPress()
    {
        AudioSource.PlayClipAtPoint(
            uiButtonPressSound,
            Camera.main.transform.position,
            GetSoundEffectVolume()
        );
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
