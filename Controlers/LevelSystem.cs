using UnityEngine;

public class LevelSystem : MonoBehaviour
{

    public int current_level;
    public int base_XP = 20;
    public int current_xp;

    public int xp_for_next_level;
    public int xp_difference_next_level;
    public int total_xp_difference;

    public float fill_amount;
    public float reverse_fill_amount;

    public int stat_points;
    public int skill_points;

    private GameObject ui_fill_bar; // Take this out to controller

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        ui_fill_bar = GameObject.Find("UI_fill_bar");
    }

    public void AddXP(int xp_amount) 
    {
        CalculateLevel(xp_amount);
        ui_fill_bar.transform.localScale = new Vector3(reverse_fill_amount, 1, 1);
    }

    void CalculateLevel(int amount) 
    {
        current_xp += amount;

        int temp_cur_level = (int)Mathf.Sqrt(current_xp / base_XP) + 1;

        if (current_level != temp_cur_level) 
        {
            current_level = temp_cur_level;
            player.GetComponent<PlayerBehavior>().player_info.current_level = current_level;
        }

        xp_for_next_level = base_XP * current_level * current_level;
        xp_difference_next_level = xp_for_next_level - current_xp;
        total_xp_difference = xp_for_next_level - (base_XP * (current_level-1) * (current_level-1));

        fill_amount = (float) xp_difference_next_level / (float) total_xp_difference;
        reverse_fill_amount = 1 - fill_amount;

        stat_points = 5 * (current_level - 1);
        skill_points = 15 * (current_level - 1);

        player.GetComponent<PlayerBehavior>().player_info.current_XP = current_xp;
    }

}
