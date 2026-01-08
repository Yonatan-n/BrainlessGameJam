using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTMP;
    private float countDown = 59;

    [SerializeField]
    private List<string> _wordsTemplate;


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
        UIManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        float seconds = countDown % 60f;
        float minutes = Mathf.Floor(countDown / 60f);
        timerTMP.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
