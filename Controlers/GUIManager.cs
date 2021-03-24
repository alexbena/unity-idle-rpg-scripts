using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ui_level_text = instance.GO_ui_level.GetComponent<TextMeshProUGUI>();
        ui_health_bar_text = instance.GO_ui_health_bar_text.GetComponent<TextMeshProUGUI>();
        ui_gold = instance.GO_ui_gold.GetComponent<Text>();
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

    public void UpdateHealth(int current_health, int max_health, float percent) 
    {
        ui_health_bar_text.text = current_health + " / " + max_health;
        GO_ui_health_bar.transform.localScale = new Vector3(percent, 1, 1);
    } 

    public void UpdateLevel(int current_level, float fill_amount) 
    {
        ui_level_text.text = current_level.ToString();
        GO_ui_fill_bar.transform.localScale = new Vector3(fill_amount, 1, 1);

    }

    public void UpdateGold(int amount)
    {
        ui_gold.text = amount.ToString();
    }
}
