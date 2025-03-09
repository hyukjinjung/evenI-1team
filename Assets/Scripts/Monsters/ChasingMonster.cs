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
            MoveTowardsPlayer();
        }

    }



    private void MoveTowardsPlayer()
    {
        transform.position += new Vector3(0, movespeed, 0);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
