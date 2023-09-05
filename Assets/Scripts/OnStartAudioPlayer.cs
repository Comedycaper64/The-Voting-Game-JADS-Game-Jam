using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    void Start()
    {
        float volume = 1f;
        if (SoundManager.Instance)
        {
            volume = SoundManager.Instance.GetSoundEffectVolume();
        }

        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
    }
}
