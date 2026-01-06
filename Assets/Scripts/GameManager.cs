using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class GameManager : MonoBehaviour
{

    private int _currentSMSLevel = 0;
    private SMSLevelManager _smsLevelManager;
    private static GameManager _instance;

    private Stack<string> _incomingMessages = new Stack<string>();

    public Stack<string> IncomingMessages => _incomingMessages;

    public static GameManager Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        string[,] parsedCSV =    {{"Section 1", "Best Friend", "Hi are you on the bus? I don't see you here" ,
                                            "Did it pass `already`? I'm stil at the stop",
                                            "lol you missed it again :P", "30"},   // row 0
											
										 {"Section 1","Best Friend", "", "Can you cover for me? make up `something`?", "I got U", "30"},

                                         {"Section 1","Teacher", "Good morning, I'm running a bit late, can you tell the class to start reading at page 12?", "`4sure`", ":|", "30"},

                                         {"Section 1","Best Friend", "", "Teacher just msg me he is running late, tell the class to start reading at `page 12`", "Wat I already msg him dat U R in UR grama's funeral", "30"}

                                        };

        _smsLevelManager = new SMSLevelManager();
        _smsLevelManager.LoadLevelsFromCSV(parsedCSV);

        GameUIManager.Initialize();

        SubmitButton._onPhoneTextSubmitted += CheckSubmittedPhoneText;

    }

    public void Start()
    {
        StartCoroutine(UpdateIncomingMessage(_currentSMSLevel));
    }

    public IEnumerator UpdateIncomingMessage(int levelIndex)
    {
        string incomingMessage = _smsLevelManager.GetIncomingmessage(levelIndex);

        if (incomingMessage.Length > 0)
        {
            _incomingMessages.Push(incomingMessage);
            GameUIManager.ShowIncomingMessages(_incomingMessages);
            yield return new WaitForSeconds(1f);
        }
        GameUIManager.ShowResponseTemplate(_smsLevelManager.GetPlayerResponseTemplate(levelIndex));

    }

    public IEnumerator WaitForCharacterResponse(int levelIndex)
    {
        yield return new WaitForSeconds(3f);

        string incomingMessage = _smsLevelManager.GetSenderReply(levelIndex);
        _incomingMessages.Push(incomingMessage);
        GameUIManager.ShowIncomingMessages(_incomingMessages);

        _currentSMSLevel++;
        yield return UpdateIncomingMessage(_currentSMSLevel);
    }

    private void CheckSubmittedPhoneText()
    {
        var template = _smsLevelManager.GetPlayerResponseTemplate(_currentSMSLevel);

        //only check the submitted text once the template is displayed on screen
        if (template == "")
            return;

        var backTickIndex1 = template.IndexOf("`");
        bool hasBackTicks = backTickIndex1 != -1;

        //check if template has backticks ad update the template to the word in backticks
        if (hasBackTicks)
        {
            var backTickIndex2 = template.IndexOf("`", backTickIndex1 + 1);
            template = template.Substring(backTickIndex1 + 1, backTickIndex2 + 1 - (backTickIndex1 + 2));
        }
        Debug.Log("template: " + template);

        if (template == PhoneController.Instance.InputWord)
            OnLevelCompleted();
        else
            Debug.Log("You entered the wrong word!");
    }

    private void OnLevelCompleted()
    {
        Debug.Log("You entered the correct word!");
        GameUIManager.ShowResponseTemplate("SUCCESS!");
        StartCoroutine(WaitForCharacterResponse(_currentSMSLevel));
    }

    public void Update()
    {

    }

}