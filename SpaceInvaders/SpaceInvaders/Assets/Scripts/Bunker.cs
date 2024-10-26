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
        //Checks if a missile or an invader hits the bunker
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {

            GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
            
            //If the bunker gets hit number of hits go up and it changes sprite depending on how many hits.
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

    //Resets the bunker 
    public void ResetBunker()
    {
        gameObject.SetActive(true);
    }
}
