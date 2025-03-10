using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private Transform player;
    public float movespeed = 5;
    private float nextMoveTime = 0f;



    void Start()
    {
        
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }



    void Update()
    {
        if (player == null) return;

        if (Time.time >= nextMoveTime)
        {
            nextMoveTime = Time.time + 1f;
            StartCoroutine(MoveTowardsPlayer());
        }

    }



    private IEnumerator MoveTowardsPlayer()
    {
        Vector3 startPos = transform.position;
        Vector3 direction = (player.position - startPos).normalized;
        Vector3 targetPos = startPos + direction * movespeed;

        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
