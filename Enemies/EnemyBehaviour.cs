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

    public float look_radius;
    public float attack_radius;
    public float speed;

    [Header("STATS")] // This need base class for enemies
    public bool dead;

    // Attack
    public float next_attack;
    public float attack_rate = 1f;

    // Target
    Transform target_player;

    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
        target_player = PlayerManager.instance.player.transform;
        dead = false;
    }

    // Update is called once per frame
    void Update()
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
        if (distance <= look_radius && PlayerManager.instance.player.GetComponent<PlayerBehavior>().IsAlive())
        {
            MoveAndAttack();
        }
        else 
        {
            walking = false;
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


    // DEBUG: VISION GIZMO 
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position + new Vector3(0,0.2f,0), transform.up, transform.right, 360, look_radius);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, attack_radius);
    }
}
