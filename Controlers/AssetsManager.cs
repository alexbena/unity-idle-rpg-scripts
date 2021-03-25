using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// REFERENCES TO ALL ASSETS NEEDED
public class AssetsManager : MonoBehaviour
{
    public static AssetsManager instance;


    void Awake()
    {
        instance = this;
    }

    public GameObject popup_damage;
    public GameObject Level_changer;
    public GameObject game_controller;
    public GameObject procedural_generator;
}
