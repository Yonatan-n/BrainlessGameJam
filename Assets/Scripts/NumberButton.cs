using UnityEngine;
using TMPro;
using System.Runtime.Serialization;

public class NumberButton : PhoneButton
{
    private TextMeshProUGUI _letters;
    private int _currentLetterIndex;
    private float _timeSinceLastButtonClick = 0.0f;

    [SerializeField]
    private float _fastTypeDelay = 0.25f;

    private bool _typeLetter = false;

    [SerializeField]
    private string number;

    private string _possibleChars;
    public string PossibleChars => _possibleChars;

    public override void Awake()
    {
        base.Awake();
        _letters = transform.Find("Text (TMP) (1)").gameObject.GetComponent<TextMeshProUGUI>();
        _possibleChars = _letters.text + number;
        _currentLetterIndex = 0;
    }

    public override void OnButtonClick()
    {
        // Debug.Log("Number Button clicked!");
        base.OnButtonClick();
        if (_timeSinceLastButtonClick > _fastTypeDelay)
        {
            _currentLetterIndex = 0;
        }
        else
        {
            _currentLetterIndex++;
            _currentLetterIndex = _currentLetterIndex % _possibleChars.Length;
            //_currentLetterIndex = _currentLetterIndex % _letters.text.Length;

        }
        _typeLetter = true;
    }

    public void Update()
    {
        if (_typeLetter)
        {
            PhoneController.Instance.TypeLetter(
                _possibleChars[_currentLetterIndex],
                _timeSinceLastButtonClick < _fastTypeDelay, gameObject);
            _typeLetter = false;
            _timeSinceLastButtonClick = 0.0f;
        }

        _timeSinceLastButtonClick += Time.deltaTime;

    }
}