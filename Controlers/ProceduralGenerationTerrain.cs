using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationTerrain : MonoBehaviour
{
    public GameObject[] squares;

    List<GameObject> squares_ingame = new List<GameObject>();
    public GameObject last_build;
    // Start is called before the first frame update
    void Start()
    {
        squares_ingame.Add(last_build);
    }

    private void Update()
    {
        if (squares_ingame.Count > 2) 
        {
            Destroy(squares_ingame[0]);
            squares_ingame.RemoveAt(0);
        }
    }

    public void BuildNext() 
    {
        Vector3 position = last_build.transform.position;
        position.x += 20;
        last_build = Instantiate(squares[Random.Range(0, squares.Length)], position, last_build.transform.rotation);
        
        // Automove update route
        GameObject wp = last_build.transform.Find("WP_endblock").gameObject;
        PlayerManager.instance.player.GetComponent<AutoMove>().AddWP("route_grind", wp);
        squares_ingame.Add(last_build);
    }
}