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

    public void AddXP(int xp_amount) 
    {
        CalculateLevel(xp_amount);
        GUIManager.instance.UpdateLevel(current_level, reverse_fill_amount);
    }

    void CalculateLevel(int amount) 
    {
        current_xp += amount;

        int temp_cur_level = (int)Mathf.Sqrt(current_xp / base_XP) + 1;

        if (current_level != temp_cur_level) 
        {
            current_level = temp_cur_level;
            PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.current_level = current_level;
            stat_points = 5;
            skill_points = 15;
            PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.skill_points += skill_points;
            PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.stat_points += stat_points;
            PlayerManager.instance.player.GetComponent<PlayerBehavior>().LevelUP();
        }

        xp_for_next_level = base_XP * current_level * current_level;
        xp_difference_next_level = xp_for_next_level - current_xp;
        total_xp_difference = xp_for_next_level - (base_XP * (current_level-1) * (current_level-1));

        fill_amount = (float) xp_difference_next_level / (float) total_xp_difference;
        reverse_fill_amount = 1 - fill_amount;

        PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.current_XP = current_xp;
    }

}
