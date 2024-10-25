using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GregoryCode : MonoBehaviour
{
    public GameObject rocket;
    public float add_x = 0f;
    public bool follow = false;
    public bool arm = false;
    public float scale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // This is all old code for the old player, it was made out of seperate objects a limbs, instead of a singular sprite
        if (arm == false)
        {
            if (follow == false)
            {
                transform.position = new Vector3(add_x + transform.position.x + ((rocket.transform.position.x) - transform.position.x) * 10f * Time.deltaTime, transform.position.y, 0f);
            }
            else
            {
                transform.position = new Vector3(rocket.transform.position.x + add_x, transform.position.y, 0f);
            }
        }
        else
        {
            Vector2 Distance = rocket.transform.position - transform.position;
            float RadiansRotation = Mathf.Atan2(Distance.y, Distance.x);
            float DegreesRotation = RadiansRotation * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,DegreesRotation);
            transform.localScale = new Vector3(scale, 1, 1);
        }
    }
}
