using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform chasingMonster;

    public float transitionDuration = 5f;
    public float waitBeforetransition = 1.5f;

    private bool transitioning = false;


    void Start()
    {
        if (player == null || chasingMonster == null)
            return;
    }

    IEnumerator StartCameraSequece()
    {
        transitioning = true;

        transform.position = new Vector3(chasingMonster.position.x, chasingMonster.position.y,
            chasingMonster.position.z);

        yield return new WaitForSeconds(waitBeforetransition);

        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, player.position.z);

        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        transitioning = false;
    }
}
