using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    [Header("Distances")]
    [Range(10f,25f)]public float distance = 25f;
    public float min_distance = 10f;
    public float max_distance = 25f;
    private Vector3 offset = new Vector3(2.0f, 1.0f, 0.0f);
    [Header("Speeds")]
    public float smooth_speed = 5f;
    public float scroll_sensitivity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target) 
        {
            print("No target set to camera");
            return;
        }

        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scroll_sensitivity;
        distance = Mathf.Clamp(distance, min_distance, max_distance);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * distance;

        transform.position = Vector3.Lerp(transform.position, pos, smooth_speed * Time.deltaTime);
    }
}
