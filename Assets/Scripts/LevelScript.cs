using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {

    }
}


public enum Charecter
{
    BestFriend,
    Teacher,
    Mom,
    Frenemy,
    Crush,
    Dog,
}
public class StoryRow
{
    public int id;
    public int section;
    public Charecter charecter;
    public string incomingSMS;
    public string replyTemplate;
    public string replySMS;
    public float time;
}