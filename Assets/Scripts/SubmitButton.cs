using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SubmitButton : PhoneButton
{

    public static event Action _onPhoneTextSubmitted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public override void OnButtonClick()
    {
        Debug.Log("Submit Button clicked!");
        _onPhoneTextSubmitted?.Invoke();

    }
}
