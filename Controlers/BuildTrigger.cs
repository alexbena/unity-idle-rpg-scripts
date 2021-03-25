using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AssetsManager.instance.procedural_generator.GetComponent<ProceduralGenerationTerrain>().BuildNext();
    }
}
