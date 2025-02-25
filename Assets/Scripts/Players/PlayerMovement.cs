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
    private bool isGameOver = false; // ���� ���� �ߺ� ����
    private float deathHeight = -5f; // ĳ���Ͱ� �������� ���� �����Ǵ� ����

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
        isJumping = Mathf.Abs(rb.velocity.y) > 0.1f;

        // ���� �� �ϰ� �ӵ� ����
        rb.gravityScale = (rb.velocity.y < 0) ? 3f : 1.0f;

        playerAnimationController.SetJumping(isJumping);

    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

        // �÷��̾ Ÿ���� �������� ������ �Ʒ��� �������� ���� ����
        if (transform.position.y < -0.2f)
        {
            Debug.Log("�÷��̾� �߶�. ���� ����");
            TriggerGameOver();
        }
    }


    private void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // ���� ���� �ִϸ��̼� ����
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

        Tile targetTile = testTileManager.GetTile(currentFloor);

        // ���Ͱ� �ִ�  Ÿ���̶� NinjaFrog ���¶�� ���� �����ϰ� ����
        PlayerCollisionController collisionController = GetComponentInParent<PlayerCollisionController>();


        if (targetTile != null && targetTile.HasMonster() && collisionController != null)
        {
            if (collisionController.CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog ����. ���� Ÿ�� ���� ����");

                // ���� ��ġ�� Ÿ�� ���� ����
                targetPosition = targetTile.transform.position;
            }
            else
            {
                Debug.Log("NormalFrog ����. ���� Ÿ���� ���� �� ����");
                GameManager.Instance.GameOver(); // ���� Ÿ�Ͽ��� �̵� �Ұ�. ���� ����
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


        // ���Ϳ� �浹�� ��� (NormalFrog ���¿��� ���� ����)
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collisionController != null && collisionController.CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog ����. ���Ϳ� �浹 ����");
                return;
            }

            // ���� ���� ���� ���� �ð� ���� �浹 ����
            if (playerTransformationController.IsRecentlyTransformed())
            {
                Debug.Log("���� ���� ���� ���� �浹 ����"); // ���� ���� ���� ���� ���� ���� ����
                return;
            }


            // NormalFrog ���¿����� �浹 �� ���� ����
            Debug.Log("NormalFrog ����. ���Ϳ� �浹. ���� ����");
            GameManager.Instance.GameOver();
            isGameOver = true;
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