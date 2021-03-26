using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationTerrain : MonoBehaviour
{
    public GameObject[] squares;
    public GameObject[] checkpoint_squares;
    public GameObject[] boss_squares;

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

        // Square Decision
        if (PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.forest_distance % 10 == 8)
        {
            last_build = Instantiate(boss_squares[Random.Range(0, boss_squares.Length)], position, last_build.transform.rotation);
        }
        else if (PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.forest_distance % 10 == 9)
        {
            PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.forest_checkpoint++;
            last_build = Instantiate(checkpoint_squares[Random.Range(0, checkpoint_squares.Length)], position, last_build.transform.rotation);
        }
        else
        {
            last_build = Instantiate(squares[Random.Range(0, squares.Length)], position, last_build.transform.rotation);
        }

        // Automove update route
        GameObject wp = last_build.transform.Find("WP_endblock").gameObject;
        PlayerManager.instance.player.GetComponent<AutoMove>().AddWP("route_grind", wp);
        squares_ingame.Add(last_build);

        PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.forest_distance++;
        GUIManager.instance.UpdateDistance(PlayerManager.instance.player.GetComponent<PlayerBehavior>().player_info.forest_distance);
    }
}