using UnityEditor;
using UnityEngine;

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

    public LevelSystem level_system;

    public float GetHealthPercent() 
    {
        return (float)player_info.cur_health/player_info.max_health;
    }

    void Start()
    {
        level_system = GetComponent<LevelSystem>();
        LoadInfo();

        dead = false;
        anim = GetComponent<Animator>();
        actual_target = null;
        audio_sfx = GetComponent<AudioSource>();

        GUIManager.instance.UpdateHealth(player_info.cur_health, player_info.max_health, GetHealthPercent());
    }

    // Update is called once per frame
    void Update()
    {
        // Do attack on click and implement spells

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

        GUIManager.instance.UpdateHealth(player_info.cur_health, player_info.max_health, GetHealthPercent());
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
        GUIManager.instance.UpdateGold(player_info.gold);
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
        GameObject save_data = GameObject.FindGameObjectWithTag("SaveData");
        // Player
        save_data.GetComponent<PlayerInfo>().cur_health = player_info.cur_health;
        save_data.GetComponent<PlayerInfo>().max_health = player_info.max_health;
        save_data.GetComponent<PlayerInfo>().player_name = player_info.player_name;
        save_data.GetComponent<PlayerInfo>().gold = player_info.gold;
        save_data.GetComponent<PlayerInfo>().current_xp = player_info.current_XP;        // Get this from player
        save_data.GetComponent<PlayerInfo>().current_level = player_info.current_level;  // Get this from player

        // Level System
        save_data.GetComponent<PlayerInfo>().xp_for_next_level = level_system.xp_for_next_level;
        save_data.GetComponent<PlayerInfo>().xp_difference_next_level = level_system.xp_difference_next_level;
        save_data.GetComponent<PlayerInfo>().total_xp_difference = level_system.total_xp_difference;
        save_data.GetComponent<PlayerInfo>().fill_amount = level_system.fill_amount;
        save_data.GetComponent<PlayerInfo>().reverse_fill_amount = level_system.reverse_fill_amount;
        save_data.GetComponent<PlayerInfo>().stat_points = level_system.stat_points;
        save_data.GetComponent<PlayerInfo>().skill_points = level_system.skill_points;
    }

    void LoadInfo() 
    {
        GameObject save_data = GameObject.FindGameObjectWithTag("SaveData");
        if (save_data.GetComponent<PlayerInfo>().current_level != 0)
        {
            // Player          
            player_info.cur_health = save_data.GetComponent<PlayerInfo>().cur_health;
            player_info.max_health = save_data.GetComponent<PlayerInfo>().max_health;
            player_info.player_name = save_data.GetComponent<PlayerInfo>().player_name;         
            player_info.gold = save_data.GetComponent<PlayerInfo>().gold;
            player_info.current_XP = save_data.GetComponent<PlayerInfo>().current_xp;
            player_info.current_level = save_data.GetComponent<PlayerInfo>().current_level;

            // Level System
            level_system.current_xp = player_info.current_XP;
            level_system.current_level = player_info.current_level;
            level_system.xp_for_next_level = save_data.GetComponent<PlayerInfo>().xp_for_next_level;
            level_system.xp_difference_next_level = save_data.GetComponent<PlayerInfo>().xp_difference_next_level;
            level_system.total_xp_difference = save_data.GetComponent<PlayerInfo>().total_xp_difference;
            level_system.fill_amount = save_data.GetComponent<PlayerInfo>().fill_amount;
            level_system.reverse_fill_amount = save_data.GetComponent<PlayerInfo>().reverse_fill_amount;
            level_system.stat_points = save_data.GetComponent<PlayerInfo>().stat_points;
            level_system.skill_points = save_data.GetComponent<PlayerInfo>().skill_points;
            level_system.AddXP(0);
        }
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
