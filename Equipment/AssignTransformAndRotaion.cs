using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTransformAndRotaion : MonoBehaviour
{
   public GameObject PlayerSkin;
    public GameObject Helmetprefab;
    public GameObject ChestPlaceprefab;
    public GameObject PantArmorprefab;
    //////////
    [TextArea]
    public string something;
    public GameObject Helmet;
    public GameObject ChestPlace;
    public GameObject PantArmor;
    private ClassifyBodyBone[] playerBones;

    private void Start()
    {
        if (PlayerSkin != null)
        {
            playerBones = PlayerSkin.GetComponentsInChildren<ClassifyBodyBone>();
        }

        if (Helmetprefab != null)
        {
            Helmet = Instantiate(Helmetprefab, transform.position, Quaternion.identity,transform);
            EquipArmor(Helmet);
        }
        
        if (ChestPlaceprefab != null)
        {
            ChestPlace = Instantiate(ChestPlaceprefab, transform.position,Quaternion.identity);
            EquipArmor(ChestPlace);
        }
        
        if (PantArmorprefab != null)
        {
            PantArmor = Instantiate(PantArmorprefab, transform.position,Quaternion.identity);
            EquipArmor(PantArmor);
        }
        
    }

    public void EquipArmor(GameObject armorPrefab)
    {
        ClassifyBodyBone[] boneArray = armorPrefab.GetComponentsInChildren<ClassifyBodyBone>();
        foreach (ClassifyBodyBone armorBone in boneArray)
        {
            ClassifyBodyBone matchingBone = findMatchBone(armorBone);
            if (matchingBone != null)
            {
                // Vector3 scale = armorBone.transform.localScale;
                // armorBone.transform.SetParent(matchingBone.transform);
                // armorBone.transform.localPosition = Vector3.zero;
                // armorBone.transform.localRotation = Quaternion.identity;
                armorBone.transform.position = matchingBone.transform.position;
                armorBone.transform.rotation = matchingBone.transform.rotation;
            }
        }
    }
    private void Update() {
         if (PlayerSkin != null)
        {
            playerBones = PlayerSkin.GetComponentsInChildren<ClassifyBodyBone>();
        }

        if (Helmetprefab != null)
        {
            // Helmet = Instantiate(Helmetprefab, transform.position, Quaternion.identity,transform);
            EquipArmor(Helmet);
        }
        
        if (ChestPlaceprefab != null)
        {
            // ChestPlace = Instantiate(ChestPlaceprefab, transform.position,Quaternion.identity);
            EquipArmor(ChestPlace);
        }
        
        if (PantArmorprefab != null)
        {
            // PantArmor = Instantiate(PantArmorprefab, transform.position,Quaternion.identity);
            EquipArmor(PantArmor);
        }
    }
    public ClassifyBodyBone findMatchBone(ClassifyBodyBone boneToFind)
    {
        foreach (ClassifyBodyBone bone in playerBones)
        {
            if (bone.bodyClassify == boneToFind.bodyClassify)
            {
                return bone;
            }
        }
        return null;
    }
}