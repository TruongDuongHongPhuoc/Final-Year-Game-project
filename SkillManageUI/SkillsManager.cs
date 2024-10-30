using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance { get; private set; }
    public List<SkillBase> skillsUnlocked;
    [SerializeField] GameObject UIPrefabBase;
    [SerializeField] GameObject container;
    [HideInInspector]
    SkillBase currentSelectedSkill;
    [SerializeField] TextMeshProUGUI skillDescription;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Button equipButton;
    [SerializeField] Button unEquipButton;
    private UnitBase playerUnit;
    [SerializeField] public Animator animator;
    // 1 -> ying 2-> yang 3-> head 4-> tail 
    [SerializeField] List<Sprite> elementsImages;
    Dictionary<SkillBase, GameObject> spawnedUI = new Dictionary<SkillBase, GameObject>();
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
        playerUnit = PlayerController.intance.playerCondition.CurrentCharacter.useUnit;
        foreach (SkillBase skill in playerUnit.skillset)
        {
            CreateUIObject(skill);
        }
    }

    public void CreateUIObject(SkillBase skill)
    {
        GameObject spanwed = Instantiate(UIPrefabBase, container.transform);
        spanwed.transform.GetChild(0).GetComponent<Image>().sprite = skill.skillDisplayImage;
        spanwed.transform.GetChild(1).GetComponent<Image>().sprite = ReturnElementImage(skill.element);
        spanwed.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = skill.skillName;
        spanwed.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Damage: " + skill.DamageAmount + "\n Element: " + skill.element + "\n Effect :" + (skill.effectBase == null ? " dont have effect" : skill.effectBase.effectName);
        spanwed.GetComponent<Button>().onClick.AddListener(() => SetOnClickEvent(skill));
        if (spawnedUI.ContainsKey(skill))
        {
            Destroy(spanwed);
        }
        else
        {
            spawnedUI.Add(skill, spanwed);
        }
    }
    public Sprite ReturnElementImage(Element element)
    {
        if (element is Element.yang)
        {
            return elementsImages[0];
        }
        else if (element is Element.yin)
        {
            return elementsImages[1];
        }
        else if (element is Element.head)
        {
            return elementsImages[2];
        }
        else
        {
            return elementsImages[3];
        }
    }
    public void SetOnClickEvent(SkillBase skill)
    {
        if (skill == null)
        {
            Debug.LogError("Skill is null please add skill to SetOnClickEvent in" + gameObject.name);
            return;
        }
        if (skill.clip != null)
        {
            currentSelectedSkill = skill;
            videoPlayer.clip = skill.clip;
            skillDescription.text = skill.skillDescription;
        }
        else
        {
            Debug.LogError("Video is Null");
        }
    }
    public void OnClickEquipButton()
    {
        if (currentSelectedSkill != null && playerUnit != null)
        {
            if (playerUnit.EquipSkill(currentSelectedSkill))
            {
                GameObject temp;
                spawnedUI.TryGetValue(currentSelectedSkill, out temp);
                temp.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Equipped";
            }
        }
        else
        {
            Debug.LogError("Selected Skill or Player Unit is Null in Gameobject name" + this.gameObject.name);
        }
    }
    public void OnClickUnEquipButton()
    {
        if (currentSelectedSkill != null && playerUnit != null)
        {
            playerUnit.UnEquipSkill(currentSelectedSkill);
            GameObject temp;
            spawnedUI.TryGetValue(currentSelectedSkill, out temp);
            temp.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}

