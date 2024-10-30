using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI armor;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI shield;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] GameObject container;
    [SerializeField] GameObject UIBase;
    [SerializeField] GameObject helmetDisplay;
    [SerializeField] GameObject armorDisplay;
    [SerializeField] GameObject pantDisplay;
    [SerializeField] GameObject weaponDisplay;
    [SerializeField] GameObject ringDisplay;
    PlayerCondition playerCondition;
    public bool isOpen;
    List<GameObject> spawnList = new List<GameObject>();


    private void Update()
    {
        Debug.Log(PlayerController.intance.playerCondition.helmet);
        getPlayerCondition();
        UpdateText(playerCondition);
    }
    public void UpdateText(PlayerCondition playerCondition)
    {
        hpText.text = "HP: " + playerCondition.CurrentCharacter.useUnit.MaxHeal.ToString();
        characterName.text = "Name: " + playerCondition.CurrentCharacter.useUnit.UnitName.ToString();
        armor.text = "level: " + playerCondition.CurrentCharacter.useUnit.Armor.ToString();
        level.text = "level: " + playerCondition.CurrentCharacter.useUnit.Level.ToString();
        shield.text = "shield: " + playerCondition.CurrentCharacter.useUnit.MaxShield.ToString();
        damage.text = "damage: " + playerCondition.CurrentCharacter.useUnit.Damage.ToString();
        coin.text = "coin: " + playerCondition.Money.ToString();
    }
    public void ShowItems()
    {
        getPlayerCondition();
        foreach (InventorySlot slot in PlayerController.intance.inventory.container)
        {
            if (slot.itemBase.ItemType == ItemType.Equipment)
            {
                spawnList.Add(Spawnobject(slot.itemBase));
            }
        }
        ShowEquipALl();
    }
    public GameObject Spawnobject(ItemBase item)
    {
        var spawned = Instantiate(UIBase, container.transform);
        Dictionary<string, int> value = item.ReturnPropertise();

        // Set the sprite
        spawned.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.displayImg;

        // Convert dictionary to a list of key-value pairs
        var valueList = value.ToList();
        var KeyList = value.Keys.ToList();
        spawned.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = item.name;
        // Loop through the dictionary elements and set the text
        for (int i = 1; i <= valueList.Count; i++)
        {
            if (i < spawned.transform.GetChild(1).childCount)
            {
                spawned.transform.GetChild(1).GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text = KeyList[i - 1] + valueList[i - 1].Value.ToString();
            }
        }
        spawned.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(item.UseOfItem);
        spawned.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => ShowEquip(item));
        spawned.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() => UnshowEquip(item));
        spawned.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() => PlayerController.intance.UnEquipItem(item));
        return spawned;
    }
    public void ClearOjbects()
    {
        for (int i = spawnList.Count - 1; i >= 0; i--)
        {
            GameObject item = spawnList[i];
            spawnList.RemoveAt(i);
            Destroy(item);
        }
    }
    public void ShowEquipALl()
    {
        if (playerCondition.helmet != null)
        {
            helmetDisplay.SetActive(true);
            helmetDisplay.gameObject.GetComponent<Image>().sprite = playerCondition.helmet.displayImg;
        }
        if (playerCondition.Chest != null)
        {
            armorDisplay.SetActive(true);
            armorDisplay.gameObject.GetComponent<Image>().sprite = playerCondition.Chest.displayImg;
        }
        if (playerCondition.pant != null)
        {
            pantDisplay.SetActive(true);
            pantDisplay.gameObject.GetComponent<Image>().sprite = playerCondition.pant.displayImg;
        }
        if (playerCondition.Weapon != null)
        {
            weaponDisplay.SetActive(true);
            weaponDisplay.gameObject.GetComponent<Image>().sprite = playerCondition.Weapon.displayImg;
        }
        if (playerCondition.Ring != null)
        {
            ringDisplay.SetActive(true);
            ringDisplay.gameObject.GetComponent<Image>().sprite = playerCondition.Ring.displayImg;
        }
    }
    public void UnshowEquip(ItemBase itemBase)
    {
        NotifyPlayer.intance.showNotify("your have successfully unequip: " + itemBase.name);
        AudioManager.instance.PlayAudioHaveName("Success");
        Debug.Log("UnShow Equipment" + itemBase.name);
        switch (itemBase)
        {
            case Helmet helmet:
            
                if (helmet.description.Equals(playerCondition.helmet.description))
                {
                    helmetDisplay.gameObject.SetActive(false);
                }
                break;
            case Armor chestPlate:
                if (chestPlate.description.Equals(playerCondition.Chest.description))
                {

                    armorDisplay.SetActive(false);
                }
                break;
            case LowerArmor pantArmor:
                if (pantArmor.description.Equals(playerCondition.pant.description))
                {

                    pantDisplay.SetActive(false);
                }
                break;
            case HandHoldWeapon weapon:
                if (weapon.description.Equals(playerCondition.Weapon.description))
                {

                    weaponDisplay.SetActive(false);
                }
                break;
            case Ring ring:
                if (ring.description.Equals(playerCondition.Ring.description))
                {
                    ringDisplay.SetActive(false);
                }
                break;
        }
    }
    public void ShowEquip(ItemBase itemBase)
    {
        Debug.Log("Show Equipment" + itemBase.name);
        NotifyPlayer.intance.showNotify("You have equip: "+ itemBase.name);
        AudioManager.instance.PlayAudioHaveName("Success");
        switch (itemBase)
        {
            case Helmet helmet:
                helmetDisplay.gameObject.GetComponent<Image>().sprite = itemBase.displayImg;
                helmetDisplay.SetActive(true);
                break;
            case Armor chestPlate:
                armorDisplay.gameObject.GetComponent<Image>().sprite = itemBase.displayImg;
                armorDisplay.SetActive(true);
                break;
            case LowerArmor pantArmor:
                pantDisplay.gameObject.GetComponent<Image>().sprite = itemBase.displayImg;
                pantDisplay.SetActive(true);

                break;
            case HandHoldWeapon weapon:
                weaponDisplay.gameObject.GetComponent<Image>().sprite = itemBase.displayImg;
                weaponDisplay.SetActive(true);

                break;
            case Ring ring:
                ringDisplay.gameObject.GetComponent<Image>().sprite = itemBase.displayImg;
                ringDisplay.SetActive(true);
                break;
        }
    }
    public void getPlayerCondition()
    {
        if (playerCondition == null)
        {
            playerCondition = PlayerController.intance.playerCondition;
        }
    }

}

