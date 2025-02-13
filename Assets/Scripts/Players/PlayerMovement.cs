using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0f;
    public float jumpForce = 0f;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isGameOver = false; // 게임 오버 중복 방지

    
    

    private Vector2 leftDirection = new Vector2(-1f, 0.5f); // 좌표는 타일 간격에 따라 변동
    private Vector2 rightDirection = new Vector2(1f, 0.5f); // 좌표는 타일 간격에 따라 변동

    public int lastJumpDirection { get; private set; } // 마지막 점프 방향을 저장할 변수 추가
                                                       // -1: 왼쪽, 1: 오른쪽
    public JumpEffectSpawner jumpEffectSpawner;

    private PlayerInputController playerInputController;

    [SerializeField] private PlayerAnimationController playerAnimationController;


    private int currentFloor = 0;
    [SerializeField] TestTileManager testTileManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();

        playerInputController.OnJumpEvent += Jump;

        //animator.ResetTrigger("GameOver"); // 게임 시작 시 트리거 초기화
    }



    void Update()
    {
        if (isGameOver) return;

        // 점프 중일 때, 이동 감속 효과
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }

        playerAnimationController.SetJumping(isJumping);
    }



    void Jump(Vector2 direction)
    {
        if (isGameOver) return;

        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null)
            return;

        bool isLeft = tile.TileOnLeft(transform);




        if (isLeft)
        {
            // 왼쪽으로 점프
            Jumping(-1);
        }
        else
        {
            // 오른쪽으로 점프
            Jumping(1);
        }
    }     
               
        
        
    void Jumping(int direction)
    {
        if (isGameOver || isJumping) return;

        Vector2 previousPosition = transform.position; // 이전 위치 저장
        Vector2 jumpDirection = (direction == -1) ? leftDirection : rightDirection;
        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f; // 타일 중앙에 착지

        //isJumping = true;
        rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);
        //transform.position = targetPosition; // 타겟 위치 적용

        jumpEffectSpawner.SpawnJumpEffect(previousPosition); // 이전 위치에 점프이펙트 생성
        
        currentFloor++;
    }





    // 몬스터와 충돌 시 -> 게임 오버 처리
    // 타일과 충돌 시
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 타일과 충돌할 경우
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(isJumping);
        }


        // 몬스터와 충돌할 경우
        if (collision.gameObject.CompareTag("Monster"))
        {
            // 게임 오버 처리
            Debug.Log("Player hit a monster");
            GameManager.instance.GameOver();
            isGameOver = true;
        }
    }
}