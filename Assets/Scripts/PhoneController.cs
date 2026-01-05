using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _inputWord;

    private static PhoneController _instance;


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

}