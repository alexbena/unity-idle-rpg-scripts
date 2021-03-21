using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private Transform popup_dmg;
    // Start is called before the first frame update
    void Start()
    {
        Transform damage_pop_transform = Instantiate(popup_dmg, Vector3.zero, Quaternion.identity);

        DamagePopUp damage_popup_script = damage_pop_transform.GetComponent<DamagePopUp>();

        damage_popup_script.Setup(200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
