using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public static class GameUIManager
{

    private static GameObject _incomingMessagesContainer;
    private static GameObject _messageSenders;
    private static GameObject _responseTemplateContainer;

    public static void Initialize()
    {
        _incomingMessagesContainer = GameObject.Find("Canvas/MessagePanel/Incoming Messages");
        _messageSenders = GameObject.Find("Canvas/MessagePanel/Senders");
        _responseTemplateContainer = GameObject.Find("Canvas/ResponseTemplatePanel/ResponseTemplate");
    }

    public static void ShowIncomingMessages(Stack<string> incomingMessages)
    {
        _incomingMessagesContainer.GetComponent<TextMeshProUGUI>().text = "";
        _messageSenders.GetComponent<TextMeshProUGUI>().text = "";
        foreach (var incomingMessage in incomingMessages)
        {
            var sender = incomingMessage.Substring(0, incomingMessage.IndexOf(":") + 1);
            var message = incomingMessage.Substring(incomingMessage.IndexOf(":") + 2);
            _incomingMessagesContainer.GetComponent<TextMeshProUGUI>().text += message + "\n";
            _messageSenders.GetComponent<TextMeshProUGUI>().text += sender + "\n";
            if (message.Length > 45)
                _messageSenders.GetComponent<TextMeshProUGUI>().text += "\n";
        }

    }

    public static void ShowResponseTemplate(string responseTemplate)
    {
        _responseTemplateContainer.GetComponent<TextMeshProUGUI>().text = responseTemplate;
    }

}