using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRaceManager : MonoBehaviour
{
    public Action OnRaceStart;
    public Action OnRaceEnd;

    [SerializeField]
    private ParticleSystem windParticleSystem;

    [SerializeField]
    public GameObject victoryScreen;

    [SerializeField]
    public GameObject lossScreen;

    private void Start()
    {
        //BeginRace();
    }

    public void BeginRace()
    {
        windParticleSystem.Play();
        OnRaceStart?.Invoke();
    }

    public void FinishRace(MarbleRaceMarble winningMarble)
    {
        // if winning marble is FE, victory screen, else failure screen
        if (winningMarble.IsPlayerMarble())
        {
            victoryScreen.SetActive(true);
        }
        else
        {
            lossScreen.SetActive(true);
        }
        OnRaceEnd?.Invoke();
    }
}
