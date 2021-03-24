using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Automated ROUTES for player
public class AutoMove : MonoBehaviour
{
    public Dictionary<string, List<GameObject>> routes = new Dictionary<string, List<GameObject>>();
    private List<GameObject> active_route;
    public GameObject player;
    int current = -1;
    public float speed;
    float proximity_radius = 1f;
    private Animator anim;
    private string active_route_name;


    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject[] main_routes = GameObject.FindGameObjectsWithTag("ROUTE_GRIND");
        List<GameObject> new_list = new List<GameObject>(main_routes);
        routes.Add("route_grind", new_list);
        this.active_route = new_list;
        active_route_name = "route_grind";

        main_routes = GameObject.FindGameObjectsWithTag("ROUTE_HOME");
        new_list = new List<GameObject>(main_routes);
        routes.Add("route_home", new_list);
    }

    void Update()
    {
        if(current != -1 && !PlayerManager.instance.player.GetComponent<PlayerBehavior>().attacking) {
            transform.LookAt(active_route[current].transform.position);

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, active_route[current].transform.position, step);
            
            // TODO simplify
            if (Vector3.Distance(transform.position, active_route[current].transform.position) < proximity_radius)
            {
                current++;
                if (current >= active_route.Count) { 
                    current = -1;
                    anim.SetBool("isWalking", false); // Take anims out
                    if(active_route_name == "route_grind")
                        AssetsManager.instance.Level_changer.GetComponent<LevelChanger>().FadeToLevel(1);
                }
            }
        }

    }

    public void Move() {
        this.current = 0;
        anim.SetBool("isWalking", true);
    }

    public void LoadRoute(string route) {
        this.active_route = this.routes[route];
        active_route_name = route;
        Move();
        if (active_route_name == "route_home")
            StartCoroutine("GoHomeTransition");
        
    }

    IEnumerator GoHomeTransition()
    {
        yield return new WaitForSeconds(2); // Wait 2 segs
        AssetsManager.instance.Level_changer.GetComponent<LevelChanger>().FadeToLevel(0);
    }
}
