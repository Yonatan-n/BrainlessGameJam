using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _inputWord;

    public string InputWord => _inputWord.text;

    private static PhoneController _instance;

    private float _removeTextDelay = 0.1f;
    private float _removeTextTimer = 0.0f;

    public static PhoneController Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindFirstObjectByType<PhoneController>();
                if (_instance == null)
                {
                    Debug.Log("Error, Phone Controller Instance not found in Scene");
                }
            }
            return _instance;

        }
    }

    private void Awake()
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
    }

    private void Start()
    {
        _inputWord.text = "";
    }

    public void TypeLetter(char letter, bool fastTyping)
    {
        if (!fastTyping || _inputWord.text.Length == 0)
        {
            _inputWord.text += letter;
        }
        else if (fastTyping)
        {
            char[] chars = _inputWord.text.ToCharArray();
            chars[_inputWord.text.Length - 1] = letter;
            string result = new string(chars);
            _inputWord.text = result;
        }
        else
        {
            _inputWord.text += letter;
        }
    }

    public void RemoveText(float clickTimer = 0.0f)
    {
        if (_inputWord.text.Length > 0)
        {
            if (clickTimer <= 0.0f)
            {
                _inputWord.text = _inputWord.text.Remove(_inputWord.text.Length - 1);
            }

            else if (clickTimer > 1.0f)
            {
                _removeTextTimer += Time.deltaTime;
                if (_removeTextTimer > _removeTextDelay)
                {
                    _inputWord.text = _inputWord.text.Remove(_inputWord.text.Length - 1);
                    _removeTextTimer = 0.0f;
                }
                _removeTextDelay -= clickTimer * Time.deltaTime * 0.04f;
            }
        }

    }
}