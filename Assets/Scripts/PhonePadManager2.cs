

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PhonePadManager2 : MonoBehaviour
{
    private AudioSource source;
    List<AudioClip> loadedClips = new List<AudioClip>();
    // [SerializeField] Button btn1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        source = GetComponent<AudioSource>();
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

    public void PlaySound(int index)
    {
        source.PlayOneShot(loadedClips[index]);
    }

    void Update()
    {

    }
}
