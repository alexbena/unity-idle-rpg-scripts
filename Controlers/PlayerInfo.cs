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
    public string name;
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

