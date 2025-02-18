using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 1f;

    private Rigidbody2D rb;

    private bool isJumping = false;
    private bool isGameOver = false; // 게임 오버 중복 방지

    private Vector2 leftDirection = new Vector2(-1f, 1f); // 좌표는 타일 간격에 따라 변동
    private Vector2 rightDirection = new Vector2(1f, 1f); // 좌표는 타일 간격에 따라 변동

    public JumpEffectSpawner jumpEffectSpawner;
    private PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;    
    [SerializeField] TestTileManager testTileManager;

    [SerializeField] private int currentFloor = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();

        playerInputController = FindObjectOfType<PlayerInputController>();

        playerInputController.OnJumpEvent -= Jump; // 기존 리스너를 제거한 후 다시 등록(중복 실행 방지)
        playerInputController.OnJumpEvent += Jump;

        if (playerInputController != null )
        {
            playerInputController.AssignPlayerMovement(); // PlayerMovement 다시 등록
        }

    }



    void Update()
    {
        if (isGameOver) return;

        // 점프 중일 때, 이동 감속 효과
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }

        Debug.Log("현재 층" + currentFloor);

        playerAnimationController.SetJumping(isJumping);

        
    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        //Debug.Log($"Jump 함수 실행 jumpLeft = {jumpLeft}"); // 점프 방향을 확인

        Tile tile = testTileManager.GetTile(currentFloor);    

        if (tile == null)
        {
            Debug.Log("타일 null");
            return;
        }

        bool isLeft = tile.TileOnLeft(transform);

        PerformJump(jumpLeft);

        CheckGameOver(isLeft, jumpLeft);

    }     
               
        
        
    void PerformJump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return; // 점프 중이면 추가 점프를 막음

        isJumping = true; // 점프 중

        Vector2 previousPosition = transform.position;  // 이전 위치 저장
                                                        // 점프 이펙트 생성할 때 사용

        //Debug.Log($"PerformJump 함수 실행jumpLeft = {jumpLeft}"); // 점프 방향을 확인


        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;

        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f; // 타일 중앙에 착지하도록 조정 


        rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);
        transform.position = targetPosition; // 타겟 위치로 즉시 이동


        jumpEffectSpawner.SpawnJumpEffect(previousPosition); // 이전 위치에 점프 이펙트 생성

        currentFloor++; // 층 증가

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 타일과 충돌할 경우
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(!isJumping);
        }


        // 몬스터와 충돌할 경우
        if (collision.gameObject.CompareTag("Monster"))
        {
            // 게임 오버 처리
            Debug.Log("Player hit a monster");
            GameManager.Instance.GameOver();
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("TransformationItem"))
        {
            Debug.Log("변신 아이템 획득");


        }
    }



    void CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if ((isLeft && !jumpLeft) || (!isLeft && jumpLeft)) // 플레이어 이동 시 게임 오버 처리
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}