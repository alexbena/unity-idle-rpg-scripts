using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{

    private TextMeshPro text_mesh;
    private float disappear_time;
    private Color text_color;

    public static DamagePopUp Create(Vector3 position, int damage_amount) 
    {
        GameObject damage_pop = Instantiate(AssetsManager.instance.popup_damage, position, Quaternion.identity);
        DamagePopUp damage_popup_script = damage_pop.GetComponent<DamagePopUp>();
        damage_popup_script.Setup(damage_amount);

        return damage_popup_script;
    }

  
    private void Awake()
    {
        text_mesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damage_amount) 
    {
        text_mesh.SetText(damage_amount.ToString());
        text_color = text_mesh.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move_y_speed = 2.0f; 
        transform.position += new Vector3(0, move_y_speed) * Time.deltaTime;

        disappear_time -= Time.deltaTime;
        if (disappear_time < 0) 
        {
            float disappear_speed = 3f;
            text_color.a -= disappear_time * Time.deltaTime;
            text_mesh.color = text_color;
        }
    }
}
