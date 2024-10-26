using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    // Array with sprites used for the animation
    public Sprite[] animationSprites = new Sprite[2];
    public float animationTime;

    SpriteRenderer spriteRenderer;
    int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Sets the first sprite used in the animation
        spriteRenderer.sprite = animationSprites[0];
    }
    private void Start()
    {
        // Starts animateSprite repeteadly with an intervall
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }
    private void animateSprite()
    {
        animationFrame++;

        // If reach the end of the array, go back to the first sprite
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        // Updates the spriteRenderer to the next sprite in the animation
        spriteRenderer.sprite = animationSprites[animationFrame];
    }
}

