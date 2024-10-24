using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[3];
    public float animationTime;

    SpriteRenderer spriteRenderer;
    int animationFrame;

    private void Awake()
    {
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
}

