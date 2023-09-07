using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleRaceMarble : MonoBehaviour
{
    MarbleRaceManager marbleRaceManager;

    [SerializeField]
    private float speedModifier;

    [SerializeField]
    private Transform marbleVisual;

    [SerializeField]
    private bool playerMarble = false;
    private float marbleMoveSpeed = 0.1f;
    private float marbleMaxMoveSpeed = 0.5f;
    private bool raceStarted = false;

    [SerializeField]
    private float marbleDropTime = 999f;
    private float timer = 0f;

    [SerializeField]
    private AudioClip dropAudioClip;

    private void Awake()
    {
        marbleRaceManager = GetComponentInParent<MarbleRaceManager>();
        marbleRaceManager.OnRaceStart += RaceMarble;
        marbleRaceManager.OnRaceEnd += RaceEnd;
    }

    private void OnDisable()
    {
        marbleRaceManager.OnRaceStart -= RaceMarble;
        marbleRaceManager.OnRaceEnd -= RaceEnd;
    }

    private void Update()
    {
        if (raceStarted)
        {
            timer += Time.deltaTime;

            marbleVisual.Rotate(
                Vector3.back * 350f * speedModifier * marbleMoveSpeed * Time.deltaTime
            );

            if (timer >= marbleDropTime)
            {
                gameObject.AddComponent<Rigidbody2D>();
                if (dropAudioClip && SoundManager.Instance)
                {
                    AudioSource.PlayClipAtPoint(
                        dropAudioClip,
                        Camera.main.transform.position,
                        SoundManager.Instance.GetSoundEffectVolume()
                    );
                }
                RaceEnd();
                Destroy(gameObject, 3f);
            }

            transform.Translate(Vector2.right * marbleMoveSpeed * speedModifier * Time.deltaTime);
            if (playerMarble && Input.GetMouseButtonDown(0))
            {
                marbleMoveSpeed = Mathf.Min(marbleMoveSpeed + 0.05f, marbleMaxMoveSpeed);
            }
            if (marbleMoveSpeed >= 0.1f)
            {
                marbleMoveSpeed -= 0.2f * Time.deltaTime;
            }
        }
    }

    private void RaceMarble()
    {
        raceStarted = true;
        timer = 0f;
    }

    private void RaceEnd()
    {
        raceStarted = false;
    }

    public bool IsPlayerMarble()
    {
        return playerMarble;
    }
}
