using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private Transform player;
    private ChasingMonsterAnimationController animationController;
    private UIChasingMonsterGauge monsterGauge;
    
    private float nextMoveTime = 0f;
    private float startTime;

    private bool isAttacking = false;
    public float attackRange = 1f;

    [Header("MoveSpeed Settings")]
    [SerializeField] private float baseMoveSpeed = 3;
    [SerializeField] private float initialSpeedUpTime = 60;
    [SerializeField] private float speedIncreaseTime = 30;
    [SerializeField] private float speedIncreaseAmount = 1;

    [Header("Real-time Speed")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float elapsedTime;


    private void Awake()
    {
        animationController = GetComponentInChildren<ChasingMonsterAnimationController>();

    }

    void Start()
    {
        startTime = Time.time;
        currentMoveSpeed = baseMoveSpeed;
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
        //monsterGauge = gauge;
    }



    void Update()
    {
        if (GameManager.Instance.currentState != GameState.Playing) return;
        if (player == null) return;
        if (isAttacking) return;

        elapsedTime = Time.time - GameManager.Instance.gameStartTime;
        UpdateMoveSpeed(elapsedTime);

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


    private void UpdateMoveSpeed(float elapsedTime)
    {
        if (elapsedTime < initialSpeedUpTime)
        {
            currentMoveSpeed = baseMoveSpeed;
        }
        else
        {
            int extraSpeed = Mathf.FloorToInt((elapsedTime - (initialSpeedUpTime - speedIncreaseTime))
                / speedIncreaseTime);
            currentMoveSpeed = baseMoveSpeed + (extraSpeed * speedIncreaseAmount);
        }

        Debug.Log($"MoveSpeed {currentMoveSpeed}");
    }


    private IEnumerator MoveTowardsPlayer()
    {
        Vector3 startPos = transform.position;
        Vector3 direction = (player.position - startPos).normalized;
        Vector3 targetPos = startPos + direction * currentMoveSpeed;

        float elapsedTime = 0f;
        float duration = 1f;

        //animationController?.PlayMove();

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
