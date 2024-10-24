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
        Debug.Log("invoke sak");
    }

    private void AnimateSprite()
    {
        Debug.Log("animate sprite sak");
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            Debug.Log("frame sak");
            animationFrame = 0;
        }
        spRend.sprite = animationSprites[animationFrame];
    }
}
