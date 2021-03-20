using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    private bool has_spawned;
    private Transform player;

    public GameObject[] enemies_spawnables;

    public int max_spawn_amount;
    
    public float spawn_range;
    public float detection_range;

    //DEBUG GIZMOS
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, detection_range);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, spawn_range);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detection_range) 
        {
            if (!has_spawned)
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies() 
    {
        has_spawned = true;

        // Randomized spawnable amount
        int spawn_amount = Random.Range(1, max_spawn_amount);

        for (int i = 0; i < spawn_amount; i++) 
        {
            float theta = 360f * Random.value; // Part of the spawn angle randomized
            float radius = Random.Range(0f, spawn_range);

            Vector3 center = transform.position;
            Vector3 point = new Vector3(radius * Mathf.Sin(theta),0,radius * Mathf.Cos(theta)); // Classic point in a circle

            Vector3 spawn_point = center + point;

            // Direction on spawn
            float direct = 360f * Random.value;
            Quaternion spawn_direction = Quaternion.Euler(0, direct, 0); 


            GameObject new_enemy = (GameObject)Instantiate(enemies_spawnables[Random.Range(0,enemies_spawnables.Length)], spawn_point, spawn_direction);

        }
    }
}
