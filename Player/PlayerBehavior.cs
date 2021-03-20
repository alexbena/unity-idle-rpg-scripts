using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{

    public BaseClass player_info;

    // GUI MAKE THIS INTO CONTROLLER
    private Text ui_level;
    
    // Start is called before the first frame update
    void Start()
    {
        ui_level = GameObject.Find("UI_level").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ui_level.text = "Level " + player_info.current_level;
        
    }
}
