using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }
    private bool isMainMenuActive;
    private bool isInventoryActive;
    Animator mainMenuAnimator;
    [SerializeField] Animator InventoryAnimator;
    [SerializeField] Animator SkillTreeAnimator;
    [SerializeField] GameObject playermanage;
    [SerializeField] Camera SKillTreeCamera;
    [SerializeField] GameObject saveMenu;
    public List<TextMeshProUGUI> textstatus;
    private PlayerCondition playerCondition;
    [SerializeField] GameObject notifyPlayer;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        playerCondition = PlayerController.intance.playerCondition;
        mainMenuAnimator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        OpenCloseMenu();
        UpdateMainMenuStats();
    }
    public void ResumeButton()
    {
        if (isMainMenuActive)
        {
            isMainMenuActive = false;
            mainMenuAnimator.SetBool("isActive", false);
        }
    }

    public void OpenCloseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMainMenuActive)
            {
                DeactiveMenu();
            }
            else
            {
                ActiveMenu();
            }
        }
    }
    public void ActiveMenu()
    {
        //Deactive all UI
        DeactiveInventory();
        //Active MainMenu
        isMainMenuActive = true;
        mainMenuAnimator.SetBool("isActive", true);
    }
    public void DeactiveMenu()
    {
        // if (GameObject.FindAnyObjectByType<Sorcerer>().gameObject != null)
        // {
        //     SKillTreeCamera = GameObject.FindAnyObjectByType<Sorcerer>().gameObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        //     if (SKillTreeCamera != null)
        //     {
        //         SKillTreeCamera.gameObject.SetActive(false);
        isMainMenuActive = false;
        mainMenuAnimator.SetBool("isActive", false);
    }
    public void ActiveInventory()
    {
        DeactiveMenu();

        isInventoryActive = true;
        InventoryAnimator.SetBool("isActive", true);
    }
    public void DeactiveInventory()
    {
        isInventoryActive = false;
        InventoryAnimator.SetBool("isActive", false);
    }
    public void DeActiveSkillTree()
    {
        if (GameObject.FindAnyObjectByType<Sorcerer>().gameObject != null)
        {
            SKillTreeCamera = GameObject.FindAnyObjectByType<Sorcerer>().gameObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
            if (SKillTreeCamera != null)
            {
                SKillTreeCamera.gameObject.SetActive(false);
                SKillTreeCamera.gameObject.SetActive(false);
                SkillTreeAnimator.SetBool("isActive", false);
            }
        }
    }
    public void UpdateMainMenuStats()
    {
        Debug.Log(PlayerController.intance.playerCondition.CurrentCharacter.useUnit);
        for (int i = 0; i < textstatus.Count; i++)
        {
            switch (i)
            {
                case 0: textstatus[i].text = "HP: " + PlayerController.intance.playerCondition.CurrentCharacter.useUnit.CurrentHeal.ToString() + "/" + playerCondition.CurrentCharacter.useUnit.MaxHeal.ToString(); break;
                case 1: textstatus[i].text = "Damage: " + playerCondition.CurrentCharacter.useUnit.Damage.ToString(); break;
                case 2: textstatus[i].text = "Money: " + playerCondition.Money.ToString(); break;
                case 3: textstatus[i].text = "Armor: " + playerCondition.CurrentCharacter.useUnit.Armor.ToString(); break;
                case 4: textstatus[i].text = "Shield: " + playerCondition.CurrentCharacter.useUnit.MaxShield.ToString(); break;
                case 5: textstatus[i].text = "Level: " + playerCondition.CurrentCharacter.useUnit.Level.ToString() + "Experience:" + playerCondition.CurrentCharacter.useUnit.Experience.ToString() + "/" + playerCondition.CurrentCharacter.useUnit.ExperienceRequire.ToString(); break;
            }
        }
    }
    public void ActivePlayerManage()
    {
        playermanage.gameObject.SetActive(true);
        playermanage.GetComponent<PlayerManage>().ShowItems();

        DeactiveMenu();
    }
    public void DeactivePlayerManage()
    {
        playermanage.gameObject.SetActive(false);
        playermanage.GetComponent<PlayerManage>().ClearOjbects();
        ActiveMenu();

    }
    public void ActiveSaveMenu()
    {
        saveMenu.SetActive(true);
    }
    public void DeactiveSaveMenu()
    {
        saveMenu.SetActive(false);
    }
    public void ActiveSkillManagement()
    {
        SkillsManager.Instance.animator.SetBool("isOpen", true);
        DeactiveMenu();
    }
    public void DeactiveSkillManagement()
    {
        SkillsManager.Instance.animator.SetBool("isOpen", false);
        ActiveMenu();
    }
}
