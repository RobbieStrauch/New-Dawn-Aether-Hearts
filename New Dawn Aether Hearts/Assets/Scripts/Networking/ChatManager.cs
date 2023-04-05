// Reference: https://www.youtube.com/watch?v=IRAeJgGkjHk&t=602s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;
    public string username;
    public string inputMessage;
    public string usernameStructure;
    public int maxMessages = 20;
    public int maxUsernameLength = 20;
    public bool sentMessage;

    public GameObject contentPanel;
    public GameObject textPrefab;
    public TMP_InputField inputField;

    public Color myMessageColor;
    public Color otherMessageColor;
    public Color infoMessageColor;

    List<Message> messageList = new List<Message>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        username = string.Empty;
        sentMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChatClient.instance.usernameInput)
        {
            if (username.Length > maxUsernameLength)
            {
                username = string.Empty;
                ChatClient.instance.usernameInput = false;
            }
            else
            {
                username = ChatClient.instance.username;
                usernameStructure = "[" + username + "]: ";
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && inputField.text != string.Empty)
        {
            inputMessage = inputField.text;
            sentMessage = true;
            inputField.text = string.Empty;
        }
    }

    public void SendChatMessage(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages) 
        {
            Destroy(messageList[0].textTMP.gameObject);
            messageList.Remove(messageList[0]);
        }

        int counter = 0;
        if (usernameStructure.Length <= text.Length)
        {
            for (int i = 0; i < usernameStructure.Length; i++)
            {
                if (usernameStructure[i] == text[i])
                {
                    counter++;
                }
            }
            if (counter == usernameStructure.Length)
            {
                messageType = Message.MessageType.MyMessage;
            }
            if (text[0] != '[')
            {
                messageType = Message.MessageType.InfoMessage;
            }
        }

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(textPrefab, contentPanel.transform);
        newMessage.textTMP = newText.GetComponent<TMP_Text>();
        newMessage.textTMP.text = newMessage.text;
        newMessage.textTMP.color = MessageTypeColor(messageType);
        messageList.Add(newMessage);
    }

    public Color MessageTypeColor(Message.MessageType messageType)
    {
        Color currentColor = Color.white;

        switch (messageType)
        {
            case Message.MessageType.MyMessage:
                currentColor = myMessageColor;
                break;
            case Message.MessageType.OtherMessage:
                currentColor = otherMessageColor;
                break;
            case Message.MessageType.InfoMessage:
                currentColor = infoMessageColor;
                break;
            default:
                break;
        }

        return currentColor;
    }
}

public class Message
{
    public string text;
    public TMP_Text textTMP;
    public MessageType messageType;

    public enum MessageType
    {
        MyMessage,
        OtherMessage,
        InfoMessage
    }
}
