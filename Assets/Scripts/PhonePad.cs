using UnityEngine;
using UnityEngine.UI;

public class PhonePad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(soundOnClick);
    }

    void soundOnClick()
    {
        PhonePadManager padManagerScript = GetComponentInParent<PhonePadManager>();
        padManagerScript.PlaySound(0);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
