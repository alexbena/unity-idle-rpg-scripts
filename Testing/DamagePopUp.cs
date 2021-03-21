using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{

    private TextMeshPro text_mesh;

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
