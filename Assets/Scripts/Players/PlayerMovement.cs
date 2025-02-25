using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private bool isJumping = false;
    private bool isGameOver = false; // 게임 오버 중복 방지
    private float deathHeight = -5f; // 캐릭터가 떨어지면 게임 오버되는 높이

    private Vector2 leftDirection = new Vector2(-1f, 1f); // 좌표는 타일 간격에 따라 변동
    private Vector2 rightDirection = new Vector2(1f, 1f); // 좌표는 타일 간격에 따라 변동

    private Rigidbody2D rb;
    private PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;

    [SerializeField] TestTileManager testTileManager;
    [SerializeField] private int currentFloor = 0;

    public JumpEffectSpawner jumpEffectSpawner;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        playerTransformationController = GetComponent<PlayerTransformationController>();

        playerInputController.OnJumpEvent -= Jump; // 기존 리스너를 제거한 후 다시 등록(중복 실행 방지)
        playerInputController.OnJumpEvent += Jump;

        if (playerInputController != null)
        {
            playerInputController.AssignPlayerMovement(); // PlayerMovement 다시 등록
        }

    }



    void Update()
    {
        if (isGameOver) return;

        if (isJumping) return;
        

        Debug.Log("현재 층" + currentFloor);


        // y축 속도를 기반으로 점프 상태를 감지
        isJumping = Mathf.Abs(rb.velocity.y) > 0.1f;

        // 점프 후 하강 속도 증가
        rb.gravityScale = (rb.velocity.y < 0) ? 3f : 1.0f;

        playerAnimationController.SetJumping(isJumping);

    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

        // 플레이어가 타일이 없어지는 곳에서 아래로 떨어지면 게임 오버
        if (transform.position.y < -0.2f)
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

        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null) return;


        bool isLeft = tile.TileOnLeft(transform);

        PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();


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


        isJumping = true; // 점프 중

        Vector2 previousPosition = transform.position;  // 이전 위치 저장
                                                        // 점프 이펙트 생성할 때 사용

        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;

        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f; // 타일 중앙에 착지하도록 조정 

        Tile targetTile = testTileManager.GetTile(currentFloor);

        // 몬스터가 있는  타일이라도 NinjaFrog 상태라면 점프 가능하게 설정
        PlayerCollisionController collisionController = GetComponentInParent<PlayerCollisionController>();


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
        // 충돌 무효화 상태라면 아무 효과도 주지 않음
        PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();


        // 타일과 충돌할 경우
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(!isJumping);
        }


        // 몬스터와 충돌할 경우 (NormalFrog 상태에서 게임 오버)
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collisionController != null && collisionController.CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog 상태. 몬스터와 충돌 무시");
                return;
            }

            // 변신 해제 직후 일정 시간 동안 충돌 방지
            if (playerTransformationController.IsRecentlyTransformed())
            {
                Debug.Log("변신 해제 직후 몬스터 충돌 무시"); // 변신 해제 직후 게임 오버 판정 오류
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