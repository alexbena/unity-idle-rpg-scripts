using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyBehaviour : Interactable
{

    private Animator anim;
    private bool walking;

    [Header("STATS")] // This need base class for enemies
    public bool dead;
    public float look_radius;
    public float attack_radius;
    public float speed;
    public float next_attack;
    public float attack_rate = 1f;
    public int max_health;
    public int cur_health;

    // DROP (Split in drop table)
    public int xp_drop = 20;
    public int gold_drop;
    // Target
    Transform target_player;

    // CHECK
    public AudioClip hit_sfx;
    public AudioClip die_sfx;
    AudioSource audio_sfx;

    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
        target_player = PlayerManager.instance.player.transform;
        dead = false;
        audio_sfx = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive() && PlayerManager.instance.player.GetComponent<PlayerBehavior>().IsAlive())
        {
            this.anim.SetBool("walking", walking);

            if (!walking)
            {
                this.anim.SetBool("idling", true); // Need change for fighting
            }
            else
            {
                this.anim.SetBool("idling", false);
            }

            float distance = Vector3.Distance(transform.position, target_player.position);
            if (distance <= look_radius)
            {
                MoveAndAttack();
            }
            else
            {
                this.anim.SetBool("fighting", false);
                walking = false;
            }
        }
        else
        {
            this.anim.SetBool("idling", true);
        }
    }

    void MoveAndAttack()
    {
        transform.LookAt(target_player.position);

        float step = speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target_player.position) > attack_radius)
            transform.position = Vector3.MoveTowards(transform.position, target_player.position, step);

        // TODO simplify
        if (Vector3.Distance(transform.position, target_player.position) <= attack_radius)
        {
            this.walking = false;
            this.anim.SetBool("fighting", false);

            if (Time.time > next_attack)
            {
                next_attack = Time.time + attack_rate;
                this.anim.SetBool("fighting", true);
                audio_sfx.clip = hit_sfx;
                audio_sfx.volume = 0.04f;
                audio_sfx.Play();

                // HIT
                PlayerManager.instance.player.GetComponent<PlayerBehavior>().GetHit(20); // this needs correction
            }
            this.walking = false;
        }
        else
        {
            this.walking = true;
        }
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
            cur_health -= damage;
        }
        else
        {
            cur_health = 0;
            Die();
        }

    }

    public bool WillDie(int damage)
    {
        return (cur_health - damage) <= 0 ? true : false;
    }

    public void Die()
    {
        GiveXP();
        GiveDrop();
        dead = true;
        // Dead effect
        audio_sfx.clip = die_sfx;
        audio_sfx.volume = 0.1f;
        audio_sfx.Play();
        Rigidbody rigi = GetComponent<Rigidbody>();
        rigi.constraints = RigidbodyConstraints.None;
        rigi.AddForce(transform.up * 300);
        rigi.AddTorque(transform.forward * -100);
        rigi.AddForce(transform.forward * -100);
        Destroy(this.gameObject, 5);
    }

    public bool IsAlive()
    {
        return !dead;
    }

    public void GiveXP() 
    {
        PlayerManager.instance.player.GetComponent<PlayerBehavior>().level_system.AddXP(xp_drop); // Change to GameController Manager
    }

    public void GiveDrop() 
    {
        gold_drop = Random.Range(1, 5);
        PlayerManager.instance.player.GetComponent<PlayerBehavior>().AddGold(gold_drop);
    }


    // DEBUG: VISION GIZMO 
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position + new Vector3(0,0.2f,0), transform.up, transform.right, 360, look_radius);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, attack_radius);
    }
#endif
}
