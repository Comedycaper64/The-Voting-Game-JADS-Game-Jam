using System;
using System.Collections;
using System.Collections.Generic;
using TheraBytes.BetterUi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordGameLetter : MonoBehaviour
{
    public static EventHandler<string> OnLetterChosen;

    [SerializeField]
    private TextMeshProUGUI letterText;

    [SerializeField]
    private Image backgroundImage;
    private Vector2 movementDirection;
    private float movementSpeed = 0.5f;
    private bool moving = false;
    private float lifetime;
    private float maxLifetime = 5f;

    [SerializeField]
    private AudioClip correctLetterSFX;

    [SerializeField]
    private AudioClip wrongLetterSFX;

    private void Update()
    {
        if (moving)
        {
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
            lifetime -= Time.deltaTime;
            backgroundImage.color = new Color(1, 1, 1, lifetime / maxLifetime);
            letterText.color = new Color(0, 0, 0, lifetime / maxLifetime);
            if (lifetime < 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetLetter(string newText)
    {
        letterText.text = newText;
    }

    public void ChooseLetter()
    {
        OnLetterChosen?.Invoke(this, letterText.text);
    }

    public void CreateLetter()
    {
        movementDirection = new Vector2(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f)
        );
        lifetime = maxLifetime;
        moving = true;
    }

    public void CorrectLetter(RectTransform letterAnchor)
    {
        PlayButtonPress(true);
        backgroundImage.color = new Color(1, 1, 1, 1);
        letterText.color = new Color(0, 0, 0, 1);
        moving = false;
        GetComponent<AnchorOverride>().CurrentAnchors.Elements[0].Reference = letterAnchor;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3();
        rectTransform.offsetMax = new Vector2();
        rectTransform.offsetMin = new Vector2();
    }

    public void IncorrectLetter()
    {
        PlayButtonPress(false);
    }

    public void SetPosition(Vector3 newPosition)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = newPosition;
    }

    public void PlayButtonPress(bool correct)
    {
        if (SoundManager.Instance)
        {
            if (correct)
            {
                AudioSource.PlayClipAtPoint(
                    correctLetterSFX,
                    Camera.main.transform.position,
                    SoundManager.Instance.GetSoundEffectVolume()
                );
            }
            else
            {
                AudioSource.PlayClipAtPoint(
                    wrongLetterSFX,
                    Camera.main.transform.position,
                    SoundManager.Instance.GetSoundEffectVolume()
                );
            }
        }
    }
}
