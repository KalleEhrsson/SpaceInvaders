using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Missile : Projectile
{
    public float otherDirection = 0;

    private void Awake()
    {
        direction = Vector3.down;
    }
    void Update()
    {
        float _speed = speed;
        if (otherDirection != 0) _speed = speed * (1f / 1.41f);

        transform.position += _speed * Time.deltaTime * direction;

        if(otherDirection == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 315);
            transform.position += _speed * Time.deltaTime * -Vector3.right;
        }

        if (otherDirection == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
            transform.position += _speed * Time.deltaTime * Vector3.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //s� fort den krockar med n�got s� ska den f�rsvinna.
    }
   
}
