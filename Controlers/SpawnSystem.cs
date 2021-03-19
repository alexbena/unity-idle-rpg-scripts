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
            float x_spawn_pos = transform.position.x + Random.Range(-spawn_range, spawn_range);
            float z_spawn_pos = transform.position.z + Random.Range(-spawn_range, spawn_range);

            Vector3 spawn_point = new Vector3(x_spawn_pos, 0, z_spawn_pos);
            GameObject new_enemy = (GameObject)Instantiate(enemies_spawnables[Random.Range(0,enemies_spawnables.Length)], spawn_point, Quaternion.identity);

        }
    }
}
