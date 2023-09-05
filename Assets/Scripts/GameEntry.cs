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
    private string gameName;

    [SerializeField]
    public EntryType entryType = EntryType.game;

    [SerializeField]
    private TextMeshProUGUI gameNameText;

    [SerializeField]
    private TextMeshProUGUI upvoteText;

    [SerializeField]
    private Image upvoteSprite;

    [SerializeField]
    private Image downvoteSprite;

    [SerializeField]
    private GameObject cutSprite;

    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private Color upvotedColor;

    [SerializeField]
    private bool upvoted;

    [SerializeField]
    private bool downvoted;

    public enum EntryType
    {
        game,
        bomb,
        angel,
        defusalFairy,
    }

    private void Awake()
    {
        upvoteText.text = upvoteAmount.ToString();
        gameNameText.text = gameName;
    }

    private void Start()
    {
        if (upvoted)
        {
            upvoteSprite.color = upvotedColor;
        }
        else
        {
            upvoteSprite.color = defaultColor;
        }

        if (downvoted)
        {
            downvoteSprite.color = upvotedColor;
        }
        else
        {
            downvoteSprite.color = defaultColor;
        }
    }

    public void UpvoteEntry()
    {
        if (!upvoted)
        {
            upvoteAmount++;
            upvoteSprite.color = upvotedColor;
            upvoted = true;
        }
        else
        {
            upvoteAmount--;
            upvoteSprite.color = defaultColor;
            upvoted = false;
        }
        upvoteText.text = upvoteAmount.ToString();
        OnEntryNumberChanged?.Invoke();
    }

    public void DownvoteEntry()
    {
        if (!downvoted)
        {
            upvoteAmount--;
            downvoteSprite.color = upvotedColor;
            downvoted = true;
        }
        else
        {
            upvoteAmount++;
            downvoteSprite.color = defaultColor;
            downvoted = false;
        }
        upvoteText.text = upvoteAmount.ToString();
        OnEntryNumberChanged?.Invoke();
    }

    public int GetUpvoteAmount()
    {
        return upvoteAmount;
    }

    public void ToggleCutSprite(bool toggle)
    {
        cutSprite.SetActive(toggle);
    }
}
