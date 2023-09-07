using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressSound : MonoBehaviour
{
    private void Start()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(() =>
            {
                PlayButtonPress();
            });
        }
    }

    public void PlayButtonPress()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayUIButtonPress();
        }
    }
}
