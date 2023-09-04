using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    private float timeLimit = 30f;

    private float timeTracker = 0f;
    private bool gameActive;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private GameObject wordGameLetterPrefab;

    [SerializeField]
    private List<RectTransform> blankLetterTransforms = new List<RectTransform>();

    [SerializeField]
    private List<string> missingLetters = new List<string>();

    [SerializeField]
    private List<string> randomLetters = new List<string>();
    private int currentMissingLetterIndex = 0;
    private float timeBetweenLetterSpawn = 0.5f;
    private Coroutine minigameCoroutine;

    [SerializeField]
    private Transform victoryScreen;

    [SerializeField]
    private Transform lossScreen;

    private void Start()
    {
        //StartWordGame();
        WordGameLetter.OnLetterChosen += TestGivenLetter;
    }

    private void OnDisable()
    {
        WordGameLetter.OnLetterChosen -= TestGivenLetter;
    }

    private void Update()
    {
        if ((timeTracker > 0f) && gameActive)
        {
            timeTracker -= Time.deltaTime;
            timeText.text = timeTracker.ToString("F2");
            if (timeTracker <= 0f)
            {
                gameActive = false;
                StopCoroutine(minigameCoroutine);
                EndWordGame(false);
            }
        }
    }

    public void StartWordGame()
    {
        timeTracker = timeLimit;
        gameActive = true;
        minigameCoroutine = StartCoroutine(SpawnLetters());
    }

    private IEnumerator SpawnLetters()
    {
        int randomInt = Random.Range(0, 3);
        WordGameLetter wordGameLetter;
        float randomXLocation = Random.Range(-350f, 350f);
        float randomYLocation = Random.Range(-180f, 250f);
        if (randomInt > 0)
        {
            int randomMissingLetter = Random.Range(0, missingLetters.Count);
            wordGameLetter = Instantiate(wordGameLetterPrefab, transform)
                .GetComponent<WordGameLetter>();
            wordGameLetter.SetLetter(missingLetters[randomMissingLetter]);
        }
        else
        {
            int randomLetter = Random.Range(0, randomLetters.Count);
            wordGameLetter = Instantiate(wordGameLetterPrefab, gameObject.transform)
                .GetComponent<WordGameLetter>();
            wordGameLetter.SetLetter(randomLetters[randomLetter]);
        }
        //wordGameLetter.transform = new Vector3(0, 0, 0);
        wordGameLetter.SetPosition(new Vector3(randomXLocation, randomYLocation, 0));
        wordGameLetter.CreateLetter();

        yield return new WaitForSeconds(timeBetweenLetterSpawn);
        minigameCoroutine = StartCoroutine(SpawnLetters());
    }

    private void TestGivenLetter(object sender, string letter)
    {
        WordGameLetter letterScript = sender as WordGameLetter;
        if (letter.Equals(missingLetters[currentMissingLetterIndex]))
        {
            letterScript.CorrectLetter(blankLetterTransforms[currentMissingLetterIndex]);
            currentMissingLetterIndex++;
            if (currentMissingLetterIndex >= missingLetters.Count)
            {
                //finish minigame
                StopCoroutine(minigameCoroutine);
                gameActive = false;
                EndWordGame(true);
            }
        }
        else
        {
            letterScript.IncorrectLetter();
            timeTracker -= 1f;
            //reduce time
        }
    }

    private void EndWordGame(bool wonWordGame)
    {
        if (wonWordGame)
        {
            victoryScreen.gameObject.SetActive(true);
        }
        else
        {
            lossScreen.gameObject.SetActive(true);
        }
    }
}
