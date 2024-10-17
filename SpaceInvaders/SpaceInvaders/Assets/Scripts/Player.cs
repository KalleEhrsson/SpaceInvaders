using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
   // float speed = 10f;
    public Animator anim;
    float x_position = 0f;
    public bool left = false;
    public bool right = false;
    public bool shoot = false;
    public bool move = false;

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

        if (Input.GetKey(KeyCode.A))
        {
            if (left == false)
            {
                if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beattimer > 0f)
                {
                    if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input == false)
                    {
                        x_position -= 5f;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 1.5f;
                        left = true;
                        move = true;
                    }
                }
                else
                {
                    GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 0.75f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().failtimer = 15f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = false;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().hurt.Play();
                    left = true;
                    move = true;
                }
            }
        }
        else
        {
            left = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (right == false)
            {
                if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beattimer > 0f)
                {
                    if(GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input == false)
                    {
                        x_position += 5f;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 1.5f;
                        right = true;
                        move = true;
                    }
                }
                else
                {
                    GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 0.75f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().failtimer = 15f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = false;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().hurt.Play();
                    right = true;
                    move = true;
                }
            }
        }
        else
        {
            right = false;
        }

        position = new Vector3(position.x + ((x_position - position.x) * 10f * Time.deltaTime), position.y, position.z);

        Vector3 leftedge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightedge = Camera.main.ViewportToWorldPoint(Vector3.right);

        position.x = Mathf.Clamp(position.x, leftedge.x, rightedge.x);

        transform.position = position;

        if (Input.GetKey(KeyCode.Space))
        {
            if (shoot == false)
            {
                if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beattimer > 0f)
                {
                    if (GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input == false)
                    {
                        GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);

                        Laser laser_object = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                        laser_object.GetComponent<Laser>().weak = true;
                        if (move == true) laser_object.GetComponent<Laser>().weak = false;
                        move = false;

                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 1.5f;

                        shoot = true;
                    }
                }
                else
                {
                    GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 0.75f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().failtimer = 15f;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = false;
                    GameObject.Find("GregoryHeart").GetComponent<HeartCode>().hurt.Play();

                    shoot = true;
                }
            }
        }
        else
        {
            shoot = false;
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
