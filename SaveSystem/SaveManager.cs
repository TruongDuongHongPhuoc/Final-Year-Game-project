using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveManager : MonoBehaviour
{
    public static SaveManager intance { get; private set; }
    public List<SkillBlock> Skills;
    public List<string> skillUnlocked;
    public List<QuestBase> questDataBase;
    public List<EventStoryBase> eventStoryDatabase;
    public List<string> eventstoriesTriggerd;
    public static List<string> questCompleted = new List<string>();
    public List<EventTRiggerBase> eventTriggerDataBase;
    public List<string> TriggerTriggered = new List<string>();
    [SerializeField] GameData gameData = new GameData();
    public Dictionary<int, string> SavePath = new Dictionary<int, string>();
    public Dictionary<int, GameObject> SlotAvailable = new Dictionary<int, GameObject>();
    public void DeleteSloth(SaveSloth saveSloth)
    {
        if (saveSloth.fileName != null)
        {
            if (File.Exists(Application.persistentDataPath + "/" + saveSloth.fileName))
            {
                try
                {
                    File.Delete(Application.persistentDataPath + "/" + saveSloth.fileName);
                    Debug.Log("File deleted: " + saveSloth.fileName);
                    SlotAvailable.Remove(saveSloth.fileSlot);
                }
                catch (IOException ioExp)
                {
                    Debug.LogError("Error deleting file: " + ioExp.Message);
                }
            }
            else
            {
                Debug.LogWarning("File not found: " + saveSloth.fileName);
            }
        }
    }
    public void SaveGame(SaveSloth saveSloth)
    {
        string fileName = saveSloth.fileName;
        Save(fileName);
        UpdateText(saveSloth);
    }
    public void UpdateText(SaveSloth saveSloth)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/" + saveSloth.fileName);
        gameData = JsonUtility.FromJson<GameData>(json);
        saveSloth.UpdateText(gameData.unitData.name, gameData.unitData.level.ToString(), gameData.playerData.Money.ToString());
    }
    public void LoadGame(SaveSloth saveSloth)
    {
        string fileName = saveSloth.fileName;
        Load(fileName);
    }
    private void Awake()
    {
        if (intance != null && intance != this)
        {
            Destroy(this);
        }
        else
        {
            intance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        string json = JsonUtility.ToJson(gameData);

    }
    private void Start()
    {

    }
    public void Save(string saveFileName)
    {
        if (gameData == null)
        {
            gameData = new GameData();
        }
        SavePlayerData();
        SaveUnit();
        SaveEquipment();
        SaveSkillTreeData();
        SaveInventory();
        SaveQuests();
        SaveTriggerData();
        SaveEvents();
        //write to File
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, json);
        Debug.Log("Game saved to " + Application.persistentDataPath + "/" + saveFileName);
    }
    public void Load(string saveFileName)
    {
        string filePath = Application.persistentDataPath + "/" + saveFileName;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            // Restore player position or other relevant data
            BeforeLoad();
         //skin
            LoadUnitData(); //
            LoadEquipmentData(); // equipment
            LoadSkillTreeData();
            LoadInventory();
            StartCoroutine(waitBeforeLoad());
            
            StartCoroutine(startUnlockUnrealtedScene());
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
            gameData = new GameData();  // Initialize new game data if no save file exists
        }
    }
    private void Update()
    {

    }
    public void SavePlayerData()
    {
        PlayerController player = PlayerController.intance;
        Vector3 position = player.transform.position;
        int money = player.playerCondition.Money;
        string sceneName = SceneLoader.CurrentSceneName;
        int skillpoint = player.playerCondition.skillPont;
        string RPGIndex = player.playerCondition.CurrentCharacter.gameObject.name;
        gameData.playerData = new PlayerData(sceneName, position, money, skillpoint, RPGIndex);
    }
    public void SaveUnit()
    {
        UnitBase unit = PlayerController.intance.playerCondition.CurrentCharacter.useUnit;
        string name = unit.UnitName;
        int currentHeal = unit.CurrentHeal;
        int maxHeal = unit.MaxHeal;
        int shield = unit.Shield;
        int maxShield = unit.MaxShield;
        int armor = unit.Armor;
        int damage = unit.Damage;
        int speed = unit.Speed;
        int level = unit.Level;
        int experience = unit.Experience;
        int ExperienceRequire = unit.ExperienceRequire;
        List<string> skillsNames = unit.ReturnSkillList();
        gameData.unitData = new UnitData(name, currentHeal, maxHeal, shield, maxShield, armor, damage, speed, level, experience, ExperienceRequire, skillsNames);
    }
    public void SaveEquipment()
    {
        PlayerCondition player = PlayerController.intance.playerCondition;
        string helmet = null;
        string armor = null;
        string lowerArmor = null;
        string weaponName = null;
        string ringName = null;
        if (player.helmet != null)
        {
            helmet = player.helmet.name;
        }
        if (player.Chest != null)
        {
            armor = player.Chest.name;
        }
        if (player.pant != null)
        {
            lowerArmor = player.pant.name;
        }
        if (player.Weapon != null)
        {
            weaponName = player.Weapon.name;
        }
        if (player.Ring != null)
        {
            ringName = player.Ring.name;
        }
        gameData.equipmentData = new EquipmentData(helmet, armor, lowerArmor, weaponName, ringName);
    }
    public void SaveSkillTreeData()
    {
        gameData.skillTreeData = new SkillTreeData(skillUnlocked);
    }
    public void SaveInventory()
    {
        Inventory inventory = PlayerController.intance.inventory;
        List<string> ids = new List<string>();
        List<int> quantity = new List<int>();
        for (int i = 0; i < inventory.container.Count; i++)
        {
            ids.Add(inventory.container[i].itemBase.name);
            quantity.Add(inventory.container[i].amount);
        }
        gameData.inventoryData = new InventoryData(ids, quantity);
    }
    public void SaveQuests()
    {
        QuestManager QM = QuestManager.intance;
        List<string> questName = new List<string>();
        List<string> sceneToload = new List<string>();
        List<string> completedQuest = questCompleted;
        foreach (QuestBase q in QM.quests)
        {
            questName.Add(q.gameObject.name);
            sceneToload.Add(q.sceneName);
        }
        gameData.questData = new QuestData(sceneToload, questName, completedQuest);
    }
    public void SaveTriggerData()
    {
        List<string> id = new List<string>();
        foreach (string trigger in TriggerTriggered)
        {
            id.Add(trigger);
        }

        gameData.triggerData = new TriggerData(id);
    }
    public void SaveEvents()
    {
        List<string> id = new List<string>();
        foreach (string eventStoryBase in eventstoriesTriggerd)
        {
            id.Add(eventStoryBase);
        }
        gameData.eventStoriesData = new EventStoriesData(id);
    }
    public void LoadPlayerData()
    {
        SceneLoader.LoadSceneByNameAsynAdditive(gameData.playerData.sceneName);
        PlayerController player = PlayerController.intance;
        player.playerCondition.Money = gameData.playerData.Money;
        player.playerCondition.skillPont = gameData.playerData.skillPoint;
        player.transform.position = gameData.playerData.position;
        
        // player.playerCondition.CurrentCharacter = ResourceLoadFind("PlayableUnit", gameData.playerData.RPGCharacterBaseID).GetComponent<CharacterBase>();
        // player.playerCondition.CurrentSkin = player.playerCondition.CurrentCharacter.skin;
        // player.ChangeSkin(player.playerCondition.CurrentCharacter.skin);
    }
    public void LoadUnitData()
    {
        UnitBase unit = PlayerController.intance.playerCondition.CurrentCharacter.useUnit;
        if (unit != null)
        {
            unit.UnitName = gameData.unitData.name;
            unit.CurrentHeal = gameData.unitData.currentHeal;
            unit.MaxHeal = gameData.unitData.maxHeal;
            unit.Shield = gameData.unitData.shield;
            unit.MaxShield = gameData.unitData.maxShield;
            unit.Armor = gameData.unitData.armor;
            unit.Damage = gameData.unitData.damage;
            unit.Speed = gameData.unitData.speed;
            unit.Level = gameData.unitData.level;
            unit.ExperienceRequire = gameData.unitData.ExperienceRequire;
            // load current skill
            unit.skillset.Clear();
            foreach (string skillname in gameData.unitData.skillsNames)
            {
                unit.skillset.Add(ResourceLoadFind("PlayerSkill", skillname).GetComponent<SkillBase>());
            }
        }
        else
        {
            StartCoroutine(waitToCreateUseUnit());
        }
    }
    IEnumerator waitToCreateUseUnit()
    {
        yield return new WaitForSeconds(0.2f);
        LoadUnitData();
    }
    public void LoadEquipmentData()
    {
        PlayerController player = PlayerController.intance;
        player.equipment.ClearArmor();
        player.equipment.PlayerSkin = player.Skin;
        player.equipment.Inital();
        LoadAndUseItem("Equipable", gameData.equipmentData.HelmetName);
        LoadAndUseItem("Equipable", gameData.equipmentData.ArmorName);
        LoadAndUseItem("Equipable", gameData.equipmentData.LowerArmorName);
        LoadAndUseItem("Equipable", gameData.equipmentData.WeaponName);
        LoadAndUseItem("Equipable", gameData.equipmentData.ringName);
    }
    public void LoadSkillTreeData()
    {
        foreach (string blockId in gameData.skillTreeData.unlockedSkill)
        {
            foreach (SkillBlock skill in Skills)
            {
                if (skill.skillBlockId == blockId)
                {
                    skill.StaticUnLockSkill();
                }
            }
        }
    }
    public void LoadInventory()
    {
        Inventory inventory = PlayerController.intance.inventory;
        inventory.container.Clear();
        for (int i = 0; i < gameData.inventoryData.itemID.Count; i++)
        {
            ItemBase itemToAdd = ResourceLoadFindScriptable<ItemBase>("ItemsDataBase", gameData.inventoryData.itemID[i]);
            int quantityToAdd = gameData.inventoryData.quantities[i];
            if (itemToAdd != null && quantityToAdd > 0)
            {
                inventory.AddItem(itemToAdd, quantityToAdd);
            }
        }
    }
    public void LoadQuest()
    {
        Debug.Log("Quest Load");
        List<QuestBase> questsToRemove = new List<QuestBase>();
        List<QuestBase> questsToAdd = new List<QuestBase>();

        // Identify completed quests to remove
        foreach (string questname in gameData.questData.CompletedQuestNames)
        {
            foreach (QuestBase quest in questDataBase)
            {
                if (questname.Equals(quest.gameObject.name))
                {
                    questsToRemove.Add(quest);
                }
                else
                {
                }
            }
        }
        List<string> questNeedToAdd = gameData.questData.questActivatedNames;
        //Load scene to have quest to add
        //add quest
        Debug.Log("Lenght of quest need to add" + questNeedToAdd.Count);
         Debug.Log("Lenght of quest in database " + questDataBase.Count);
        foreach (string questName in questNeedToAdd)
        {
            foreach (QuestBase quest in questDataBase)
            {
                if (questName.Equals(quest.gameObject.name))
                {
                    questsToAdd.Add(quest);
                }
                else
                {

                }
            }
        }
        // unload all scene
        // SceneLoader.UnloadAllScenesExcept();
        // // Remove completed quests
        // foreach (QuestBase quest in questsToRemove)
        // {

        //     questDataBase.Remove(quest);
        //     Destroy(quest.gameObject);
        // }

        // Add active quests
        foreach (QuestBase quest in questsToAdd)
        {
            Debug.Log("Quest mange add quest: " + quest.name);
            quest.EventHandle();
        }
    }
    public void LoadTriggerData()
    {
        Debug.Log("Triggers Count: " + gameData.triggerData.ids.Count);

        // Create a list to hold items to remove
        List<EventTRiggerBase> triggersToRemove = new List<EventTRiggerBase>();

        foreach (string trigger in gameData.triggerData.ids)
        {
            // Find matching triggers and add them to the removal list
            foreach (EventTRiggerBase tRiggerBase in eventTriggerDataBase)
            {
                if (tRiggerBase.gameObject.name.Equals(trigger))
                {
                    triggersToRemove.Add(tRiggerBase);
                    Debug.Log("Marked for removal: " + tRiggerBase.gameObject.name);
                }
                else
                {
                    Debug.Log("Compare: " + tRiggerBase.gameObject.name + " with " + trigger);
                }
            }
        }

        // Remove and destroy marked triggers outside of the iteration
        foreach (EventTRiggerBase tRiggerBase in triggersToRemove)
        {
            eventTriggerDataBase.Remove(tRiggerBase);
            Destroy(tRiggerBase.gameObject);
            Debug.Log("Removed and destroyed: " + tRiggerBase.gameObject.name);
        }
    }
    private void LoadEvents()
    {
        Debug.Log("Loading event ....");
        eventstoriesTriggerd = gameData.eventStoriesData.eventStoriesActivated;
        foreach (EventStoryBase e in eventStoryDatabase)
        {
            if (eventstoriesTriggerd.Contains(e.gameObject.name))
            {
                Debug.Log("event is operating "+ e.gameObject.name );
                e.EventHandle();
            }
            else
            {
                Debug.Log("event stories triggered is not contain" + e.gameObject.name+"");
            }
        }
    }
    private void LoadAndUseItem(string path, string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            ItemBase item = ResourceLoadFindScriptable<ItemBase>(path, itemName);
            if (item != null)
            {
                item.UseOfItem();
            }
            else
            {
                Debug.LogWarning($"Item not found");
            }
        }
        else
        {

        }
    }
    IEnumerator startUnlockUnrealtedScene(){
        LoadPlayerData();
        yield return new WaitForSecondsRealtime(2f);
        UnLoadUnrelatedScene();
    }
    public void UnLoadUnrelatedScene(){
        Debug.Log("Unload scenes start");
        SceneLoader.UnloadAllScenesExcept(gameData.playerData.sceneName);
    }

    public static GameObject ResourceLoadFind(string path, string name)
    {
        if (path.Equals("") || name.Equals(""))
        {
            Debug.Log("Must Find something to return");
            return null;
        }
        string fullPath = System.IO.Path.Combine(path, name);
        GameObject prefab = Resources.Load<GameObject>(fullPath);
        if (prefab != null)
        {
            return prefab;
        }
        else
        {
            Debug.LogError("Prefab not found in Resources folder: " + fullPath + name);
            return null;
        }
    }
    public static T ResourceLoadFindScriptable<T>(string path, string name) where T : ScriptableObject
    {
        if (path.Equals("") || name.Equals(""))
        {
            Debug.LogError("Must have path and Name to find Object");
            return null;
        }
        string fullPath = System.IO.Path.Combine(path, name);
        T scriptableObject = Resources.Load<T>(fullPath);
        if (scriptableObject != null)
        {
            return scriptableObject;
        }
        else
        {
            Debug.LogError("ScriptableObject not found in Resources folder: " + fullPath + name);
            return null;
        }
    }
    public void BeforeLoad()
    {
        QuestManager.intance.quests.Clear();
        QuestManager.intance.UnDisplayAllQuest();
        eventTriggerDataBase.Clear();
        questDataBase.Clear();
        eventStoryDatabase.Clear();
        loadallSceneRequired();
    }
    public void loadallSceneRequired(){
        List<string> allscene = gameData.questData.sceneRequireToLoad;
        Debug.Log("Scene required to load" + allscene.Count);
        foreach(string s in allscene){
            SceneLoader.LoadSceneByNameAsyn(s);
        }
    }
    IEnumerator waitBeforeLoad()
    {
        yield return new WaitForSeconds(1f);
        LoadQuest();
        LoadTriggerData();
        LoadEvents();
    }
}

