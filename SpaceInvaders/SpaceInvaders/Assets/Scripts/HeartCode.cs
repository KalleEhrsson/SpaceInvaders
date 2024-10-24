using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCode : MonoBehaviour
{
    public float timer = 0f;
    public float beattimer = 0f;
    public float failtimer = 0f;
    public float bpm = 130f;
    public float scale = 1f;
    public bool input = false;
    public SpriteRenderer flash;
    public SpriteRenderer fail;
    public GameObject[] line = new GameObject[18];
    public int current_line = 0;
    public bool beat = false;
    float line_timer = 0;
    float[] line_y = new float[18];
    public AudioSource heartbeat;
    public AudioSource hurt;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(beat == true)
        {
            if (line_timer <= 0f)
            {
                line_y[current_line] = 2.5f;
                line_y[current_line+9] = 2.5f;
                current_line += 1;
                line_timer = 5f;
            }

            if(current_line >= 9)
            {
                current_line = 0;
                beat = false;
            }
        }

        for (int _i = 0; _i < 9; _i++)
        {
            line_y[_i] += (1 - line_y[_i]) * 10f * Time.deltaTime;
            line_y[_i + 9] += (1 - line_y[_i + 9]) * 10f * Time.deltaTime;
            line[_i].transform.localScale = new Vector3(20f, line_y[_i], 1f);
            line[_i + 9].transform.localScale = new Vector3(20f, line_y[_i + 9], 1f);
        }

        if (timer > 0f) timer -= 1f;
        timer = Mathf.Clamp(timer, 0, 120f);

        if (beattimer > 0f)
        {
            beattimer -= 1f;
            flash.enabled = true;
        }
        else
        {
            flash.enabled = false;
        }

        beattimer = Mathf.Clamp(beattimer, 0, 120f);

        if (failtimer > 0f)
        {
            failtimer -= 1f;
            fail.enabled = true;
        }
        else
        {
            fail.enabled = false;
        }

        beattimer = Mathf.Clamp(beattimer, 0, 120f);

        if (scale > 1) scale -= 0.025f;
        scale = Mathf.Clamp(scale, 1f, 2f);

        if (line_timer > 0) line_timer -= 1f;
        line_timer = Mathf.Clamp(line_timer, 0f, 1000f);

        if (timer <= 0f)
        {
            input = false;

            heartbeat.Play();

            timer = 60/bpm*60;
            beattimer = 15f;
        }

        transform.localScale = new Vector3(scale, scale, 1);
    }
}
