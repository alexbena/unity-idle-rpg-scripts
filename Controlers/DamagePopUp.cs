using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{

    private TextMeshPro text_mesh;
    private float disappear_time;
    private const float MAX_DISAPPEAR_TIME = 1f; // for half time detection
    private Color text_color;
    private Vector3 move_vector;
    private static int sorting_order;

    public static DamagePopUp Create(Vector3 position, int damage_amount, bool is_critical) 
    {
        GameObject damage_pop = Instantiate(AssetsManager.instance.popup_damage, position, Quaternion.identity);
        DamagePopUp damage_popup_script = damage_pop.GetComponent<DamagePopUp>();
        damage_popup_script.Setup(damage_amount, is_critical);

        return damage_popup_script;
    }

  
    private void Awake()
    {
        text_mesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damage_amount, bool is_critical) 
    {
        text_mesh.SetText(damage_amount.ToString());
        if (is_critical)
        {
            text_mesh.fontSize = 9;
            text_mesh.color = new Color(255, 9, 0);
        }
        else
        {
            text_mesh.fontSize = 5;
            text_mesh.color = new Color(255, 106, 0);
        }

        text_color = text_mesh.color;
        disappear_time = 1f;

        sorting_order++;
        text_mesh.sortingOrder = sorting_order;

        move_vector = new Vector3(0.7f, 1) * 3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        transform.position += move_vector * Time.deltaTime;
        move_vector -= move_vector * 8f * Time.deltaTime;

        if (disappear_time > MAX_DISAPPEAR_TIME * 0.5) // first half lifetime
        {
            float increase_scale_speed = 1f;
            transform.localScale += Vector3.one * increase_scale_speed * Time.deltaTime;
        }
        else 
        {
            float decrease_scale_speed = 1f;
            transform.localScale -= Vector3.one * decrease_scale_speed * Time.deltaTime;
        }

        disappear_time -= Time.deltaTime;
        if (disappear_time < 0) 
        {
            float disappear_speed = 3f;
            text_color.a -= disappear_speed * Time.deltaTime;
            text_mesh.color = text_color;

            if (text_mesh.color.a < 0) 
            {
                Destroy(gameObject);
            }
        }
    }
}
