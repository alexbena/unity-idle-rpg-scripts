using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddXP", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddXP() 
    {
        CalculateLevel(5);
    }

    void CalculateLevel(int amount) 
    {
        current_xp += amount;

        int temp_cur_level = (int)Mathf.Sqrt(current_xp / base_XP);

        if (current_level != temp_cur_level) 
        {
            current_level = temp_cur_level;
        }

        xp_for_next_level = base_XP * (current_level + 1) * (current_level + 1);
        xp_difference_next_level = xp_for_next_level - current_xp;
        total_xp_difference = xp_for_next_level - (base_XP * current_level * current_level);

        fill_amount = (float) xp_difference_next_level / (float) total_xp_difference;
        reverse_fill_amount = 1 - fill_amount;

    }
}
