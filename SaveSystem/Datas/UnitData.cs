using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UnitData
{
     public string name;
    public int currentHeal;
    public int maxHeal;
    public int shield;
    public int maxShield;
    public int armor;
    public int damage;
    public int speed;
    public int level;
    public int experience;
    public int ExperienceRequire;
    public List<string> skillsNames;
    public UnitData(string name, int currentHeal, int maxHeal, int shield, int maxShield, int armor, int damage, int speed, int level, int experience ,int ExperienceRequire, List<string> skillsNames)
    {
        this.name = name;
        this.currentHeal = currentHeal;
        this.maxHeal = maxHeal;
        this.shield = shield;
        this.maxShield = maxShield;
        this.armor = armor;
        this.damage = damage;
        this.speed = speed;
        this.level = level;
        this.experience = experience;
        this.ExperienceRequire = ExperienceRequire;;
        this.skillsNames = skillsNames;
    }
}
