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
        if (isGameOver) return;

        if (transform.position.y < -0.3f)
        {
            if (FeverSystem.Instance != null && FeverSystem.Instance.isFeverActive)
            {
                return;
            }

            Debug.Log("�÷��̾� �߶�. ���� ����");
            GameManager.Instance.GameOver();
        }
    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        gameManager.AddScore(1);

        Tile tile = testTileManager.GetForwardTile(transform.position);

        if (tile == null) return;

        bool isLeft = tile.TileOnLeft(transform);

        //if (isAutoMode)
        //{
        //    Tile temp = testTileManager.GetNextTile(currentFloor);
        //    jumpLeft = transform.position.x > temp.transform.position.x;
        //}

        if (tile.HasMonster() && CanIgnoreMonster())
        {

            Debug.Log("NinjaFrog ����. ���� �����ϰ� ����");
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

        GameManager.Instance.AddScore(1);
        isJumping = true;

        Vector2 previousPosition = transform.position;
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;
        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f;

        Tile targetTile = testTileManager.GetNextTile(currentFloor);

        if (targetTile !=  null && targetTile.GetComponent<TogglePlatform>() != null)
        {
            StartCoroutine(GameOverDueToInvisible(targetTile));
        }

        if (targetTile != null && targetTile.HasMonster())
        {
            if (CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog ����. ���� Ÿ�� ���� ����");
                targetPosition = targetTile.transform.position;
            }
            else
            {
                Debug.Log("NormalFrog ����. ���� Ÿ���� ���� �� ����");
                GameManager.Instance.GameOver();
            }

        }

        transform.position = targetPosition;
        isJumping = false;
        //StartCoroutine(JumpSmoothly(previousPosition, targetPosition));

        jumpEffectSpawner.SpawnJumpEffect(previousPosition);
        currentFloor++;

    }


    //private IEnumerator JumpSmoothly(Vector3 start, Vector3 end, float speed = 20f)
    //{
    //    float duration = 0.3f;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        float t = elapsedTime / duration;
    //        transform.position = Vector3.Lerp(start, end, t);
    //        elapsedTime += Time.deltaTime * speed;
    //        yield return null;
    //    }

    //    transform.position = end;
    //    isJumping = false;

    //}




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(false);
            playerAnimationController.SetJumpWait();
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            bool isTransformed = playerTransformationController.IsTransformed();
            bool canIgnoreMonster = CanIgnoreMonster();

            Debug.Log($"���Ϳ� �浹. ���� ����: {playerTransformationController.IsTransformed()} " +
                $"/ �浹 ���� ���� ���� {CanIgnoreMonster()}");

            if (canIgnoreMonster)
            {
                Debug.Log("NinjaFrog ����. ���Ϳ� �浹 ����");
                return;
            }

            Debug.Log("NormalFrog ����. ���Ϳ� �浹. ���� ����");
            GameManager.Instance.GameOver();
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("TransformationItem"))
        {
            Debug.Log("���� ������ ȹ��");
        }

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


    void CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if (FeverSystem.Instance != null && FeverSystem.Instance.isFeverActive)
            return;

        if (isLeft == jumpLeft)
        {
            Debug.Log("���� �̵�. ���� ���� �ƴ�");
            return;
        }

        Debug.Log("�߸��� ���� ����. ���� ���� ó����");
        GameManager.Instance.GameOver();
    }


    private IEnumerator GameOverDueToInvisible(Tile targetTile)
    {
        if (targetTile == null && targetTile.GetComponent<TogglePlatform>() == null)
            yield break;

        float PlayerY = transform.position.y;
        float TileY = targetTile.transform.position.y;

        if (PlayerY < TileY - 0.3f)
        {
            GameManager.Instance.GameOver();
        }
    }


    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;
        Debug.Log($"���Ϳ� �浹 ���� Ȱ��ȭ. ���� �ð� {duration}");

        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("���� �浹 ��Ȱ��ȭ");

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