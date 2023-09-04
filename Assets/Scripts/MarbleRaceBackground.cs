using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRaceBackground : MonoBehaviour
{
    MarbleRaceManager marbleRaceManager;

    [SerializeField]
    private Vector3 scrollStartLocation;

    [SerializeField]
    private float xPositionThreshhold;

    private float backgroundMoveSpeed = 5f;
    private bool raceStarted = false;

    private void Awake()
    {
        marbleRaceManager = GetComponentInParent<MarbleRaceManager>();
        marbleRaceManager.OnRaceStart += ScrollBackground;
        marbleRaceManager.OnRaceEnd += RaceEnd;
    }

    private void OnDisable()
    {
        marbleRaceManager.OnRaceStart -= ScrollBackground;
        marbleRaceManager.OnRaceEnd += RaceEnd;
    }

    private void RaceEnd()
    {
        raceStarted = false;
    }

    private void Update()
    {
        if (raceStarted)
        {
            transform.Translate(Vector2.left * backgroundMoveSpeed * Time.deltaTime);
            if (transform.position.x < xPositionThreshhold)
            {
                transform.position = scrollStartLocation;
            }
        }
    }

    private void ScrollBackground()
    {
        raceStarted = true;
    }
}
