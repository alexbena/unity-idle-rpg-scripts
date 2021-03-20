using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : Interactable
{

    public BaseClass player_info;

    public bool dead;

    // GUI MAKE THIS INTO CONTROLLER
    private Text ui_level;
    
    // Start is called before the first frame update
    void Start()
    {
        ui_level = GameObject.Find("UI_level").GetComponent<Text>();
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        ui_level.text = "Level " + player_info.current_level;
        
    }

    // INTERACTIONS
    public override void Interact()
    {
        base.Interact();
    }

    public override void GetHit(int damage)
    {
        base.GetHit(damage);

        if (!WillDie(damage))
        {
            player_info.cur_health -= damage;
        }
        else
        {
            Die();
        }

    }

    public bool WillDie(int damage)
    {
        return (player_info.cur_health - damage) <= 0 ? true : false;
    }

    public void Die() 
    {
        dead = true;
    }

    public bool IsAlive() 
    {
        return !dead;
    }
}
