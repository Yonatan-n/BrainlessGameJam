using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class GameManager : MonoBehaviour
{

    private int _currentSMSLevel = 0;
    private SMSLevelManager _smsLevelManager;
    private static GameManager _instance;

    private LevelScript _levelScript;

    public LevelScript LevelScript => _levelScript;

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

        _levelScript = GetComponent<LevelScript>();
        _smsLevelManager = new SMSLevelManager();

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

        var parts = template.Split('`');

        if (parts[1] == PhoneController.Instance.InputWord)
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