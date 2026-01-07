using UnityEngine;

public class Cusor : MonoBehaviour
{
    private bool show = true;
    private float blinkSeconds = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("blinkCursor", 0f, blinkSeconds);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void blinkCursor()
    {
        show = !show;
        gameObject.SetActive(show);
    }
}
