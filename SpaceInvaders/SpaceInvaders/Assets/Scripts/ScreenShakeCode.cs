using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeCode : MonoBehaviour
{
    public float shake = 0f;
    public float flash = 0f;

    // Start is called before the first frame update
    void Start()
    {
        shake = 5f;
        flash = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);

        if (shake > 0f) shake -= Time.deltaTime * 10f;

        shake = Mathf.Clamp(shake, 0, 100);

        if (flash > 0)
        {
            GetComponent<Camera>().backgroundColor = new Color(0.1f, 0.1f, 0.3f);
        }
        else
        {
            GetComponent<Camera>().backgroundColor = new Color(0.1f, 0.1f, 0.1f);
        }

        if (flash > 0f) flash -= Time.deltaTime * 10f;

        flash = Mathf.Clamp(flash, 0, 100);
    }

    public void ScreenShake(float screen_shake)
    {
        shake = screen_shake;
    }

    public void ScreenFlash(float screen_flash)
    {
        flash = screen_flash;
    }
}
