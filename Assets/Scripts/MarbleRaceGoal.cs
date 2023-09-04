using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRaceGoal : MonoBehaviour
{
    MarbleRaceManager marbleRaceManager;
    private float goalMoveSpeed = 0.1f;
    private bool raceStarted = false;

    private void Awake()
    {
        marbleRaceManager = GetComponentInParent<MarbleRaceManager>();
        marbleRaceManager.OnRaceStart += MoveGoal;
        marbleRaceManager.OnRaceEnd += RaceEnd;
    }

    private void OnDisable()
    {
        marbleRaceManager.OnRaceStart -= MoveGoal;
        marbleRaceManager.OnRaceEnd -= RaceEnd;
    }

    private void RaceEnd()
    {
        raceStarted = false;
    }

    private void Update()
    {
        if (raceStarted)
        {
            transform.Translate(Vector2.left * goalMoveSpeed * Time.deltaTime);
        }
    }

    private void MoveGoal()
    {
        raceStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out MarbleRaceMarble winningMarble))
        {
            marbleRaceManager.FinishRace(winningMarble);
        }
    }
}
