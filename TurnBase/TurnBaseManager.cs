using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
// initial -> playerEffect -> player input -> enemy effect -> enemy input -> cool down -> initial
public class TurnBaseManager : MonoBehaviour
{
    public BaseTurn currentTurn;
    public InitialTurn initialTurn = new InitialTurn();
    public PlayerEffectTurn playerEffectTurn = new PlayerEffectTurn();
    public PlayerInputTurn playerInputTurn = new PlayerInputTurn();
    public EnemyEffectTurn enemyEffectTurn = new EnemyEffectTurn();
    public EnemyInputTurn enemyInputTurn = new EnemyInputTurn();
    public CoolDownTurn coolDownTurn = new CoolDownTurn();
    public WinTurn winTurn = new WinTurn(); 
    public LostTurn lostTurn = new LostTurn();
    public UnitBase PlayerCharacter;
    public UnitBase EnemyCharacter;
    public bool buttonClicked;
    public bool isTesting;
    [SerializeField] public Queue<UnitBase> EnemyRemain = new Queue<UnitBase>();
    [SerializeField] public GameObject playerPosition;
    [SerializeField] public GameObject enemyPosition;
    [SerializeField] public Animator animator;
    [SerializeField] public PlayerSkillUIManager UI;
    [SerializeField] public WinScreenDisplayer winscreen;
    [SerializeField] public UnityEngine.UI.Button continueButton;
    [SerializeField] public GameObject container;
    [SerializeField] public GameObject popUp;
    [SerializeField] public VirtualCameraBehavior virtualCameraBehavior;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //Set up Enemy
        if (!isTesting)
        {
            SetUp();
        }
        //turn start 
        this.currentTurn = initialTurn;
        currentTurn.EnterTurn(this);
        currentTurn.TurnFlow(this);

    }
    public void SwitchTurn(BaseTurn turnToSwitch)
    {
        StartCoroutine(waitForSecond(turnToSwitch));
    }
    IEnumerator waitForSecond(BaseTurn turnToSwitch)
    {
        continueButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(() => buttonClicked = true);
        yield return new WaitUntil(() => buttonClicked);
        buttonClicked = false;
        if (currentTurn != this.winTurn)
        {
            if (isPlayerAlive() && !isEnemyAlive())
            {
                turnToSwitch = winTurn;
            }
            else if (!isPlayerAlive())
            {
                turnToSwitch = lostTurn;
            }
            else
            {
                Debug.Log("both still alive");
            }
        }
        turnToSwitch.EnterTurn(this);
        turnToSwitch.TurnFlow(this);

    }
    public GameObject Spawn(UnitBase objecToSpaw, GameObject SpawnPosition)
    {
        GameObject spawned = Instantiate(objecToSpaw.gameObject, SpawnPosition.transform.position, quaternion.identity, container.transform);
        return spawned;
    }
    public bool isPlayerAlive()
    {
        if (PlayerCharacter.CurrentHeal <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool isEnemyAlive()
    {
        if (EnemyCharacter == null || EnemyCharacter.Equals(null))
        {
            return false;
        }
        else if (EnemyCharacter.CurrentHeal <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void PlayerInput(string skillnameToUse)
    {
        // if(skillnameToUse != null){
        // playerInputTurn.PlayerInput(skillnameToUse);
        // }
        // Debug.Log("skill name to use is freaking null");
        if (skillnameToUse != null && skillnameToUse.Length > 0)
        {
            if (!skillnameToUse.Equals("NULL"))
            {
                playerInputTurn.PlayerInput(skillnameToUse);
            }
            else
            {
                Debug.Log("Skill name is NULL");
            }
        }
    }
    public void SetUp()
    {
        // set character

        PlayerCharacter = Spawn(Sender.playerUnit, playerPosition).GetComponent<UnitBase>();
        equipPlayerArmor();
        foreach (UnitBase enemy in Sender.enemy)
        {
            EnemyRemain.Enqueue(enemy);
            Debug.Log("Enque" + enemy.name);
        }
        Sender.enemy.Clear();
    }
    public void EnemyDefeat()
    {
        if (EnemyCharacter.Level == 0)
        {
            EnemyCharacter.Level = 1;
        }
        int experince = EnemyCharacter.Level * UnityEngine.Random.Range(5, 30) / 2;
        Debug.Log("Player GainExperience" + experince);
        PlayerCharacter.GainExperience(experince);
        StartCoroutine(PlayDeadAnimation());
    }
    IEnumerator PlayDeadAnimation()
    {
        EnemyCharacter.isDeath = true;
        EnemyCharacter.animator.SetTrigger("Death");
        yield return new WaitForSecondsRealtime(1.5f);
        EnemyCharacter.CurrentHeal = 0;

    }

    public void PlayAnimation(float time, Action function)
    {
        StartCoroutine(StartAnimation(time, function));
    }
    IEnumerator StartAnimation(float time, Action function)
    {
        Debug.Log("Start Animation");
        yield return new WaitForSeconds(time);
        function?.Invoke();
    }
    public void equipPlayerArmor()
    {
        PlayerCharacterEquipment playerEquipment = PlayerCharacter.gameObject.GetComponent<PlayerCharacterEquipment>();
        PlayerCharacterEquipment RPGEquipment = PlayerController.intance.equipment;
        playerEquipment.PlayerSkin = PlayerCharacter.gameObject;
        playerEquipment.ChestPlaceprefab = RPGEquipment.ChestPlaceprefab;
        playerEquipment.PantArmorprefab = RPGEquipment.PantArmorprefab;
        playerEquipment.Helmetprefab = RPGEquipment.Helmetprefab;
        playerEquipment.EquipAllArmor();
    }
    public void shakeCamerainSkill(SkillBase skill)
    {
        Debug.Log("Shaking should be start");
        virtualCameraBehavior.ShakeCamera(skill.shakeAmplify, skill.frequencyAmplify, skill.shakingTime);
    }
    public void staticSwitchTurn(BaseTurn turnToSwitch)
    {
        turnToSwitch.EnterTurn(this);
        turnToSwitch.TurnFlow(this);
    }
}
