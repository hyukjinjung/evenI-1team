using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour
{

    void Start()
    {
        SetResolution();
    }



    public void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        Screen.SetResolution(setWidth, setHeight, true);

        //float targetAspect = 16f / 9f;

        //float windowAspect = (float)Screen.width / (float)Screen.height;

        //if (windowAspect > targetAspect )
        //{
        //    float scaleHeight = targetAspect / windowAspect;
        //    Camera.main.rect = new Rect((1f - scaleHeight) / 2f, 0, scaleHeight, 1);
        //}

        //else 
        //{
        //    float scaleWidth = windowAspect / targetAspect;
        //    Camera.main.rect = new Rect(0, (1f - scaleWidth) / 2f, 1, scaleWidth);
        //}
    }
}
