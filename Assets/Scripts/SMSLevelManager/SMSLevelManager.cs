using UnityEngine;
using System.Collections.Generic;
class SMSLevelManager
{

    //returns all levels by sectionID
    public List<StoryRow> GetLevelSection(int section)
    {
        return GameManager.Instance.LevelScript.section1.FindAll(level => level.section == section);
    }

    public string GetIncomingmessage(int lvlIndex)
    {
        if (GameManager.Instance.LevelScript.section1[lvlIndex].incomingSMS.Length == 0)
            return "";
        return string.Format("{0} : {1}", GameManager.Instance.LevelScript.section1[lvlIndex].charecter,
        GameManager.Instance.LevelScript.section1[lvlIndex].incomingSMS);
    }

    public string GetPlayerResponseTemplate(int lvlIndex)
    {
        return string.Format("{0}", GameManager.Instance.LevelScript.section1[lvlIndex].replyTemplate);

    }

    public string GetSenderReply(int lvlIndex)
    {
        return string.Format("{0} : {1}", GameManager.Instance.LevelScript.section1[lvlIndex].charecter,
         GameManager.Instance.LevelScript.section1[lvlIndex].replySMS);
    }
}

/*struct SMSLevel
{
    public int SectionID;
    public string Sender;
    public string IncomingMessage;
    public string ResponseTemplate;
    public string SenderReply;
    public int Time;

    public SMSLevel(string sectionID, string sender, string incomingMessage, string responseTemplate,
                    string senderReply, string time)
    {
        SectionID = sectionID[sectionID.Length - 1] - '0';
        Sender = sender;
        IncomingMessage = incomingMessage;
        ResponseTemplate = responseTemplate;
        SenderReply = senderReply;
        Time = int.Parse(time);
    }
}*/

