using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheraBytes.BetterUi;
using TMPro;
using UnityEngine;

public class VotingVeldt : MonoBehaviour
{
    public static Action OnVotingFinished;

    [SerializeField]
    private float timeLimit = 10f;

    private float timeTracker = 0f;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private GameEntry targetGameEntry;

    [SerializeField]
    private List<int> safePositions = new List<int>();

    [SerializeField]
    private bool alternateWinCondition = false;

    [SerializeField]
    private int alternateWinVoteAmount;

    private List<GameEntry> gameEntries = new List<GameEntry>();
    private Dictionary<GameEntry, GameObject> entryToLayoutChild =
        new Dictionary<GameEntry, GameObject>();

    [SerializeField]
    private Transform gameEntryLayoutGroup;

    [SerializeField]
    private GameObject gameEntryLocatorPrefab;

    [SerializeField]
    private Transform victoryScreen;

    [SerializeField]
    private Transform lossScreen;

    private void Awake()
    {
        GameEntry.OnEntryNumberChanged += UpdateVotingArea;
        //SetupVotingArea();
    }

    private void OnDisable()
    {
        GameEntry.OnEntryNumberChanged -= UpdateVotingArea;
    }

    private void Update()
    {
        if (timeTracker > 0f)
        {
            timeTracker -= Time.deltaTime;
            timeText.text = timeTracker.ToString("F2");
            if (timeTracker <= 0f)
            {
                timeText.text = "0.00";
                OnVotingFinished?.Invoke();
                if (!alternateWinCondition)
                {
                    if (CheckIfSafeFromCulling(targetGameEntry))
                    {
                        victoryScreen.gameObject.SetActive(true);
                    }
                    else
                    {
                        lossScreen.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (targetGameEntry.GetUpvoteAmount() == alternateWinVoteAmount)
                    {
                        victoryScreen.gameObject.SetActive(true);
                    }
                    else
                    {
                        lossScreen.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void SetupVotingArea()
    {
        timeTracker = timeLimit;
        gameEntries = GetComponentsInChildren<GameEntry>().ToList();
        foreach (GameEntry gameEntry in gameEntries)
        {
            int upvoteAmount = gameEntry.GetUpvoteAmount();
            GameObject newObject = Instantiate(gameEntryLocatorPrefab, gameEntryLayoutGroup);
            newObject.name = gameEntry.name;
            //newObject.transform.SetSiblingIndex(upvoteAmount * 10);
            entryToLayoutChild.Add(gameEntry, newObject);
            gameEntry.GetComponent<AnchorOverride>().CurrentAnchors.Elements[0].Reference =
                newObject.GetComponent<RectTransform>();
        }
        UpdateVotingArea();
    }

    private void UpdateVotingArea()
    {
        List<GameEntry> tempList = gameEntries;
        tempList.Sort((GameEntry a, GameEntry b) => b.GetUpvoteAmount() - a.GetUpvoteAmount());
        for (int i = 0; i < tempList.Count; i++)
        {
            entryToLayoutChild[tempList[i]].transform.SetSiblingIndex(i);
            if (!alternateWinCondition)
            {
                if (CheckIfSafeFromCulling(tempList[i]))
                {
                    tempList[i].ToggleCutSprite(false);
                }
                else
                {
                    tempList[i].ToggleCutSprite(true);
                }
            }
        }
    }

    private bool CheckIfSafeFromCulling(GameEntry gameEntry)
    {
        List<GameEntry> tempList = gameEntries;
        tempList.Sort((GameEntry a, GameEntry b) => b.GetUpvoteAmount() - a.GetUpvoteAmount());
        int targetGameIndex = tempList.IndexOf(gameEntry);

        bool bombAdjacent = false;
        GameEntry bomb = tempList.Find(
            bombEntry => bombEntry.entryType == GameEntry.EntryType.bomb
        );
        if ((bomb != null) && (Mathf.Abs(tempList.IndexOf(bomb) - targetGameIndex) == 1))
        {
            bombAdjacent = true;
        }

        bool angelAdjacent = false;
        GameEntry angel = tempList.Find(
            angelEntry => angelEntry.entryType == GameEntry.EntryType.angel
        );
        if ((angel != null) && (Mathf.Abs(tempList.IndexOf(angel) - targetGameIndex) == 1))
        {
            angelAdjacent = true;
        }

        if (angelAdjacent || (safePositions.Contains(targetGameIndex) && !bombAdjacent))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
