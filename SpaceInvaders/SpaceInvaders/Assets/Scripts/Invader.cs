using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.Build.Content;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[2];
    public float animationTime;
    public GameObject explosion;
    public AudioSource death;
    public int invaderType;


    SpriteRenderer spRend;
    int animationFrame;
    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = animationSprites[0];

    }

    void Start()
    {
        //Anropar AnimateSprite med ett visst tidsintervall
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    //pandlar mellan olika sprited f�r att skapa en animation
    private void AnimateSprite()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spRend.sprite = animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            //Invadern d�r
            GameManager.Instance.OnInvaderKilled(this);
            GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenFlash(0.1f);
            GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(2);
            death.Play();
            Instantiate(explosion, transform.position + new Vector3(Random.Range(-0.25f,0.25f), Random.Range(-0.25f, 0.25f),0), Quaternion.identity);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary")) //n�tt nedre kanten
        {
            GameManager.Instance.OnBoundaryReached();
        }
    }

}
