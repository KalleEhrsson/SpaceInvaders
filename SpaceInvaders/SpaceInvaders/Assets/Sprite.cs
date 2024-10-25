using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour // Renamed class
{
    // Array with sprites used for animation
    public Sprite[] animationSprites = new Sprite[2];
    public float animationTime;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set the first sprite from animationSprites
        spriteRenderer.sprite = animationSprites[0];
    }

    private void Start()
    {
        // Starts the animateSprite method repeatedly with an interval of animationTime
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }

    private void animateSprite()
    {
        animationFrame++;

        // If we reach the end of the array, go back to the first sprite
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        // Update SpriteRenderer's sprite to the next one in the animation
        spriteRenderer.sprite = animationSprites[animationFrame];
    }
}