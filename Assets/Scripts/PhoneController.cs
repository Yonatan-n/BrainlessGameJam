using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class PhoneController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _inputWord;

    private static PhoneController _instance;

    private float _removeTextDelay = 0.1f;
    private float _removeTextTimer = 0.0f;
    [SerializeField] AudioSource audioSource;
    private List<AudioClip> loadedClips = new List<AudioClip>();
    private List<string> getIndexOfButton = new List<string>{
        "Pad 1", "Pad 2", "Pad 3",
        "Pad 4", "Pad 5", "Pad 6",
        "Pad 7", "Pad 8", "Pad 9",
        "Pad 0", "Asterisk", "Hashtag",
    };

    [SerializeField]
    private InputScreen _inputScreen;
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

        audioSource = GetComponent<AudioSource>();
        var clipsByOrder = new List<string> {
            "Sounds/button1",
            "Sounds/button2",
            "Sounds/button3",
            "Sounds/button4",
            "Sounds/button5",
            "Sounds/button6",
            "Sounds/button7",
            "Sounds/button8",
            "Sounds/button9",
            "Sounds/button0",
            "Sounds/button_star",
            "Sounds/button#",
        };
        foreach (var path in clipsByOrder)
        {
            AudioClip clip = Resources.Load<AudioClip>(path);
            loadedClips.Add(clip);
        }
    }

    public void TypeLetter(char letter, bool fastTyping, GameObject Phonebutton)
    {
        playSound(Phonebutton);
        if (!fastTyping || _inputWord.text.Length == 0)
        {
            if (letter == ' ')
                _inputScreen.ShiftLeft();
            else
            {
                _inputWord.text += letter;
                _inputScreen.UpdateCursor(false, true);
            }

        }
        else if (fastTyping)
        {
            char[] chars = _inputWord.text.ToCharArray();
            chars[_inputWord.text.Length - 1] = letter;
            string result = new string(chars);
            _inputWord.text = result;
            _inputScreen.UpdateCursor(false, false);
        }
        else
        {
            if (letter == ' ')
                _inputScreen.ShiftLeft();
            else
            {
                _inputWord.text += letter;
                _inputScreen.UpdateCursor(true, false);
            }
        }

    }

    public void RemoveText(float clickTimer = 0.0f)
    {
        if (_inputWord.text.Length > 0)
        {
            if (clickTimer <= 0.0f)
            {
                _inputWord.text = _inputWord.text.Remove(_inputWord.text.Length - 1);
                _inputScreen.UpdateCursor(true, false);
            }

            else if (clickTimer > 1.0f)
            {
                _removeTextTimer += Time.deltaTime;
                if (_removeTextTimer > _removeTextDelay)
                {
                    _inputWord.text = _inputWord.text.Remove(_inputWord.text.Length - 1);
                    _removeTextTimer = 0.0f;
                    _inputScreen.UpdateCursor(true, false);
                }
                _removeTextDelay -= clickTimer * Time.deltaTime * 0.04f;

            }
        }

    }

    public void playSound(GameObject Phonebutton)
    {
        var index = getIndexOfButton.IndexOf(Phonebutton.name);
        audioSource.PlayOneShot(loadedClips[index]);
    }
}