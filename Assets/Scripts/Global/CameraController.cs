using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }



    public CinemachineVirtualCamera virtualCamera;
    private CinemachineImpulseSource impulseSource;

    public Transform player;
    public Transform chasingMonster;

    public float transitionDuration = 5f;
    public float holdTime = 3f;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

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
        if (virtualCamera == null || chasingMonster == null)
        {
            Debug.Log("CameraSequence 실행 불가");
            yield break;
        }


        virtualCamera.Follow = chasingMonster;
        virtualCamera.LookAt = chasingMonster;

        yield return new WaitForSeconds(holdTime);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
    }



    public void ShakeCameara(float intensity, float duration)
    {
        if (impulseSource == null) return;

        impulseSource.GenerateImpulseWithForce(intensity);
        StartCoroutine(StopShakeAfterTime(duration));

    }


    private IEnumerator StopShakeAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        impulseSource.m_ImpulseDefinition.m_AmplitudeGain = 0;
    }
}
