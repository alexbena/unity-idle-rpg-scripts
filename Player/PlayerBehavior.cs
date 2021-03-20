using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : Interactable
{

    public BaseClass player_info;

    public bool dead;


    public float look_radius;
    public float attack_radius;

    // GUI MAKE THIS INTO CONTROLLER
    private Text ui_level;

    // Enemy Focus
    GameObject[] enemies;
    GameObject actual_target;

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

        if (actual_target == null)
        {
            actual_target = GetNearEnemy();
        }
        else if (!actual_target.GetComponent<EnemyBehaviour>().IsAlive()) // Mantain focus on enemy even if gets a bit far
        {
            actual_target = GetNearEnemy();
        }
        else {
            Attack();
        }
    }
    GameObject GetNearEnemy() 
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies) 
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance <= look_radius && e.GetComponent<EnemyBehaviour>().IsAlive()) 
            {
                return e;
            }
        }
        return null;
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


    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, look_radius);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, attack_radius);
    }
}
