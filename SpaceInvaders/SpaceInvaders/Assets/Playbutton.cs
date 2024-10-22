using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playbutton : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[2];
    public float animationTime;
    int animationFrame;
    SpriteRenderer spRend;


    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = animationSprites[0];
    }
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spRend.sprite = animationSprites[animationFrame];
    }
}
