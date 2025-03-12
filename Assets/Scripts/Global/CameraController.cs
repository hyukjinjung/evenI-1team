using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform player;
    public Transform chasingMonster;

    public float transitionDuration = 5f;
    public float holdTime = 3f;



    void Start()
    {
        if (virtualCamera == null) return;

        if (player == null || chasingMonster == null)
            return;
       
    }

    public void StartCameraSequnece()
    {
        StartCoroutine(CameraSequence());
    }


    private IEnumerator CameraSequence()
    {
        if (virtualCamera == null || player == null || chasingMonster == null)
            yield break;

        virtualCamera.Follow = chasingMonster;
        yield return new WaitForSeconds(holdTime);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.Follow = player;
    }
}
