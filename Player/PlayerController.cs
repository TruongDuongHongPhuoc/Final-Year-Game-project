using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[System.Serializable]
public class PlayerController : MonoBehaviour
{
    public static PlayerController intance { get; private set; }
    [SerializeField] private float defautWalkingSpeedRPG;
    [SerializeField] private float defautRunningSpeedRPG;
    [SerializeField] private float defautInjuredSpeedRPG;
    [SerializeField] private float speedRPG;
    [SerializeField] public GameObject Skin;
    [SerializeField] public PlayerCondition playerCondition;
    [SerializeField] public Inventory inventory;
    [SerializeField] public PlayerCharacterEquipment equipment;
    [SerializeField] public PlayerCharacterEquipment skinEquipment;
    private CharacterController characterController;
    [SerializeField] private Animator animator;
    private Gravity gravity;
    private Vector3 direction;
    private Vector3 Facing_Dir;
    private bool isground;
    [SerializeField] private bool isInjured;
    [SerializeField] private bool isRunning;
    public static GameObject InteractedGameObject;
    private bool isMoveAble;
    public static bool isBattling;
    public bool isTestign;
    private void Awake()
    {
        if (intance != null && intance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            intance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SetupUnit(Instantiate(playerCondition.CurrentCharacter.currentUnit,transform));
    }
    private void Start()
    {
        if(isTestign){
            UnitBase unit = playerCondition.CurrentCharacter.useUnit;
            unit.PowerUpTolevel(10000);
            unit.CurrentHeal = unit.MaxHeal;
            this.defautRunningSpeedRPG = 50;
        }
        SetSkin();
        animator = Skin.GetComponentInChildren<Animator>();
        animator.gameObject.SetActive(true);
        isMoveAble = true;
        gravity = gameObject.GetComponent<Gravity>();
        characterController = gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        Debug.Log("is move able:" +isMoveAble);
        Debug.Log("is battle:" + isBattling);
        if (!isBattling)
        {
            isMoveAble = true;
            FixFreeAnimation();
            InjuringAnimation();
            Running();
            Movement();
            Interact();
        }
        else
        {
            isMoveAble = false;
            FixFreeAnimation(); 
            Debug.Log("Player is Battle");
        }
    }

    private void InjuringAnimation()
    {
        CheckInjured();
        if (isInjured)
        {
            animator.SetBool("isInjured", true);
        }
        else
        {
            animator.SetBool("isInjured", false);
        }
    }

    public void Movement()
    {
        if (isInjured)
        {
            Debug.Log("Set speed to injured speed");
            speedRPG = defautInjuredSpeedRPG;
        }else if(isRunning){
            speedRPG = defautRunningSpeedRPG;
        }
        else{
            speedRPG = defautWalkingSpeedRPG;
        }
        isground = gravity.Checkground();
        isground = true;
        if (isground && isMoveAble)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            // animator.SetFloat("Xmove", horizontal);
            // animator.SetFloat("Ymove", vertical);
            direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (horizontal != 0 || vertical != 0)
            {
                Facing_Dir = new Vector3(horizontal, 0f, vertical);
            }
            if (direction.magnitude >= 0.1f)
            {
                if (!isInjured && !isRunning)
                {
                    animator.SetBool("isRunning",false);
                    animator.SetBool("isMoving", true);
                }
                else if(!isInjured && isRunning){
                    animator.SetBool("isMoving",false);
                    animator.SetBool("isRunning",true);
                } else
                {
                    animator.SetBool("isMovingInjured", true);
                }
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion RotateTarget = Quaternion.Euler(0f, targetAngle, 0f);
                Skin.transform.rotation = Quaternion.Lerp(Skin.transform.rotation, RotateTarget, 0.5f);
                characterController.Move(direction * speedRPG * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isMovingInjured", false);
            }
        }

    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 interactPos = transform.position + Facing_Dir * 1f;
            Vector3 interactBoxSize = new Vector3(0.5f, 0.5f, 0.5f);
            Collider[] colliders = Physics.OverlapBox(interactPos, interactBoxSize / 2, Quaternion.identity);
            foreach (Collider col in colliders)
            {
                Debug.Log("Interact With" + col.gameObject.name);
                if (col.gameObject != null && col.gameObject.GetComponent<I_Interact>() != null)
                {
                    col.GetComponent<I_Interact>().interact(this.gameObject);
                    InteractedGameObject = col.gameObject;
                    return;
                }
            }
        }
    }
    public void collectItem(ItemBase itemBase)
    {
        if (inventory != null)
        {
            inventory.AddItem(itemBase, 1);
        }
    }
    private void SetSkin()
    {
        if (playerCondition.CurrentCharacter != null)
        {
            Destroy(playerCondition.CurrentSkin);
            var newskin = Instantiate(playerCondition.CurrentCharacter.skin, transform.position, quaternion.identity, transform);
            playerCondition.CurrentSkin = newskin;
            this.Skin = newskin;
            equipment.InitalBones(newskin);
            skinEquipment = Skin.GetComponent<PlayerCharacterEquipment>();
        }
    }
    public void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isInjured == false)
            {
               isRunning = true;
               
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isInjured == false)
            {
                isRunning = false;
                animator.SetBool("isRunning",false);
            }
        }
    }
