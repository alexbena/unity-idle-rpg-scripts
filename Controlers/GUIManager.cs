using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    PlayerBehavior player_script;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player_script = PlayerManager.instance.GetComponent<PlayerBehavior>();
        ui_level_text = GUIManager.instance.GO_ui_level.GetComponent<TextMeshProUGUI>();
        ui_health_bar_text = GUIManager.instance.GO_ui_health_bar_text.GetComponent<TextMeshProUGUI>();
        ui_gold = GUIManager.instance.GO_ui_gold.GetComponent<Text>();
    }


    // Health Control
    public GameObject GO_ui_health_bar;
    public GameObject GO_ui_health_bar_text;
    TextMeshProUGUI ui_health_bar_text;

    // Level Control
    public GameObject GO_ui_level;
    TextMeshProUGUI ui_level_text;
    public GameObject GO_ui_fill_bar;

    // Currency Control
    public GameObject GO_ui_gold;
    Text ui_gold;

    public void UpdateHealth() 
    {
        ui_health_bar_text.text = player_script.player_info.cur_health + " / " + player_script.player_info.max_health;
        GO_ui_health_bar.transform.localScale = new Vector3(player_script.GetHealthPercent(), 1, 1);
    } 

    public void UpdateLevel(float fill_amount) 
    {
        ui_level_text.text = player_script.player_info.current_level.ToString();
        GO_ui_fill_bar.transform.localScale = new Vector3(fill_amount, 1, 1);

    }

    public void UpdateGold()
    {
        ui_gold.text = player_script.player_info.gold.ToString();
    }
}
