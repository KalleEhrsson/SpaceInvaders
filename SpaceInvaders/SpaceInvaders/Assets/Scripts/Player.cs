using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
    Laser laser;
   // float speed = 10f;
    public Animator anim;
    float x_position = 0f;
    public bool left = false;
    public bool right = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.Play("Idle");

        Vector3 position = transform.position;

        /*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }
        */

        if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beattimer > 0f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input == false && left == false)
                {
                    x_position -= 5f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input = true;
                    left = true;
                }
            }
            else
            {
                left = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input == false && right == false)
                {
                    x_position += 5f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input = true;
                    right = true;
                }
            }
            else
            {
                right = false;
            }
        }

        position = new Vector3(position.x + ((x_position - position.x) * 10f * Time.deltaTime), position.y, position.z);

        Vector3 leftedge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightedge = Camera.main.ViewportToWorldPoint(Vector3.right);

        position.x = Mathf.Clamp(position.x, leftedge.x, rightedge.x);

        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space) && laser == null)
        {
            GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);

            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}
