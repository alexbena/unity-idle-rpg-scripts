using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public BaseClass player_info;
    // Start is called before the first frame update
    void Start()
    {
        ui_level = GameObject.Find("UI_level").GetComponent<Text>();
        ui_fill_bar = GameObject.Find("UI_fill_bar").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
