using UnityEngine;
using TMPro;
public abstract class PhoneButton : MonoBehaviour
{

    public virtual void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}