public void LoseBattle()
{
    Debug.Log("Player Lose");
}
private void OnDrawGizmos()
{
    OnDrawGizmosSelected();
}
private void OnDrawGizmosSelected()
{
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    Vector3 interactDirection = new Vector3(horizontal, 0f, vertical).normalized;
    Vector3 interactPos = transform.position + Facing_Dir * 1f;

    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(interactPos, new Vector3(0.5f, 0.5f, 0.5f));
}

public void EquipItemToCharacter(ItemBase itemBase)
{
    if (itemBase == null)
    {
        Debug.LogError("Item to equip is Empty");
        return;
    }
    if (itemBase.ItemType != ItemType.Equipment)
    {
        Debug.LogError("Wrong Type of the Item");
    }
    else
    {
        this.playerCondition.EquipItems(itemBase);
        switch (itemBase)
        {
            case Helmet helmet:
                equipment.Helmetprefab = helmet.equipPrefab;
                skinEquipment.Helmetprefab = helmet.equipPrefab;
                 break;
            case Armor armor:
                equipment.ChestPlaceprefab = armor.equipPrefab;
                skinEquipment.ChestPlaceprefab = armor.equipPrefab;
                break;
            case LowerArmor lowerArmor:
                equipment.PantArmorprefab = lowerArmor.equipPrefab;
                skinEquipment.PantArmorprefab = lowerArmor.equipPrefab;
                break;
        }
        skinEquipment.EquipAllArmor();
        equipment.EquipAllArmor();
    }
}
public void UnEquipItem(ItemBase item)
{
    if (item.ItemType != ItemType.Equipment)
    {
        Debug.LogError("Wrong Type of the Item");
    }
    else
    {
        this.playerCondition.UnEquipItem(item);
        equipment.UnEquipItem(item);
        skinEquipment.UnEquipItem(item);
    }
}
public void FixFreeAnimation()
{
    if (!isMoveAble || isBattling)
    {
        isRunning = false;
        animator.SetBool("isMoving", false);
        animator.SetBool("isMovingInjured", false);
        animator.SetBool("isRunning",false);
    }
}
public void ChangeSkin(GameObject gameObject)
{
    Destroy(Skin);
    Skin = Instantiate(gameObject, transform);
    Skin.transform.localPosition = new Vector3(0, 0, 0);
    animator = Skin.GetComponentInChildren<Animator>();
    equipment.PlayerSkin = Skin;
}
public bool CheckInjured()
{
    if (playerCondition.CurrentCharacter.useUnit.CurrentHeal <= playerCondition.CurrentCharacter.useUnit.MaxHeal / 3)
    {
        isInjured = true;
        return true;
    }
    else
    {
        isInjured = false;
        return false;
    }
}
public void SetEmptyInteracted()
{
    InteractedGameObject = null;
}
public void SetupUnit(UnitBase unit){
    Debug.Log("Use unit is Instantiated do not null");
    playerCondition.CurrentCharacter.useUnit = unit;
}
public void SetupCharacterBase(){
    
}
}
