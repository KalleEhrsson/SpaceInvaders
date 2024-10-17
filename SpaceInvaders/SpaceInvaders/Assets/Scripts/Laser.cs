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
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
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
