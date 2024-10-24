using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviour
{

    public Sprite[] pianoSprites;

    int nrOfHits = 0;
    SpriteRenderer spRend;
    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = pianoSprites[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {

            GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
            /*
            //Ändrar färgen beroende på antal träffar.
            nrOfHits++;
            Color oldColor = spRend.color;

            Color newColor = new Color(oldColor.r +(nrOfHits*0.1f), oldColor.g + (nrOfHits * 0.1f), oldColor.b + (nrOfHits * 0.1f));
            
            spRend.color = newColor;
            */
            nrOfHits++;
            if(nrOfHits == 1)
            {
                spRend.sprite = pianoSprites[1];
            }
            if(nrOfHits == 2)
            {
                spRend.sprite = pianoSprites[2];
            }
            if (nrOfHits == 3)
            {
                gameObject.SetActive(false);
            }
            
        }
    }

    public void ResetBunker()
    {
        gameObject.SetActive(true);
    }
}
