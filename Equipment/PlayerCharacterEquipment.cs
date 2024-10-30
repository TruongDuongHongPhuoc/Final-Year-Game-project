using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterEquipment : MonoBehaviour
{
    public GameObject PlayerSkin;
    public GameObject Helmetprefab;
    public GameObject ChestPlaceprefab;
    public GameObject PantArmorprefab;
    [SerializeField]private GameObject Helmet;
     [SerializeField]private GameObject ChestPlace;
     [SerializeField]private GameObject PantArmor;
    [SerializeField]private ClassifyBodyBone[] playerBones;
    public void  Inital(){
         FindPlayerSkin();
        playerBones = PlayerSkin.GetComponentsInChildren<ClassifyBodyBone>();
        // Debug.Log("Player Bones"+playerBones.Length);
    }
    private void Start()
    {
        // Debug.Log("Start in Player equipment run");
        Inital();
        EquipAllArmor();
    }
    public void InitalBones(GameObject skin){
        this.PlayerSkin = skin;
    }
    public void EquipAllArmor(){
        if(Helmetprefab != null && Helmet == null){
           Helmet = EquipArmor(Helmetprefab);
        }
        if(ChestPlaceprefab != null && ChestPlace == null){
           ChestPlace = EquipArmor(ChestPlaceprefab);
        }
        if(PantArmorprefab != null && PantArmor == null){
           PantArmor = EquipArmor(PantArmorprefab);
        }   
    }
    public GameObject EquipArmor(GameObject armorPrefab)
    {
        Debug.Log("Player Skin" + PlayerSkin.name);
        var armorObject = Instantiate(armorPrefab,this.transform);
        ClassifyBodyBone[] boneArray = armorObject.GetComponentsInChildren<ClassifyBodyBone>();
        foreach (ClassifyBodyBone armorBone in boneArray)
        {
            ClassifyBodyBone matchingBone = findMatchBone(armorBone);
            if (matchingBone != null)
            {
                armorBone.transform.SetParent(matchingBone.transform);
                armorBone.transform.localPosition = Vector3.zero;
                armorBone.transform.localRotation = Quaternion.identity;
            }
        }
        return armorObject;
    }

    public ClassifyBodyBone findMatchBone(ClassifyBodyBone boneToFind)
    {
        Debug.Log("" + boneToFind.name);
        if(playerBones.Length == 0){
            playerBones = PlayerSkin.GetComponentsInChildren<ClassifyBodyBone>();
            Debug.Log("Player bones" + playerBones.Length);
        }
        foreach (ClassifyBodyBone bone in playerBones)
        {
            Debug.Log("For each run finding the bone");
            if (bone.bodyClassify == boneToFind.bodyClassify)
            {
                return bone;
            }else{

                Debug.Log( bone.bodyClassify.ToString() + boneToFind.bodyClassify.ToString() );
            }
        }
        Debug.Log("Cannot find any bone match");
        return null;
    }
    public void FindPlayerBones(){
         if (PlayerSkin != null)
        {
            playerBones = PlayerSkin.GetComponentsInChildren<ClassifyBodyBone>();
        }else{
            Debug.LogError("Player Skin is Null");
        }
    }
    public void UnEquipItem(ItemBase item){
        if(item is Helmet){
            this.Helmetprefab = null; 
            Destroy(Helmet);
            Helmet = null;
        }
        if(item is Armor){
             this.ChestPlaceprefab = null; 
            Destroy(ChestPlace);
            ChestPlace = null;
        }
         if(item is LowerArmor){
             this.PantArmorprefab = null; 
            Destroy(PantArmor);
            PantArmor = null;
        }
    }
    public void FindPlayerSkin(){
        if(PlayerSkin == null){
            PlayerSkin = this.gameObject;
            StartCoroutine(WaitBeforeFindSkin());
        }else{
            return;
        }
    }
    IEnumerator WaitBeforeFindSkin(){
       yield return new WaitForSeconds(2f);
        FindPlayerSkin();
    }
    public void ClearArmor(){
       if(Helmet != null){
        Destroy(Helmet);
        Helmetprefab = null;
       }

       if(PantArmor != null){
        Destroy(PantArmor);
        PantArmorprefab = null;
       }
       if(ChestPlace != null){
        Destroy(ChestPlace);
        ChestPlaceprefab = null;
       }
    }
}
