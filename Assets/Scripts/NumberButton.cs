using UnityEngine;
using TMPro;

public class NumberButton : PhoneButton
{
    private TextMeshProUGUI _letters;
    private int _currentLetterIndex;
    private float _timeSinceLastButtonClick = 0.0f;

    [SerializeField]
    private float _fastTypeDelay = 0.25f;

    private bool _typeLetter = true;

    public override void Awake()
    {
        base.Awake();
        _letters = transform.Find("Text (TMP) (1)").gameObject.GetComponent<TextMeshProUGUI>();
        _currentLetterIndex = 0;
    }

    public override void OnButtonClick()
    {
        Debug.Log("Number Button clicked!");
        base.OnButtonClick();
        if (_timeSinceLastButtonClick >= _fastTypeDelay)
        {
            _currentLetterIndex = 0;
        }
        else
        {
            _currentLetterIndex++;
            _currentLetterIndex = _currentLetterIndex % _letters.text.Length;
        }
        _typeLetter = true;
    }

    public void Update()
    {
        if (_typeLetter)
        {
            PhoneController.Instance.TypeLetter(_letters.text[_currentLetterIndex], _timeSinceLastButtonClick < _fastTypeDelay);
            _typeLetter = false;
            _timeSinceLastButtonClick = 0.0f;
        }

        _timeSinceLastButtonClick += Time.deltaTime;

    }
}