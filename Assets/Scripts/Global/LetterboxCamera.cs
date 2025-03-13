using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterboxCamera : MonoBehaviour
{
    private Camera maincamera;
    private CinemachineBrain cinemachineBrain;


    void Start()
    {
        maincamera = Camera.main;
        cinemachineBrain = maincamera.GetComponent<CinemachineBrain>();
        AdjustCamera();
    }



    private void AdjustCamera()
    {
        float targetAspect = 16f / 9f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }



    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
}
