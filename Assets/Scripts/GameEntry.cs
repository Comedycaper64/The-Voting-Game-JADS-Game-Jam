using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    public static event Action OnEntryNumberChanged;

    [SerializeField]
    private int upvoteAmount;

    [SerializeField]
    private TextMeshProUGUI upvoteText;

    [SerializeField]
    private Image upvoteSprite;

    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Sprite upvotedSprite;

    [SerializeField]
    private bool upvoted;

    private void Awake()
    {
        upvoteText.text = upvoteAmount.ToString();
    }

    private void Start()
    {
        if (upvoted)
        {
            upvoteSprite.sprite = upvotedSprite;
        }
        else
        {
            upvoteSprite.sprite = defaultSprite;
        }
    }

    public void UpvoteEntry()
    {
        if (!upvoted)
        {
            upvoteAmount++;
            upvoteSprite.sprite = upvotedSprite;
            upvoted = true;
        }
        else
        {
            upvoteAmount--;
            upvoteSprite.sprite = defaultSprite;
            upvoted = false;
        }
        upvoteText.text = upvoteAmount.ToString();
        OnEntryNumberChanged?.Invoke();
    }

    public int GetUpvoteAmount()
    {
        return upvoteAmount;
    }
}
