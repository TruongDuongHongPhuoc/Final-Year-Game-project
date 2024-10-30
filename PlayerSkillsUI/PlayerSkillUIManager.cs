using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class PlayerSkillUIManager : MonoBehaviour
{
    //UDP client
    public bool isUsingCV;
    private UdpClient udpClient;
    public string receivedMessage;
    public bool isRequireInput = false;  // Set this flag as needed
    public bool isGetFirstValue = false;
    //Gather input as tranditional
    TurnBaseManager turnBaseManager;
    UnitBase playerUnit;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] public GameObject UIcontainer;
    private void Start()
    {
        turnBaseManager = GameObject.Find("TurnBaseManager").GetComponent<TurnBaseManager>();
        playerUnit = turnBaseManager.PlayerCharacter;
        DisplaySKills();
        //UDP start
        if (isUsingCV)
        {
            // udpClient = new UdpClient();
            udpClient = UDP_Client.client;
            SendInitialMessage();  // Send initial message to get Python's attention
            StartCoroutine(startSending()); // iteractive sending signals
        }
    }
    private void Update()
    {
        if (!isUsingCV)
        {
            return;
        }
        ReceiveData();
    }
    public void DisplaySKills()
    {
        if (playerUnit != null)
        {
            foreach (SkillBase skill in playerUnit.skillset)
            {
                var UISkill = Instantiate(skill.BattleUIPrefab, UIcontainer.transform);
                skill.SetupPrefab(UISkill, this);
            }
        }
    }
    public void UseSkillButton(string skillnameToUse)
    {
        turnBaseManager.PlayerInput(skillnameToUse);
    }
    public void ShowDialogue(string dialogtext)
    {
        string[] text = { dialogtext };
        Dialogue dialogue = new Dialogue(text);
        dialogueManager.StartDialogue(dialogue);
    }
    //UDP Featuers
    void SendInitialMessage()
    {
        string message = "Hello from Unity!";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
        udpClient.Send(data, data.Length, "127.0.0.1", 12345);
    }

    void SendStatus()
    {
        string message = "";
        if (isRequireInput)
        {
            message = "True";
        }
        else
        {
            message = "False";
        }

        byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
        udpClient.Send(data, data.Length, "127.0.0.1", 12345);
        Debug.Log("Send " + message);
        StartCoroutine(startSending());
    }

    void ReceiveData()
    {
        if (udpClient.Available > 0 && isRequireInput)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
            receivedMessage = System.Text.Encoding.UTF8.GetString(receivedData);
            Debug.Log("Received from Python: " + receivedMessage);
            // fales after get the first input
            // isRequireInput = false;
            //use skill
            if (!isGetFirstValue)
            {
                turnBaseManager.PlayerInput(ValidateReceivedData(receivedMessage));
            }
            else
            {
                EraseMessage();
            }
        }
    }
    public void EraseMessage()
    {
        receivedMessage = null;
    }
    IEnumerator startSending()
    {
        yield return new WaitForSecondsRealtime(1);
        SendStatus();
    }
    public string ReturnSignal()
    {
        return receivedMessage;
    }
    public string ValidateReceivedData(string received)
    {
        if (received == null || received.Length == 0 || received.Equals("") || received.Equals(null))
        {
            Debug.Log("Cannot validate due to value is null");
            return null;
        }
        else
        {
            string validate = ExtractValueBetweenUnderscores(received).ToUpper();
            Debug.Log(validate);
            //Change the character to skill name
            if (!validate.Equals("NULL"))
            {
                if (validate.Equals("A"))
                {
                    return "Reversal RED";
                }
                else if (validate.Equals("B"))
                {
                    return "Blue";
                }
                else if (validate.Equals("C"))
                {
                    return "HollowPurple";
                }
                else if (validate.Equals("D"))
                {
                    return "Inifinity Void";
                }
                else if (validate.Equals("E"))
                {
                    return "Slash";
                }
                else if (validate.Equals("F"))
                {
                    return "Cleave";
                }
                else if (validate.Equals("H"))
                {
                    return "Manevolent Shrine";
                }
                else if (validate.Equals("G"))
                {
                    return "Fuga";
                }
                else if (validate.Equals("I"))
                {
                    return "DarkStar";
                }
                else if (validate.Equals("J"))
                {
                    return "Dorage";
                }
                else if (validate.Equals("K"))
                {
                    return "Tornado";
                }
                else if (validate.Equals("L"))
                {
                    return "KillerRock";
                }
                else if (validate.Equals("M"))
                {
                    return "World Reseter";
                }
                else if (validate.Equals("N"))
                {
                    return "TreasueOfDemiGod";
                }
                else if (validate.Equals("O"))
                {
                    return "Thousand spear";
                }
                else if (validate.Equals("P"))
                {
                    return "LimitBreaker";
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }
    private string ExtractValueBetweenUnderscores(string input)
    {
        int start = input.IndexOf('_') + 1;
        int end = input.LastIndexOf('_');
        if (start >= 0 && end >= 0 && end > start)
        {
            return input.Substring(start, end - start);
        }
        return null;
    }
}
