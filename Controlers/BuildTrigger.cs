using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTrigger : MonoBehaviour
{
    bool activated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            if (other.gameObject.tag == "Player")
            {
                AssetsManager.instance.procedural_generator.GetComponent<ProceduralGenerationTerrain>().BuildNext();
                activated = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
