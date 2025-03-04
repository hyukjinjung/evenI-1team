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


    private bool isJumping = false;
    private bool isGameOver = false; // ���� ���� �ߺ� ����
    private float deathHeight = -5f; // ĳ���Ͱ� �������� ���� �����Ǵ� ����

    private Vector2 leftDirection = new Vector2(-1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private Vector2 rightDirection = new Vector2(1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

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
        
        playerInputController.OnJumpEvent -= Jump; // ���� �����ʸ� ������ �� �ٽ� ���(�ߺ� ���� ����)
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
            Debug.Log("�÷��̾� �߶�. ���� ����");
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
        
        // TODO:: ��ġ�� ���缭 ����
        gameManager.AddScore(1);
        
        isJumping = true; // ���� ��

        Vector2 previousPosition = transform.position;

        
                                                        
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;

        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f;
        
        Tile targetTile = testTileManager.GetNextTile(currentFloor);

     
        if (targetTile != null && targetTile.HasMonster() && collisionController != null)
        {
            if (collisionController.CanIgnoreMonster())
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


    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
    }



    void CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if (isLeft == jumpLeft)
        {
            Debug.Log("���� �̵�. ���� ���� �ƴ�");
            return;
        }

        Debug.Log("�߸��� ���� ����. ���� ���� ó����");
        GameManager.Instance.GameOver();
    }


    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
    }
}