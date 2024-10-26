using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Invader : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource death;
    public int invaderType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            //Invadern d�r
            GameManager.Instance.OnInvaderKilled(this);

            // Add some screen shake and explosion effects
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
