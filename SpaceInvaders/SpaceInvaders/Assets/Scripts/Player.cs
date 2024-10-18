using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
   // float speed = 10f;
    float x_position = 0f;
    public bool left = false;
    public bool right = false;
    public bool shoot = false;
    public bool move = false;
    public float x_scale = 0f;
    public float y_scale = 0f;
    public AudioSource shoot_sound;
    public AudioSource move_sound;

    // Update is called once per frame
    void FixedUpdate()
    {
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
                        GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
                        move_sound.Play();
                        x_scale = 1.5f;
                        y_scale = 0.5f;
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
                        GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(0.5f);
                        move_sound.Play();
                        x_scale = 1.5f;
                        y_scale = 0.5f;
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
                        GameObject.Find("Main Camera").GetComponent<ScreenShakeCode>().ScreenShake(1f);

                        Laser laser_object = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                        laser_object.GetComponent<Laser>().weak = true;
                        if (move == true) laser_object.GetComponent<Laser>().weak = false;
                        move = false;

                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().input = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().beat = true;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().current_line = 0;
                        GameObject.Find("GregoryHeart").GetComponent<HeartCode>().scale = 1.5f;
                        shoot_sound.Play();
                        x_scale = 0.5f;
                        y_scale = 1.5f;

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

        x_scale += (1 - x_scale) * 10f * Time.deltaTime;
        y_scale += (1 - y_scale) * 10f * Time.deltaTime;

        transform.localScale = new Vector3(x_scale, y_scale, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}
