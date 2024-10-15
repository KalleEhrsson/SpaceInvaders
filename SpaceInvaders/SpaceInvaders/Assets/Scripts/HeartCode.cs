using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCode : MonoBehaviour
{
    public float timer = 0f;
    public float beattimer = 0f;
    public float bpm = 130f;
    float scale = 1f;
    public bool input = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0f) timer -= 1f;
        timer = Mathf.Clamp(timer, 0, 120f);

        if (beattimer > 0f) beattimer -= 1f;
        beattimer = Mathf.Clamp(beattimer, 0, 120f);

        if (scale > 1) scale -= 0.025f;
        scale = Mathf.Clamp(scale, 1f, 2f);

        if (timer <= 0f)
        {
            scale = 1.5f;

            input = false;

            timer = 60/bpm*60f;
            beattimer = 12f;
        }

        Debug.Log(input);

        transform.localScale = new Vector3(scale, scale, 1);
    }
}
