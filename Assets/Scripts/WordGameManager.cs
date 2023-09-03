using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
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

    private void Start()
    {
        minigameCoroutine = StartCoroutine(SpawnLetters());
        WordGameLetter.OnLetterChosen += TestGivenLetter;
    }

    private void OnDisable()
    {
        WordGameLetter.OnLetterChosen -= TestGivenLetter;
    }

    private IEnumerator SpawnLetters()
    {
        int randomInt = Random.Range(0, 3);
        WordGameLetter wordGameLetter;
        float randomXLocation = Random.Range(-250f, 250f);
        float randomYLocation = Random.Range(-250f, 250f);
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
            }
        }
        else
        {
            letterScript.IncorrectLetter();
            //reduce time
        }
    }
}
