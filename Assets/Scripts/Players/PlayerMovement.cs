using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0f;
    public float jumpForce = 0f;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isGameOver = false; // ���� ���� �ߺ� ����

    
    

    private Vector2 leftDirection = new Vector2(-1f, 0.5f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private Vector2 rightDirection = new Vector2(1f, 0.5f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

    public int lastJumpDirection { get; private set; } // ������ ���� ������ ������ ���� �߰�
                                                       // -1: ����, 1: ������
    public JumpEffectSpawner jumpEffectSpawner;

    private PlayerInputController playerInputController;

    [SerializeField] private PlayerAnimationController playerAnimationController;


    private int currentFloor = 0;
    [SerializeField] TestTileManager testTileManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();

        playerInputController.OnJumpEvent += Jump;

        //animator.ResetTrigger("GameOver"); // ���� ���� �� Ʈ���� �ʱ�ȭ
    }



    void Update()
    {
        if (isGameOver) return;

        // ���� ���� ��, �̵� ���� ȿ��
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }

        playerAnimationController.SetJumping(isJumping);
    }



    void Jump(Vector2 direction)
    {
        if (isGameOver) return;

        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null)
            return;

        bool isLeft = tile.TileOnLeft(transform);




        if (isLeft)
        {
            // �������� ����
            Jumping(-1);
        }
        else
        {
            // ���������� ����
            Jumping(1);
        }
    }     
               
        
        
    void Jumping(int direction)
    {
        if (isGameOver || isJumping) return;

        Vector2 previousPosition = transform.position; // ���� ��ġ ����
        Vector2 jumpDirection = (direction == -1) ? leftDirection : rightDirection;
        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f; // Ÿ�� �߾ӿ� ����

        //isJumping = true;
        rb.velocity = new Vector2(jumpDirection.x * moveSpeed, jumpForce);
        //transform.position = targetPosition; // Ÿ�� ��ġ ����

        jumpEffectSpawner.SpawnJumpEffect(previousPosition); // ���� ��ġ�� ��������Ʈ ����
        
        currentFloor++;
    }





    // ���Ϳ� �浹 �� -> ���� ���� ó��
    // Ÿ�ϰ� �浹 ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ÿ�ϰ� �浹�� ���
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
            playerAnimationController.SetJumping(isJumping);
        }


        // ���Ϳ� �浹�� ���
        if (collision.gameObject.CompareTag("Monster"))
        {
            // ���� ���� ó��
            Debug.Log("Player hit a monster");
            GameManager.instance.GameOver();
            isGameOver = true;
        }
    }
}