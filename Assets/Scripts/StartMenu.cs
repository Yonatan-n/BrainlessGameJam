using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button options;
    [SerializeField] Button quit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (start != null)
        {
            start.onClick.AddListener(startGame);
        }
        if (options != null)
        {
            options.onClick.AddListener(OpenOptions);
        }
        if (quit != null)
        {
            quit.onClick.AddListener(QuitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startGame()
    {
        Debug.Log("game start");
    }
    void OpenOptions()
    {
        Debug.Log("options");
    }


    void QuitGame()
    {
        Application.Quit();
    }
}
