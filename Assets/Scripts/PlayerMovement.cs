using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float JumpForce = 3f;
    public Animator animator;
    // public AudioSource jumpSound; 점프 사운드

    private Rigidbody2D rb;
    private bool isJumping = false;

    public LayerMask monsterLayer; // 몬스터와 충돌 감지용 레이어

    public Button leftButton;
    public Button rightButton;

    private Vector2 leftDirection = new Vector2(-0.75f, 0.45f); // 좌표는 타일 간격에 따라 변동
    private Vector2 rightDirection = new Vector2(0.75f, 0.45f); // 좌표는 타일 간격에 따라 변동

    private TileChecker tileChecker; // 타일 감지 스크립트

    private int lastJumpDirection = 0; // 마지막 점프 방향을 저장할 변수 추가
                                       // -1: 왼쪽, 1: 오른쪽

    private bool isGameOver = false; // 게임 오버 중복 방지

    public GameObject jumpEffectPrefab; // 점프 이펙트 프리팹


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tileChecker = GetComponent<TileChecker>(); // TileChecker 참조

        animator.updateMode = AnimatorUpdateMode.UnscaledTime; // 타임 스케일 영향을 받지 않도록

        // 버튼 이벤트 등록
        leftButton.onClick.AddListener(JumpLeft);
        rightButton.onClick.AddListener(JumpRight);

        animator.ResetTrigger("GameOver"); // 게임 시작 시 트리거 초기화
    }



    void Update()
    {
        // 점프 중일 때, 이동 감속 효과
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }



    void JumpLeft()
    {
        // 땅에 있을 때만, 점프
        if (!isJumping)
        {
            Vector2 previousPosition = transform.position; // 이전 위치 저장
            Vector2 targetPosition = (Vector2)transform.position + leftDirection;
            targetPosition.y += 0.5f;

            isJumping = true;
            rb.velocity = new Vector2(leftDirection.x * moveSpeed, JumpForce);
            transform.position = targetPosition; // 타겟 위치 적용

            lastJumpDirection = -1; // 마지막 점프 방향 = 왼쪽

            SpawnJumpEffect(previousPosition); // 이전 위치에 이펙트 생성

            // 점프 후 타일 체크 (0.1초 뒤 실행)
            Invoke(nameof(CheckTileAfterJump), 0.1f);
        }
    }

    void JumpRight()
    {
        if (!isJumping)
        {
            Vector2 previousPosition = transform.position; // 이전 위치 저장
            Vector2 targetPosition = (Vector2)transform.position + rightDirection;
            targetPosition.y += 0.5f;

            isJumping = true;
            rb.velocity = new Vector2(rightDirection.x * moveSpeed, JumpForce);
            transform.position = targetPosition; // 타겟 위치 적용

            lastJumpDirection = 1; // 마지막 점프 방향 = 오른쪽

            SpawnJumpEffect(previousPosition); // 이전 위치에 이펙트 생성

            // 점프 후 타일 체크 (0.1초 뒤 실행)
            Invoke(nameof(CheckTileAfterJump), 0.1f);
        }
    }



    // 점프 후 타일을 검사하여 게임 오버 처리
    void CheckTileAfterJump()
    {
        bool isTileLeft = tileChecker.IsTileInFrontLeft(); // 타일이 왼쪽에 있는지 확인
        bool isTileRight = tileChecker.IsTileInFrontRight(); // 타일이 오른쪽에 있는지 확인
        bool isTileBelow = tileChecker.IsTileBelow(); // 아래 타일 감지

        Debug.Log($"타일 감지 결과 - 왼쪽: {isTileLeft}, 오른쪽: {isTileRight}, 아래: {isTileBelow}");


        // 끝 타일에 도달한 경우: 대각선 타일이 없어도 아래 타일만 있으면 착지 가능
        if (lastJumpDirection == -1) // 왼쪽 끝 타일에 도달한 경우
        {
            if (!isTileBelow) // 아래 타일이 없다면
            {
                GameOver(); // 게임 오버 처리
                return;
            }
        }
        else if (lastJumpDirection == 1) // 오른쪽 끝 타일에 도달한 경우
        {
            if (!isTileBelow) // 아래 타일이 없다면
            {
                GameOver(); // 게임 오버 처리
                return;
            }
        }


        // 끝 타일에서 점프 후 대각선 타일이 없어도 아래 타일만 있으면 착지 가능
        if (!isTileBelow)
        {
            // 대각선 타일을 체크하지 않고 아래 타일만 체크
            if (lastJumpDirection == -1 && !isTileLeft) // 왼쪽 끝에서 점프 후 왼쪽 타일이 없다면 게임 오버
            {
                GameOver();
                return;
            }
            else if (lastJumpDirection == 1 && !isTileRight) // 오른쪽 끝에서 점프 후 오른쪽 타일이 없다면 게임 오버
            {
                GameOver();
                return;
            }
        }

        // 착지 성공 시 isJumping 해제
        isJumping = false;
        animator.SetBool("IsJumping", false);

    }



    void GameOver()
    {
        if (isGameOver) return; // 이미 게임 오버된 경우 실행 방지

        isGameOver = true; // 게임 오버 상태 설정
        Debug.Log("게임 오버");
        GameManager.instance.GameOver();
        animator.SetTrigger("GameOver");
        animator.Play("Die");
    }



    // 몬스터와 충돌 시 게임 오버 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 몬스터 레이어와 충돌할 경우
        if (((1 << collision.gameObject.layer) & monsterLayer) != 0)
        {
            Debug.Log("몬스터 충돌 감지! 게임 오버");
            // 애니메이션 상태 확인
            Debug.Log("Current Animator State: " + animator.GetNextAnimatorStateInfo(0).IsName("Die"));


            // 게임 오버 처리
            GameManager.instance.GameOver();
            animator.SetTrigger("GameOver");

            animator.Play("Die");
        }
        else if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("플레이어가 착지한 타일: " + collision.gameObject.name);
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void SpawnJumpEffect(Vector2 position)
    {
        GameObject effect = Instantiate(jumpEffectPrefab, position, Quaternion.identity);
        Animator effectAnimator = effect.GetComponent<Animator>();
        effectAnimator.SetTrigger("jumpEffectPrefab");

        // 이펙트가 자동으로 사라지도록 설정 (선택 사항)
        Destroy(effect, effectAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}