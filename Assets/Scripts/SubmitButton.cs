using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class SubmitButton : PhoneButton
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enabled = false;
    }

    public override void OnButtonClick()
    {

        base.OnButtonClick();
        if (PhoneController.Instance.InputWord.Length > 0)
        {
            GameManager.Instance.ResetInput(PhoneController.Instance.InputWord);
            GameManager.Instance.NextWord();
        }
    }

    public override void Update()
    {
        base.Update();
        if (PhoneController.Instance.InputWord.Length > 0 && enabled == false)
        {
            enabled = true;
        }
    }
}
