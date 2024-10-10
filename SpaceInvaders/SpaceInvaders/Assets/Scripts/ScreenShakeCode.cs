using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeCode : MonoBehaviour
{
    public float shake = 0f;

    // Start is called before the first frame update
    void Start()
    {
        shake = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);

        if (shake >= 0f) shake -= Time.deltaTime * 10f;

        shake = Mathf.Clamp(shake, 0, 100);
    }

    public void ScreenShake(float screen_shake)
    {
        shake = screen_shake;
    }
}
