using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Projectile
{
    public Sprite[] animationSprites = new Sprite[3];
    public float animationTime;

    SpriteRenderer spriteRenderer;
    int animationFrame;
    public bool weak = false;

    private void Awake()
    {
        direction = Vector3.up;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = animationSprites[0];
    }
    private void Start()
    {
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }

    private void animateSprite()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spriteRenderer.sprite = animationSprites[animationFrame];
    }

    void Update()
    {
        // If the player moves the lazer gains more speed, size and piercing
        speed = 80f;
        transform.localScale = new Vector3(2f,2f, 1f);
        if (weak == true)
        {
            speed = 40f;
            transform.localScale = new Vector3(0.75f,0.75f, 1f);
        }

        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weak == true)
        {
            CheckCollision(collision);
        }
        else
        {
            if(collision.tag == "Borders")
            {
                CheckCollision(collision);
            }
        }
    }

    void CheckCollision(Collider2D collision)
    {
        Bunker bunker = collision.gameObject.GetComponent<Bunker>();

        //Om det inte �r en bunker vi tr�ffat s� ska skottet f�rsvinna.
        if (bunker == null)
        {
            Destroy(gameObject);
        }
    }
}
