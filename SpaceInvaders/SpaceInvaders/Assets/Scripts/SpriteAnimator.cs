using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    // Array med sprites som används för animationen
    public Sprite[] animationSprites = new Sprite[2];
    public float animationTime;

    SpriteRenderer spriteRenderer;
    int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Ställer in den första sprite från animationSprites
        spriteRenderer.sprite = animationSprites[0];
    }
    private void Start()
    {
        // Startar en metod som kallas "animateSprite" upprepade gånger med ett intervall av animationTime
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }
    private void animateSprite()
    {
        animationFrame++;

        // Om vi nått slutet av arrayen, återgår vi till första sprite
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        // Uppdaterar SpriteRenderer:s sprite till nästa i animationen
        spriteRenderer.sprite = animationSprites[animationFrame];
    }
}

