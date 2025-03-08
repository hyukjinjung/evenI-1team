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

    [SerializeField] private CanvasGroup DarkOverlay; // ������ �������� UI

    private float deathHeight = -5f; // ĳ���Ͱ� �������� ���� �����Ǵ� ����

    public bool isJumping { get; private set; } = false;
    public bool isGameOver { get; private set; } = false; // ���� ���� �ߺ� ����

    private readonly Vector2 leftDirection = new Vector2(-1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private readonly Vector2 rightDirection = new Vector2(1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

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

    private bool canIgnoreMonster = false;

    private Vector3 lastJumpPosition;
    private bool isRecoveringFromFall = false;
    private Tile lastStandTile;


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
    }



    void Update()
    {
        if (isGameOver || isRecoveringFromFall) return;

        if (transform.position.y < -0.3f)
        {
            if (FeverSystem.Instance != null && FeverSystem.Instance.isFeverActive)
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

        gameManager.AddScore(1);

        Tile forwardTile = testTileManager.GetForwardTile(transform.position);

        if (forwardTile == null)
        {
            gameManager.GameOver();
            return;
        }


        //bool isLeft = tile.TileOnLeft(transform);

        //if (isAutoMode)
        //{
        //    Tile temp = testTileManager.GetNextTile(currentFloor);
        //    jumpLeft = transform.position.x > temp.transform.position.x;
        //}

        lastJumpPosition = transform.position;

        PerformJump(jumpLeft);
        isJumping = true;
        playerAnimationController.SetJumping(true);



        CheckGameOver(forwardTile, jumpLeft);

    }


    void PerformJump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        isJumping = true;

        Vector2 previousPosition = transform.position;
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;
        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f;

        Tile targetTile = testTileManager.GetNextTile(currentFloor);

        if (targetTile != null)
        {
            FeverSystem.Instance.AddFeverScore(FeverScoreType.Movement);
        }

        TogglePlatform invisibleTile = targetTile != null ? targetTile.GetComponent<TogglePlatform>() : null;

        if (invisibleTile != null && !invisibleTile.IsVisible())
        {
            StartCoroutine(GameOverDueToInvisible(targetTile));
        }


        if (!HandleMonsterOnTile(targetTile, ref targetPosition))
            return;


        transform.position = targetPosition;
        isJumping = false;

        jumpEffectSpawner.SpawnJumpEffect(previousPosition);
        currentFloor++;

    }



    private bool HandleMonsterOnTile(Tile targetTile, ref Vector2 targetPosition)
    {
        if (targetTile == null) return false;

        if (!targetTile.HasMonster()) return true;

        if (FeverSystem.Instance != null && FeverSystem.Instance.isFeverActive)
            return true;

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

        float fallTime = 0.5f;
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


        //if (collision.gameObject.CompareTag("TransformationItem"))
        //{
        //    Debug.Log("���� ������ ȹ��");
        //}

        // ? HideNext ������ �浹 ����
        if (collision.gameObject.CompareTag("HideNext"))
        {
            Debug.Log("HideNext ������ ȹ��! ��ο� ȿ�� ����");
            StartCoroutine(ApplyDarkEffect(5f)); // 5�� ���� ȿ�� ����
            Destroy(collision.gameObject); // ������ ����
        }
    }

    private IEnumerator ApplyDarkEffect(float duration)
    {
        if (DarkOverlay == null)
        {
            Debug.LogError("DarkOverlay�� �������� �ʾҽ��ϴ�! Unity���� �����ϼ���.");
            yield break;
        }

        // ? ��ο� ȿ�� ����
        DarkOverlay.alpha = 1f;

        yield return new WaitForSeconds(duration);

        // ? ȿ�� ����
        DarkOverlay.alpha = 0f;
    }


    void CheckGameOver(Tile forwardTile, bool jumpLeft)
    {
        if (FeverSystem.Instance != null && FeverSystem.Instance.isFeverActive)
            return;

        //if (forwardTile.transform.position.y < transform.position.y)
        //{
        //    gameManager.GameOver();
        //    return;
        //}

        //bool actualTileLeft = forwardTile.transform.position.x < transform.position.x;

        //if (actualTileLeft == jumpLeft)
        //{
        //    return;
        //}

        //Debug.Log($"게임 오버 발생: actualTileLeft({actualTileLeft}) != jumpLeft({jumpLeft})");
        //gameManager.GameOver();
    }


    private IEnumerator GameOverDueToInvisible(Tile targetTile)
    {
        if (targetTile == null) yield break;

        TogglePlatform invisibleTile = targetTile.GetComponent<TogglePlatform>();
        if (invisibleTile == null) yield break;

        //yield return new WaitUntil(() => invisibleTile.IsVisible());

        //if (transform.position.y < targetTile.transform.position.y)
        //{
        //    gameManager.GameOver();
        //}

        while (invisibleTile.IsVisible())
        {
            yield return null;
        }

        float PlayerY = transform.position.y;
        float TileY = targetTile.transform.position.y;

        if (PlayerY < TileY - 0.3f)
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
        if (playerTransformationController.GetCurrentTransformation() ==
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