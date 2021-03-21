using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : Interactable
{

    public BaseClass player_info;

    public bool dead;

    Animator anim;

    public float look_radius;
    public float attack_radius;
    public float next_attack;
    public float attack_rate = 1f;

    // GUI MAKE THIS INTO CONTROLLER
    private Text ui_level;
    private Text ui_gold;

    // Enemy Focus
    GameObject[] enemies;
    GameObject actual_target;

    // Start is called before the first frame update
    void Start()
    {
        ui_level = GameObject.Find("UI_level").GetComponent<Text>();
        ui_gold = GameObject.Find("UI_gold").GetComponent<Text>();
        dead = false;
        anim = GetComponent<Animator>();
        actual_target = null;
    }

    // Update is called once per frame
    void Update()
    {
        ui_level.text = "Level " + player_info.current_level;
        ui_gold.text = "Gold " + player_info.gold;

        if (actual_target == null)
        {
            anim.SetBool("isAttacking", false);
            actual_target = GetNearEnemy();
        }
        else if (!actual_target.GetComponent<EnemyBehaviour>().IsAlive()) // Mantain focus on enemy even if gets a bit far
        {
            anim.SetBool("isAttacking", false);
            actual_target = GetNearEnemy();
        }
        else {
            transform.LookAt(actual_target.transform.position);              
            Attack();
        }
        
    }

    void Attack() 
    {
        if (Vector3.Distance(transform.position, actual_target.transform.position) <= attack_radius)
        {
            anim.SetBool("isAttacking", true);
            if (Time.time > next_attack)
            {
                next_attack = Time.time + attack_rate;
                int attack_dmg = Random.Range(5, 30);
                bool is_critial;
                if (attack_dmg > 15)
                {
                    is_critial = true;
                }
                else 
                {
                    is_critial = false;
                }

                DamagePopUp.Create(actual_target.transform.position, attack_dmg, is_critial);
                // HIT
                actual_target.GetComponent<EnemyBehaviour>().GetHit(attack_dmg); // this needs correction
            }
            
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
            player_info.cur_health = 0;
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
        Destroy(this.gameObject);
    }

    public bool IsAlive() 
    {
        return !dead;
    }

    public void AddGold(int gold) 
    {
        player_info.gold += gold;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, look_radius);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, attack_radius);
    }
}
