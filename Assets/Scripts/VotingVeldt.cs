using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheraBytes.BetterUi;
using UnityEngine;

public class VotingVeldt : MonoBehaviour
{
    private List<GameEntry> gameEntries = new List<GameEntry>();
    private Dictionary<GameEntry, GameObject> entryToLayoutChild =
        new Dictionary<GameEntry, GameObject>();

    [SerializeField]
    private Transform gameEntryLayoutGroup;

    [SerializeField]
    private GameObject gameEntryLocatorPrefab;

    private void Awake()
    {
        GameEntry.OnEntryNumberChanged += UpdateVotingArea;
        SetupVotingArea();
    }

    private void OnDisable()
    {
        GameEntry.OnEntryNumberChanged -= UpdateVotingArea;
    }

    public void SetupVotingArea()
    {
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
        }
    }
}
