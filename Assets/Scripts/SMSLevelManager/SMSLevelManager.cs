using UnityEngine;
using System.Collections.Generic;
class SMSLevelManager
{
    public int LevelCount => _smsLevels.Count;

    private List<SMSLevel> _smsLevels;

    public SMSLevelManager()
    {
        _smsLevels = new List<SMSLevel>();
    }

    public void LoadLevelsFromCSV(string[,] parsedCSV)
    {
        int rows = parsedCSV.GetLength(0);    // number of rows

        for (int i = 0; i < rows; i++)
        {
            _smsLevels.Add(new SMSLevel(parsedCSV[i, 0], parsedCSV[i, 1], parsedCSV[i, 2], parsedCSV[i, 3], parsedCSV[i, 4], parsedCSV[i, 5]));
        }
    }

    //returns all levels by sectionID
    public List<SMSLevel> GetLevelSection(int sectionID)
    {
        return _smsLevels.FindAll(level => level.SectionID == sectionID);
    }

    public string GetIncomingmessage(int lvlIndex)
    {
        if (_smsLevels[lvlIndex].IncomingMessage.Length == 0)
            return "";
        return string.Format("{0} : {1}", _smsLevels[lvlIndex].Sender, _smsLevels[lvlIndex].IncomingMessage);
    }

    public string GetPlayerResponseTemplate(int lvlIndex)
    {
        return string.Format("{0}", _smsLevels[lvlIndex].ResponseTemplate);

    }

    public string GetSenderReply(int lvlIndex)
    {
        return string.Format("{0} : {1}", _smsLevels[lvlIndex].Sender, _smsLevels[lvlIndex].SenderReply);
    }
}

struct SMSLevel
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
}