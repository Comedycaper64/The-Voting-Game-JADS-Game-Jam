using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRaceManager : MonoBehaviour
{
    public Action OnRaceStart;
    public Action OnRaceEnd;

    private void Start()
    {
        BeginRace();
    }

    public void BeginRace()
    {
        OnRaceStart?.Invoke();
    }

    public void FinishRace(MarbleRaceMarble winningMarble)
    {
        // if winning marble is FE, victory screen, else failure screen
        if (winningMarble.IsPlayerMarble())
        {
            Debug.Log("ayaya");
        }
        OnRaceEnd?.Invoke();
    }
}
