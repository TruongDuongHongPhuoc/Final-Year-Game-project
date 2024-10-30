using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class SkillTreeData
{
   public List<string> unlockedSkill = new List<string>();
    public SkillTreeData(List<string> lists){
        this.unlockedSkill = lists;
    }
}
