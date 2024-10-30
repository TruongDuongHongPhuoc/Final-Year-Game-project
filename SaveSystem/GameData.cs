using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
[System.Serializable]
public class GameData
{
   [SerializeField]public  PlayerData playerData;
   [SerializeField] public UnitData unitData;
   [SerializeField] public EquipmentData equipmentData;
   [SerializeField] public SkillTreeData skillTreeData;
   [SerializeField] public InventoryData inventoryData;
   [SerializeField] public QuestData questData;
   [SerializeField] public TriggerData triggerData;
   [SerializeField] public EventStoriesData eventStoriesData;
}