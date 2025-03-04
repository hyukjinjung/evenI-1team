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
    [SerializeField] private CanvasGroup DarkOverlay; // 검은색 오버레이 UI


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
    public int CurrentFloor { get => currentFloor; }

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
            rb.velocity = new Vector2(rb.velocity.x, -12f);
        }

        playerAnimationController.SetJumping(isJumping);

    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

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

        if (playerAnimationController != null)
        {
            playerAnimationController.PlayGameOverAnimation();
        }

        GameManager.Instance.GameOver();
    }


    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;


        //Tile tile = testTileManager.GetNextTile(currentFloor);
        Tile tile = testTileManager.GetForwardTile(transform.position);

        if (tile == null) return;

        bool isLeft = tile.TileOnLeft(transform);
        
        //if (isAutoMode)
        //{
        //    Tile temp = testTileManager.GetNextTile(currentFloor);
        //    jumpLeft =  transform.position.x > temp.transform.position.x;
        //}
        
        if (tile.HasMonster() && collisionController != null && collisionController.CanIgnoreMonster())
        {

            Debug.Log("NinjaFrog 상태. 몬스터 무시하고 점프");
            PerformJump(jumpLeft);
            return;

        }


        PerformJump(jumpLeft);
        isJumping = true;
        playerAnimationController.SetJumping(true);


        CheckGameOver(isLeft, jumpLeft);

    }



    void PerformJump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return; 
        
        // TODO:: 위치는 맞춰서 수정
        gameManager.AddScore(1);
        
        isJumping = true; // 점프 중

        Vector2 previousPosition = transform.position;

        
                                                        
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;

        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f;
        
        Tile targetTile = testTileManager.GetNextTile(currentFloor);

     
        if (targetTile != null && targetTile.HasMonster() && collisionController != null)
        {
            if (collisionController.CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog 상태. 몬스터 타일 위로 착지");

               
                targetPosition = targetTile.transform.position;
            }
            else
            {
                Debug.Log("NormalFrog 상태. 몬스터 타일을 밟을 수 없음");
                GameManager.Instance.GameOver();
            }

        }

        transform.position = targetPosition;


        jumpEffectSpawner.SpawnJumpEffect(previousPosition);

        currentFloor++;

        isJumping = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            bool isTransformed = playerTransformationController.IsTransformed();
            bool canIgnoreMonster = collisionController.CanIgnoreMonster();

            if (canIgnoreMonster)
            {
                Debug.Log("NinjaFrog 상태. 몬스터와 충돌 무시");
                return;
            }

            Debug.Log("NormalFrog 상태. 몬스터와 충돌. 게임 오버");
            GameManager.Instance.GameOver();
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("TransformationItem"))
        {
            Debug.Log("변신 아이템 획득");
        }

        // ? HideNext 아이템 충돌 감지
        if (collision.gameObject.CompareTag("HideNext"))
        {
            Debug.Log("HideNext 아이템 획득! 어두운 효과 적용");
            StartCoroutine(ApplyDarkEffect(5f)); // 5초 동안 효과 유지
            Destroy(collision.gameObject); // 아이템 제거
        }
    }

    private IEnumerator ApplyDarkEffect(float duration)
    {
        if (DarkOverlay == null)
        {
            Debug.LogError("DarkOverlay가 설정되지 않았습니다! Unity에서 연결하세요.");
            yield break;
        }

        // ? 어두운 효과 적용
        DarkOverlay.alpha = 1f;

        yield return new WaitForSeconds(duration);

        // ? 효과 해제
        DarkOverlay.alpha = 0f;
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


    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
    }
}