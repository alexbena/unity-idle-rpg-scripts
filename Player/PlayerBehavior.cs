using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : Interactable
{

    public BaseClass player_info;

    public bool dead;
    public bool attacking;

    Animator anim;

    public float look_radius;
    public float attack_radius;
    public float next_attack;
    public float attack_rate = 1f;

    // GUI MAKE THIS INTO CONTROLLER
    private TextMeshProUGUI ui_level;
    private TextMeshProUGUI ui_health_bar_text;
    private GameObject ui_health_bar;
    private Text ui_gold;

    // Get this out
    public AudioClip hit_sfx;
    public AudioClip critical_hit_sfx;
    public AudioClip level_up_sfx;
    AudioSource audio_sfx;

    // EFFECTS
    public GameObject hit_vfx;
    public GameObject critical_hit_vfx;
    public GameObject level_up_vfx;

    // Enemy Focus
    GameObject[] enemies;
    GameObject actual_target;


    private float GetHealthPercent() 
    {
        return (float)player_info.cur_health/player_info.max_health;
    }
    // Start is called before the first frame update

    private void Awake()
    {    }
    void Start()
    {
        ui_level = GameObject.Find("UI_level").transform.GetComponent<TextMeshProUGUI>();
        ui_health_bar_text = GameObject.Find("UI_health_bar_text").transform.GetComponent<TextMeshProUGUI>();
        ui_health_bar = GameObject.Find("UI_health_bar");
        ui_gold = GameObject.Find("UI_gold").GetComponent<Text>();
        dead = false;
        anim = GetComponent<Animator>();
        actual_target = null;
        audio_sfx = GetComponent<AudioSource>();

        ui_health_bar.transform.localScale = new Vector3(GetHealthPercent(), 1, 1);
        ui_health_bar_text.text = player_info.cur_health + " / " + player_info.max_health;

    }

    // Update is called once per frame
    void Update()
    {
        if (ui_level.text != player_info.current_level.ToString())
            LevelUP();

        ui_level.text = player_info.current_level.ToString();
        ui_gold.text = player_info.gold.ToString();

        if (actual_target == null)
        {
            attacking = false;
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
            attacking = true;
            anim.SetBool("isAttacking", true);
            if (Time.time > next_attack)
            {
                next_attack = Time.time + attack_rate;
                int attack_dmg = Random.Range(5, 30);
                bool is_critial = false;
                if (attack_dmg > 20)
                {
                    is_critial = true;
                }
                else
                {
                    is_critial = false;
                }
                anim.SetTrigger("Attack");

                DamagePopUp.Create(actual_target.transform.position, attack_dmg, is_critial);

                if (is_critial)
                {
                    GameObject effect = (GameObject)Instantiate(critical_hit_vfx, transform.GetChild(1).GetChild(0).GetChild(5).position, Quaternion.identity);
                    Destroy(effect, 0.5f);
                    audio_sfx.clip = critical_hit_sfx;
                }
                else
                {
                    GameObject effect = (GameObject)Instantiate(hit_vfx, transform.GetChild(1).GetChild(0).GetChild(5).position, Quaternion.identity);
                    Destroy(effect, 0.5f);
                    audio_sfx.clip = hit_sfx;
                }
                audio_sfx.volume = 0.03f;
                audio_sfx.Play();
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

        ui_health_bar_text.text = player_info.cur_health + " / " + player_info.max_health;
        ui_health_bar.transform.localScale = new Vector3(GetHealthPercent(), 1, 1);
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

    public void LevelUP() 
    {
        GameObject effect = (GameObject)Instantiate(level_up_vfx, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        audio_sfx.clip = level_up_sfx;
        audio_sfx.volume = 0.15f;
        audio_sfx.Play();
    }

    private void OnDestroy()
    {
        SaveInfo();
    }

    void SaveInfo()
    {
        PlayerInfo.instance.current_XP = player_info.current_XP;
        PlayerInfo.instance.cur_health = player_info.cur_health;
        PlayerInfo.instance.max_health = player_info.max_health;
        PlayerInfo.instance.name = player_info.name;
        PlayerInfo.instance.current_level = player_info.current_level;
        PlayerInfo.instance.gold = player_info.gold;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, look_radius);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, attack_radius);
    }
#endif
}
