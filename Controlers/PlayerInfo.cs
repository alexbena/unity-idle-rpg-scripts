using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo : MonoBehaviour
{

    public static PlayerInfo instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public enum genders
    {
        MALE,
        FEMALE
    }

    [Header("Info")]
    public string player_name;
    public int gold;

    [Header("Health & Stamina")]
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

    [Header("Level System")]
    public int current_level;
    public int current_xp;
    public int base_XP = 20;

    public int xp_for_next_level;
    public int xp_difference_next_level;
    public int total_xp_difference;

    public float fill_amount;
    public float reverse_fill_amount;

    public int stat_points;
    public int skill_points;
}

