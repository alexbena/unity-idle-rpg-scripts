using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{

    private TextMeshPro text_mesh;

    public static DamagePopUp Create(Vector3 position, int damage_amount) 
    {
        Transform damage_pop_transform = Instantiate(AssetsManager.i.popup_damage, position, Quaternion.identity);
        DamagePopUp damage_popup_script = damage_pop_transform.GetComponent<DamagePopUp>();
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
