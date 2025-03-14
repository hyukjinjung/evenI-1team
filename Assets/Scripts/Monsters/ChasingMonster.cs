using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private Transform player;
    public float movespeed = 5;
    private float nextMoveTime = 0f;

    private bool isAttacking = false;

    public float attackRange = 1f;

    private ChasingMonsterAnimationController animationController;


    void Start()
    {
        animationController = GetComponent<ChasingMonsterAnimationController>();
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }



    void Update()
    {
        if (!GameManager.Instance.IsGameStarted) return;
        if (player == null) return;
        if (isAttacking) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(AttackPlayer());
            return;
        }

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

        animationController?.PlayMove();

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }


    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        animationController?.PlayAttack();

        yield return new WaitForSeconds(0.8f);

        GameManager.Instance.GameOver();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animationController.PlayAttack();
            //GameManager.Instance.GameOver();
        }
    }
}
