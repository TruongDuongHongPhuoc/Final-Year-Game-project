using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, I_Interact
{
    public string enemyName;
    public float detectionRange;
    public UnityEvent EventWhenDefeated; 
    [SerializeField]Dialogue dialogue;
    [SerializeField] public List<UnitBase> Enemies;
    [SerializeField] public List<ItemBase> ItemReward;
    [SerializeField] public int additionExp;
    [SerializeField] public int AdditionMoney;
    public bool isDefeated;
    public int BattleEnviroment;
    public int minUnitLevel; 
    public int maxUnitLevel;
    [SerializeField]public List<UnitBase> unitUse;
    private void Start() {
        if(Enemies.Count >0){
        foreach(UnitBase unit in Enemies){
            UnitBase spawn = Instantiate(unit);
            spawn.PowerUpTolevel(Random.Range(minUnitLevel,maxUnitLevel));
            unitUse.Add(spawn);
        }}else{
            Debug.LogWarning("No Enemes was assign");
        }
    }
    public void interact(GameObject other)
    {
        AudioManager.instance.PlayThemeMusic("Fight");
        Setup();
        SceneLoader.LoadSceneByNameAsynAdditive("Battle_City");
    }
    public void Defeated(){
        isDefeated = true;
        foreach(UnitBase unit in unitUse){
            Destroy(unit);
        }
        //require for defeat an type of enemy quest. it fuckin lord.
        QuestManager.intance.DefeatEnemy(this);
        Collider collider = GetComponent<BoxCollider>();
        Collider collider1 = GetComponent<CapsuleCollider>();
        collider1.enabled = false;
        collider.enabled = false;
        EventWhenDefeated.Invoke();
    }
    public void GiveReward(){
       Inventory playerinventory = GameObject.Find("Player").gameObject.GetComponent<PlayerController>().inventory;
        foreach(ItemBase i in ItemReward){
            Debug.Log("you have Get" + i.name);
            playerinventory.AddItem(i,1);
        }
    }
    public void startDestroySelf(){
        StartCoroutine(startCountDownDestroySelf());
    }
    public void Setup(){
        Sender.enviromentID = BattleEnviroment;
        Sender.enemy = this.unitUse;
        Sender.playerUnit = PlayerController.intance.playerCondition.CurrentCharacter.useUnit;
        PlayerController.isBattling = true;
        Sender.enemyBattling = this;
    }
    private void OnTriggerEnter(Collider other) {
         if (other.gameObject.GetComponent<PlayerController>() != null && !PlayerController.isBattling && Enemies != null && Enemies.Count>0)
            {
                PlayerController.InteractedGameObject = this.gameObject;
                PlayerController.isBattling = true;
                DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
                dialogueManager.StartDialogue(dialogue);
                StartCoroutine(PlayerDetected());
            }else{
                Debug.LogWarning("an "+ this.gameObject.name + "trying to interact while player is battling");
            }
    }
    IEnumerator PlayerDetected(){
        yield return new WaitForSecondsRealtime(1f);
        Faded.Instance.FadedInTrue(1.5f);
        interact(PlayerController.intance.gameObject);
    }
    IEnumerator startCountDownDestroySelf(){
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(this.gameObject);
    }
}

