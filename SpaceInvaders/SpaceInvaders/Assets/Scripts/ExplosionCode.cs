using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCode : MonoBehaviour
{
    float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= 10f * Time.deltaTime;

        if (timer <= 0f) Destroy(gameObject);
    }
}
