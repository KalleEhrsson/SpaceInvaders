using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeCode : MonoBehaviour
{
    public float shake = 0f;
    public float flash = 0f;
    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        shake = 5f;
        flash = 5f;
        time = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Position is what makes screen shake with random intervals, decreases over time, clamped at 0 and 100
        transform.position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);

        if (shake > 0f) shake -= Time.deltaTime * 10f;

        shake = Mathf.Clamp(shake, 0, 100);

        // flashes the camera to a slight blue color
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

        // this was supposed to stop time for a few frames (ended up not being used but it should work idk)
        if (time > 0f) time -= Time.deltaTime * 10f;

        time = Mathf.Clamp(time, 0, 100);

        if (time > 0)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ScreenShake(float screen_shake)
    {
        shake = screen_shake;
    }

    public void ScreenFlash(float screen_flash)
    {
        flash = screen_flash;
    }

    public void TimeSlow(float _time)
    {
        time = _time;
    }
}
