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
    //[SerializeField] private bool isAutoMode = false;

    

    private float deathHeight = -5f; // ĳ���Ͱ� �������� ���� �����Ǵ� ����

    public bool isJumping { get; private set; } = false;
    public bool isGameOver { get; private set; } = false; // ���� ���� �ߺ� ����

    private readonly Vector2 leftDirection = new Vector2(-1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private readonly Vector2 rightDirection = new Vector2(1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

    private Rigidbody2D rb;
    private PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;
    //private PlayerTransformationController playerTransformationController;
    private PlayerAttackController attackController;

    [SerializeField] TestTileManager testTileManager;
    [SerializeField] private int currentFloor = 0;

    public int CurrentFloor { get => currentFloor; }

    [SerializeField] private JumpEffectSpawner jumpEffectSpawner;
    private GameManager gameManager;
    private FeverSystem feverSystem;

    private bool canIgnoreMonster = false;

    private Vector3 lastJumpPosition;
    private bool isRecoveringFromFall = false;
    private Tile lastStandTile;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        //playerTransformationController = GetComponent<PlayerTransformationController>();
        attackController = GetComponentInParent<PlayerAttackController>();


        playerInputController.OnJumpEvent -= Jump;
        playerInputController.OnJumpEvent += Jump;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        feverSystem = GameManager.Instance.feverSystem;
    }



    void Update()
    {
        if (isGameOver || isRecoveringFromFall) return;

        if (transform.position.y < -0.3f)
        {
            if (feverSystem != null && feverSystem.isFeverActive)
            {
                StartCoroutine(RecoverFromFall());
                return;
            }

            Debug.Log("�÷��̾� �߶�. ���� ����");
            gameManager.GameOver();
        }
    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        isJumping = true;
        
        gameManager.AddScore(1);

        // Tile tile = testTileManager.GetForwardTile(transform.position);
        // if (tile == null) return;
        // bool isLeft = tile.TileOnLeft(transform);

        lastJumpPosition = transform.position;

        Tile targetTile = testTileManager.GetNextTile(currentFloor);
        bool isLeft = targetTile.TileOnLeft(transform);
        
        if (CheckGameOver(isLeft, jumpLeft))
        {
            gameManager.GameOver();
            return;
        }
        
        PerformJump(jumpLeft, targetTile);
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



    private bool HandleMonsterOnTile(Tile targetTile, ref Vector2 targetPosition)
    {
        if (targetTile == null) return false;
        
        if (feverSystem != null && feverSystem.isFeverActive)
            return true;

        if (gameManager.PlayerTransformationController != null && gameManager.PlayerTransformationController.IsInvinsible())
            return true;

        if (!targetTile.HasMonster()) return true;


        if (CanIgnoreMonster())
            return true;

        gameManager.GameOver();
        isGameOver = true;

        return false;
    }

    private IEnumerator RecoverFromFall()
    {
        if (isRecoveringFromFall) yield break;
        isRecoveringFromFall = true;

        float fallTime = 0.4f;
        float elapsedTime = 0f;

        while (elapsedTime < fallTime)
        {
            transform.position += Vector3.down * Time.deltaTime * 2f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = lastJumpPosition;

        gameManager.PlayerAnimationController.SetFeverMode(true);

        isRecoveringFromFall = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(false);
            playerAnimationController.SetJumpWait();
        }

        if (feverSystem != null && feverSystem.isFeverActive)
            return;



    }



    bool CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if (feverSystem != null && feverSystem.isFeverActive)
            return false;

        if (isLeft == jumpLeft)
            return false;

        return true;
    }


    private IEnumerator GameOverDueToInvisible(Tile targetTile)
    {
        if (targetTile == null) yield break;

        if (feverSystem != null && feverSystem.isFeverActive)
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


    public void UpdateCurrentFloor()
    {
        int floor = testTileManager.GetFloorByPosition(transform.position);
        if (floor != -1)
        {
            currentFloor = floor;
        }
    }
}