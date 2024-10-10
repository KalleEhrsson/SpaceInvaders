using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GregoryCode : MonoBehaviour
{
    public GameObject rocket;
    public float add_x = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(add_x + transform.position.x + ((rocket.transform.position.x) - transform.position.x) * 10f * Time.deltaTime, transform.position.y, 0f);
    }
}
