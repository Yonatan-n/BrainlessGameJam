using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhonePad : MonoBehaviour
{
    private List<string> getIndexOfButton = new List<string>{
    "Pad 1",
    "Pad 2",
    "Pad 3",
    "Pad 4",
    "Pad 5",
    "Pad 6",
    "Pad 7",
    "Pad 8",
    "Pad 9",
    "Pad 0",
    "Asterisk",
    "Hashtag",
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(soundOnClick);
    }

    void soundOnClick()
    {
        PhonePadManager padManagerScript = GetComponentInParent<PhonePadManager>();
        padManagerScript.PlaySound(getIndexOfButton.IndexOf(gameObject.name));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
