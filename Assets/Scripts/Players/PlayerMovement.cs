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

        if (playerInputController != null )
        {
            playerInputController.AssignPlayerMovement(); // PlayerMovement �ٽ� ���
        }

    }



    void Update()
    {
        if (isGameOver) return;

        // ���� ���� ��, �̵� ���� ȿ��
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }

        Debug.Log("���� ��" + currentFloor);

        // ���� �� �ϰ� �ӵ� ����
        if (rb.velocity.y < 0 )
        {
            rb.gravityScale = 3f;          
        }
        else
        {
            rb.gravityScale = 1.0f;
        }

        playerAnimationController.SetJumping(isJumping);
        
    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        //Debug.Log($"Jump �Լ� ���� jumpLeft = {jumpLeft}"); // ���� ������ Ȯ��

        Tile tile = testTileManager.GetTile(currentFloor);    

        if (tile == null) return;


        bool isLeft = tile.TileOnLeft(transform);

        PerformJump(jumpLeft);

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


        rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);
        transform.position = targetPosition; // Ÿ�� ��ġ�� ��� �̵�


        jumpEffectSpawner.SpawnJumpEffect(previousPosition); // ���� ��ġ�� ���� ����Ʈ ����

        currentFloor++; // �� ����

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ÿ�ϰ� �浹�� ���
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(!isJumping);
        }


        // ���Ϳ� �浹�� ���
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (playerTransformationController.IsTransformed())
            {
                // ���� ������ ���� ���� ���� �ִϸ��̼� ����
                Debug.Log("���Ϳ� �浹 - ���� ����");
                GetComponent<PlayerTransformationController>().StartRevertProcess();
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