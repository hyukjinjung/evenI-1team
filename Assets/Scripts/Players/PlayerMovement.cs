using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private bool isAutoMode = false;

    // private float deathHeight = -5f; // 사용되지 않는 필드 제거 또는 주석 처리

    public bool isJumping { get; private set; } = false;
    public bool isGrounded { get; private set; } = false ;
    public bool isGameOver { get; private set; } = false; // 게임 종료 플래그

    private readonly Vector2 leftDirection = new Vector2(-1f, 1f); // 좌측 방향 벡터
    private readonly Vector2 rightDirection = new Vector2(1f, 1f); // 우측 방향 벡터

    private Rigidbody2D rb;
    private PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private PlayerAttackController attackController;

    [SerializeField] TestTileManager testTileManager;
    [SerializeField] private int currentFloor = 0;

    public int CurrentFloor { get => currentFloor; }

    [SerializeField] private JumpEffectSpawner jumpEffectSpawner;
    private GameManager gameManager;
    private FeverSystem feverSystem;

    private bool canIgnoreMonster = false;
    //private Vector3 lastJumpPosition;
    private bool isRecoveringFromFall = false;
    private Tile lastStandTile;
    public int feverFallCount = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        playerTransformationController = GetComponent<PlayerTransformationController>();
        attackController = GetComponentInParent<PlayerAttackController>();


        playerInputController.OnJumpEvent -= Jump;
        playerInputController.OnJumpEvent += Jump;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        feverSystem = FeverSystem.Instance;
    }



    void Update()
    {
        if (isGameOver || isRecoveringFromFall) return;

        if (transform.position.y < -0.3f)
        {
            gameManager.GameOver();
        }
    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        isJumping = true;
        isGrounded = false;

        gameManager.AddScore(1);

        // Tile tile = testTileManager.GetForwardTile(transform.position);
        // if (tile == null) return;
        // bool isLeft = tile.TileOnLeft(transform);

        Tile targetTile = testTileManager.GetNextTile(currentFloor);

        bool isLeft = targetTile.TileOnLeft(transform);
        bool isWrongDirection = IsWrongJumpDirection(isLeft, jumpLeft);

        PerformJump(jumpLeft, targetTile);

        if (isWrongDirection && feverSystem != null && feverSystem.IsFeverActive)
        {
            StartCoroutine(MoveToNextTileAfterJump(targetTile));
            return;
        }

        if (isWrongDirection)
        {
            gameManager.GameOver();
            return;
        }
    }


    void PerformJump(bool jumpLeft, Tile targetTile)
    {
        Vector2 previousPosition = transform.position;
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;
        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f;

        feverSystem.AddFeverScore(FeverScoreType.Movement);

        TogglePlatform invisibleTile = targetTile != null ? targetTile.GetComponent<TogglePlatform>() : null;

        if (invisibleTile != null && !invisibleTile.IsVisible())
        {
            StartCoroutine(GameOverDueToInvisible(targetTile));
        }


        if (!HandleMonsterOnTile(targetTile, ref targetPosition))
            return;


        transform.position = targetPosition;

        jumpEffectSpawner.SpawnJumpEffect(previousPosition);
        currentFloor++;

        playerAnimationController.SetJumping(true);
    }


    private IEnumerator MoveToNextTileAfterJump(Tile nextTile)
    {
        yield return new WaitForSeconds(0.2f);

        transform.position = nextTile.transform.position;

        feverSystem.EndFever();
    }


    private bool HandleMonsterOnTile(Tile targetTile, ref Vector2 targetPosition)
    {
        if (targetTile == null) return false;

        if (feverSystem != null && feverSystem.IsFeverActive)
            return true;

        if (playerTransformationController != null && playerTransformationController.IsInvinsible())
            return true;

        if (!targetTile.HasMonster()) return true;

        ShieldState shieldState = GetComponent<ShieldState>();
        if (shieldState != null && shieldState.HasShield())
        {
            Debug.Log("✅ 쉴드로 몬스터 충돌 방어!");
            shieldState.UseShield();
            return true;
        }

        if (CanIgnoreMonster())
            return true;

        gameManager.GameOver();
        isGameOver = true;

        return false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            isGrounded = true;
            playerAnimationController.SetJumping(false);
            playerAnimationController.SetJumpWait();
        }

        if (feverSystem != null && feverSystem.IsFeverActive && IsOnObstacleTile())
            return;

        //if (feverSystem.IsFeverActive)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, -10f);
        //}
    }



    bool IsWrongJumpDirection(bool isLeft, bool jumpLeft)
    {
        if (feverSystem != null && feverSystem.IsFeverActive)
        {
            return isLeft != jumpLeft;
        }

        return isLeft != jumpLeft;
    }


    private IEnumerator GameOverDueToInvisible(Tile targetTile)
    {
        if (targetTile == null) yield break;

        if (feverSystem != null && feverSystem.IsFeverActive)
        {
            yield break;
        }

        TogglePlatform invisibleTile = targetTile.GetComponent<TogglePlatform>();
        if (invisibleTile == null) yield break;

        while (invisibleTile.IsVisible())
        {
            yield return null;
        }

        float PlayerY = transform.position.y;
        float TileY = targetTile.transform.position.y;

        if (PlayerY < TileY - 0.2f)
        {
            gameManager.GameOver();
        }
    }


    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;

        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
    }


    public bool CanIgnoreMonster()
    {
        if (gameManager.PlayerTransformationController.GetCurrentTransformation() ==
            TransformationType.NinjaFrog)
        {
            return true;
        }

        return canIgnoreMonster;
    }


    public bool IsOnMonsterTile()
    {
        Tile currentTile = testTileManager.GetTileByPosition(transform.position);
        if (currentTile != null && currentTile.HasMonster())
        {
            return true;
        }

        return false;
    }


    public bool IsOnObstacleTile()
    {
        Tile currentTile = testTileManager.GetTileByPosition(transform.position);
        if (currentTile != null && currentTile.HasObstacle())
        {
            return true;
        }

        return false;
    }


    public void UpdateCurrentFloor()
    {
        int floor = testTileManager.GetFloorByPosition(transform.position);
        if (floor != -1)
        {
            currentFloor = floor;
        }
    }
}