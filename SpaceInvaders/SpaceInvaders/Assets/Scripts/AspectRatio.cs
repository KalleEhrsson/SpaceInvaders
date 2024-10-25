using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    public float targetAspect = 1.0f; //1:1

        //Håller kvar 1:1 aspect ratio oavsett resolution
    void Start()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            camera.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            camera.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
    
}
