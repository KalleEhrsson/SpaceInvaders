using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Projectile
{
    public bool weak = false;

    private void Awake()
    {
        direction = Vector3.up;
    }

    void Update()
    {
        speed = 80f;
        if (weak == true)
        {
            speed = 40f;
            transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }

        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(weak == true) CheckCollision(collision);
    }

    void CheckCollision(Collider2D collision)
    {
        Bunker bunker = collision.gameObject.GetComponent<Bunker>();

            //Om det inte är en bunker vi träffat så ska skottet försvinna.
        if(bunker == null) 
        {
            Destroy(gameObject);
        }
    }
}
