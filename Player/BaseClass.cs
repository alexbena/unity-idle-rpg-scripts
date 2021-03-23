using UnityEngine;

[System.Serializable]
public class BaseClass
{
    public enum genders 
    { 
        MALE,
        FEMALE
    }

    [Header("Info")]
    public string player_name;
    public int current_level;
    public int gold;

    [Header("Health & Stamina")]
    public int current_XP;
    public int cur_health;
    public int max_health;
    public int cur_stamina;
    public int max_stamina;

    [Header("Gender")]
    public genders gender;

    [Header("Stats")]
    public int strength;
    public int endurance;
    public int agility;
    public int wisdom;
    public int intelligence;

    [Header("Skills")]
    public int statPoints;
    public int skillPoints;
    // List of skills
}
