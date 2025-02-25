using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 1f;

    private bool isJumping = false;
    private bool isGameOver = false; // ���� ���� �ߺ� ����

    private Vector2 leftDirection = new Vector2(-1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private Vector2 rightDirection = new Vector2(1f, 1f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

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

        playerInputController.OnJumpEvent -= Jump; // ���� �����ʸ� ������ �� �ٽ� ���(�ߺ� ���� ����)
        playerInputController.OnJumpEvent += Jump;

        if (playerInputController != null)
        {
            playerInputController.AssignPlayerMovement(); // PlayerMovement �ٽ� ���
        }

    }



    void Update()
    {
        if (isGameOver) return;

        if (isJumping) return;

        Debug.Log("���� ��" + currentFloor);

        // y�� �ӵ��� ������� ���� ���¸� ����
        if (Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        // ���� �� �ϰ� �ӵ� ����
        rb.gravityScale = (rb.velocity.y < 0) ? 3f : 1.0f;

        playerAnimationController.SetJumping(isJumping);

    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null) return;


        bool isLeft = tile.TileOnLeft(transform);

        PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();


        // ���Ͱ� �ִ� Ÿ���̸�, NinjaFrog ���¿����� �����ϰ� ������
        if (tile.HasMonster() && collisionController != null && collisionController.CanIgnoreMonster())
        {

            Debug.Log("NinjaFrog ����. ���� �����ϰ� ����");
            PerformJump(jumpLeft); // ���� ���� ����
            return;

        }

        // �⺻ ���� ó��
        PerformJump(jumpLeft);

        isJumping = true;
        playerAnimationController.SetJumping(true);

        CheckGameOver(isLeft, jumpLeft);

    }



    void PerformJump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return; // ���� ���̸� �߰� ������ ����


        isJumping = true; // ���� ��

        Vector2 previousPosition = transform.position;  // ���� ��ġ ����
                                                        // ���� ����Ʈ ������ �� ���

        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;

        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f; // Ÿ�� �߾ӿ� �����ϵ��� ���� 

        Tile targetTile = testTileManager.GetTile(currentFloor + 1);

        // ���Ͱ� �ִ�  Ÿ���̶� NinjaFrog ���¶�� ���� �����ϰ� ����
        PlayerCollisionController collisionController = GetComponentInParent<PlayerCollisionController>();


        if (targetTile != null && targetTile.HasMonster() && collisionController == null && collisionController.CanIgnoreMonster())
        {
            Debug.Log("NinjaFrog ����. ���� Ÿ�� ���� ����");

            // ���� ��ġ�� Ÿ�� ���� ����
            targetPosition = targetTile.transform.position;

            // ���Ϳ� �浹 ����
            Monster targetMonster = targetTile.GetFirstMonster();
            if (targetMonster != null)
            {
                Collider2D monsterCollider = targetMonster.GetComponent<Collider2D>();
                Collider2D playerCollider = targetMonster.GetComponent<Collider2D>();

                if (monsterCollider != null && playerCollider != null)
                {
                    Physics2D.IgnoreCollision(playerCollider, monsterCollider, true);

                }
            }
        }



        // �⺻ ���� ����
        transform.position = targetPosition; // Ÿ�� ��ġ�� ��� �̵�


        jumpEffectSpawner.SpawnJumpEffect(previousPosition); // ���� ��ġ�� ���� ����Ʈ ����

        currentFloor++; // �� ����

        isJumping = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 ��ȿȭ ���¶�� �ƹ� ȿ���� ���� ����
        PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();


        // Ÿ�ϰ� �浹�� ���
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(!isJumping);
        }


        // ���Ϳ� �浹�� ���
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collisionController != null && collisionController.CanIgnoreMonster())
            {
                Debug.Log("���� �浹 ����");
                isJumping = false;

                Collider2D monsterCollider = collision.gameObject.GetComponent<Collider2D>();
                Collider2D playerCollider = GetComponent<Collider2D>();

                if (monsterCollider != null && playerCollider != null)
                {
                    Physics2D.IgnoreCollision(playerCollider, monsterCollider, false);
                }

                return;
            }


            // �⺻ ����
            if (playerTransformationController.IsTransformed())
            {
                // ���� ������ ���� ���� ���� �ִϸ��̼� ����
                Debug.Log("���Ϳ� �浹 - ���� ����");
                playerTransformationController.StartRevertProcess();
            }

            else
            {
                // �⺻ ������ ���� ���� ���� ó��
                Debug.Log("���Ϳ� �浹 - ���� ����");
                GameManager.Instance.GameOver();
                isGameOver = true;
            }

        }

        if (collision.gameObject.CompareTag("TransformationItem"))
        {
            Debug.Log("���� ������ ȹ��");
        }
    }



    void CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if ((isLeft && !jumpLeft) || (!isLeft && jumpLeft)) // �÷��̾� �̵� �� ���� ���� ó��
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}