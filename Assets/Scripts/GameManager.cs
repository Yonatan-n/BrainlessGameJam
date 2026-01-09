using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTMP;
    private float countDown = 59;

    private List<string> _wordsTemplate;

    public List<string> WordsTemplate => _wordsTemplate;

    private int _currentWordIndex = 0;

    public string CurrentWordTemplate => WordsTemplate[_currentWordTemplateIndex];

    private int _currentWordTemplateIndex;

    [SerializeField]
    private float _wordsTemplateAnimDelay;

    private static GameManager _instance;

    private int _points = 0;

    public static GameManager Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
                if (_instance == null)
                {
                    Debug.Log("Error, Game Manager Instance not found in Scene");
                }
            }
            return _instance;

        }
    }

    void Awake()
    {
        UIManager._onWordDisappeared += UpdateCurrentWordTemplate;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO: randomize words
        // get a common english wordlist,
        // filter it abit
        // get 15 words
        // get 5 words from our dict (cya, brb, u2, 4ever)
        // combine both lists and randomize for a 20 words sentence.
        // mabybe 100 words just to be safe


        //Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.

        _wordsTemplate = new List<string>
                        { "lorem","ipsum","dolor","sit","amet","consetetur","sadipiscing","elitr","sed","diam"};

        UIManager.Initialize();
        _currentWordIndex = 0;
        _currentWordTemplateIndex = 0;
        StartCoroutine(UpdateAnimatedText(_wordsTemplateAnimDelay));

    }

    // Update is called once per frame
    void Update()
    {
        //countDown -= Time.deltaTime;
        /*float seconds = countDown % 60f;
        float minutes = Mathf.Floor(countDown / 60f);
        timerTMP.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);*/

        float seconds = countDown - PhoneController.Instance.MusicTime;
        float minutes = Mathf.Floor(countDown / 60f);

        timerTMP.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);


    }

    public void CheckPhoneText(string inputWord)
    {

        if (_currentWordTemplateIndex == _wordsTemplate.Count)
            return;

        int i = inputWord.Length - 1;

        if (_wordsTemplate[_currentWordTemplateIndex][i] == inputWord[i])
        {
            IncreasePoints(10);
        }


        if (inputWord == _wordsTemplate[_currentWordTemplateIndex] || inputWord.Length == _wordsTemplate[_currentWordTemplateIndex].Length)
        {
            StartCoroutine(ResetPhoneText());
            if (_currentWordTemplateIndex == _wordsTemplate.Count - 1)
            {
                GameOver();
            }

            _currentWordTemplateIndex++;

        }

    }

    private IEnumerator ResetPhoneText()
    {
        yield return new WaitForSeconds(0.2f);
        PhoneController.Instance.InputScreen.ResetText();
    }

    private void UpdateCurrentWordTemplate()
    {
        _currentWordTemplateIndex++;
    }

    private void GameOver()
    {

    }

    public void IncreasePoints(int points)
    {
        _points += points;
        UIManager.IncreasePoints(_points);
    }

    private IEnumerator UpdateAnimatedText(float interval)
    {
        int i = 0;
        while (i < _wordsTemplate.Count)
        {
            int j = 0;
            while (j < _wordsTemplate[i].Length)
            {
                UIManager.UpdateAnimatedText(_wordsTemplate[i], _wordsTemplate[_currentWordTemplateIndex] == _wordsTemplate[i], j++);
                yield return new WaitForSeconds(interval);
            }
            if (i < _wordsTemplate.Count - 1)
            {
                UIManager.UpdateAnimatedText("-", _wordsTemplate[_currentWordTemplateIndex] == _wordsTemplate[i], 0);
                yield return new WaitForSeconds(interval);
            }
            i++;
        }
    }
}
