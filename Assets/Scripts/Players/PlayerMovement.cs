using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool isAutoMode = false;
    
    private bool isJumping = false;
    private bool isGameOver = false; // 게임 오버 중복 방지
    private float deathHeight = -5f; // 캐릭터가 떨어지면 게임 오버되는 높이

    private Vector2 leftDirection = new Vector2(-1f, 1f); // 좌표는 타일 간격에 따라 변동
    private Vector2 rightDirection = new Vector2(1f, 1f); // 좌표는 타일 간격에 따라 변동

    private Rigidbody2D rb;
    private PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private PlayerCollisionController collisionController;
        
    [SerializeField] TestTileManager testTileManager;
    [SerializeField] private int currentFloor = 0;

    public JumpEffectSpawner jumpEffectSpawner;
    public GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        playerTransformationController = GetComponent<PlayerTransformationController>();
        collisionController = GetComponentInParent<PlayerCollisionController>();
        
        playerInputController.OnJumpEvent -= Jump; // 기존 리스너를 제거한 후 다시 등록(중복 실행 방지)
        playerInputController.OnJumpEvent += Jump;
    }

    void Start()
    {
        gameManager = GameManager.Instance;

    }



    void Update()
    {
        if (isGameOver) return;

        if(isJumping && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -9f);
        }

        playerAnimationController.SetJumping(isJumping);

    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

        // 플레이어가 타일이 없어지는 곳에서 아래로 떨어지면 게임 오버
        if (transform.position.y < -0.3f)
        {
            Debug.Log("플레이어 추락. 게임 오버");
            TriggerGameOver();
        }
    }


    private void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // 게임 오버 애니메이션 실행
        if (playerAnimationController != null)
        {
            playerAnimationController.PlayGameOverAnimation();
        }

        GameManager.Instance.GameOver();
    }


    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        //// 변신 해제 후 타일 정보 다시 업데이트
        //UpdateTileInfo();

        Tile tile = testTileManager.GetNextTile(currentFloor);

        if (tile == null) return;


        bool isLeft = tile.TileOnLeft(transform);
        
        //if (isAutoMode)
        //{
        //    Tile temp = testTileManager.GetNextTile(currentFloor);
        //    jumpLeft =  transform.position.x > temp.transform.position.x;
        //}
        
        // 몬스터가 있는 타일이면, NinjaFrog 상태에서는 무시하고 지나감
        if (tile.HasMonster() && collisionController != null && collisionController.CanIgnoreMonster())
        {

            Debug.Log("NinjaFrog 상태. 몬스터 무시하고 점프");
            PerformJump(jumpLeft); // 점프 강제 실행
            return;

        }

        // 기본 점프 처리
        PerformJump(jumpLeft);

        isJumping = true;
        playerAnimationController.SetJumping(true);

        CheckGameOver(isLeft, jumpLeft);

    }



    void PerformJump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return; // 점프 중이면 추가 점프를 막음
        
        // TODO:: 위치는 맞춰서 수정
        gameManager.AddScore(1);
        
        isJumping = true; // 점프 중

        Vector2 previousPosition = transform.position;  // 이전 위치 저장// 점프 이펙트 생성할 때 사용

        
                                                        
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;

        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f; // 타일 중앙에 착지하도록 조정 
        
        Tile targetTile = testTileManager.GetNextTile(currentFloor);

        // 몬스터가 있는  타일이라도 NinjaFrog 상태라면 점프 가능하게 설정
        if (targetTile != null && targetTile.HasMonster() && collisionController != null)
        {
            if (collisionController.CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog 상태. 몬스터 타일 위로 착지");

                // 착지 위치를 타일 위로 설정
                targetPosition = targetTile.transform.position;
            }
            else
            {
                Debug.Log("NormalFrog 상태. 몬스터 타일을 밟을 수 없음");
                GameManager.Instance.GameOver(); // 몬스터 타일에서 이동 불가. 게임 오버
            }

        }



        // 기본 상태 점프
        transform.position = targetPosition; // 타겟 위치로 즉시 이동


        jumpEffectSpawner.SpawnJumpEffect(previousPosition); // 이전 위치에 점프 이펙트 생성

        currentFloor++; // 층 증가

        isJumping = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //// 충돌 무효화 상태라면 아무 효과도 주지 않음
        //PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();


        // 타일과 충돌할 경우
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            //playerAnimationController.SetJumping(!isJumping);
        }



        // 몬스터와 충돌할 경우 (NormalFrog 상태에서 게임 오버)
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collisionController != null && collisionController.CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog 상태. 몬스터와 충돌 무시");
                return;
            }


            // NormalFrog 상태에서는 충돌 시 게임 오버
            Debug.Log("NormalFrog 상태. 몬스터와 충돌. 게임 오버");
            GameManager.Instance.GameOver();
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("TransformationItem"))
        {
            Debug.Log("변신 아이템 획득");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
    }



    void CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if (isLeft == jumpLeft)
        {
            Debug.Log("정상 이동. 게임 오버 아님");
            return;
        }

        Debug.Log("잘못된 점프 방향. 게임 오버 처리됨");
        GameManager.Instance.GameOver();
    }



    public void UpdateTileInfo()
    {
        // currentFloor가 증가한 후 올바른 타일을 가져와야 함
        // 이전 층 타일 확인
        Tile newTile = testTileManager.GetNextTile(currentFloor);

        if (newTile == null)
        {
            Debug.LogError("타일 정보를 찾을 수 없음. currentFloor: " + (currentFloor - 1));
            return;
        }


        bool isLeft = newTile.TileOnLeft(transform);
        Debug.Log($"타일 정보 갱신. 현재 타일 기준: {(isLeft ? "왼쪽" : " 오른쪽")}");
    }
}