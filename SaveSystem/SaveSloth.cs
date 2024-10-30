using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public class SaveSloth : MonoBehaviour
{
    public int fileSlot;
    public string fileName;
    public TextMeshProUGUI religonText;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI level;
    public TextMeshProUGUI money;
    public TextMeshProUGUI time;
    public TextMeshProUGUI day;
    private void Start() {
        religonText = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        unitName = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        level = transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
        money = transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>();
        time = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        day = transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        SaveManager.intance.UpdateText(this);
    }
    public void SaveButton()
    {
        SaveManager.intance.SaveGame(this);
    }
    public void DeleteButton()
    {
        SaveManager.intance.DeleteSloth(this);
    }
    public void LoadButton()
    {
        SaveManager.intance.LoadGame(this);
    }
    public void UpdateText( string unitName,string level,
    string money)
    {
        DateTime now = DateTime.Now;
        this.unitName.text = "Unit: " + unitName;
        this.level.text = "Level :"+level;
        this.money.text = "Money: "+ money;
        this.time.text = now.ToString("HH:mm:ss");
        string minutes = now.Minute.ToString("");
        string hous = now.Hour.ToString("");
        string second = now.Second.ToString("");
        this.day.text = second +"::" + minutes+"::" + hous;
    }
}
