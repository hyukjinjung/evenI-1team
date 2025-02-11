using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private bool controlsInverted = false;

    public void InvertControls(float duration)
    {
        controlsInverted = true;
        StartCoroutine(ResetControls(duration));
    }

    public void ModifySpeed(float multiplier, float duration)
    {
        moveSpeed *= multiplier;
        StartCoroutine(ResetSpeed(duration));
    }

    private IEnumerator ResetControls(float duration)
    {
        yield return new WaitForSeconds(duration);
        controlsInverted = false;
    }

    private IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeed = 5f;
    }
}
