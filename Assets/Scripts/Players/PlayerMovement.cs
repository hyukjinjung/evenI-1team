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


        Tile tile = testTileManager.GetNextTile(currentFloor);

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
        PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();


     
        if (collision.gameObject.CompareTag("Tile"))
        {
            isJumping = false;
        }



        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log($"���Ϳ� �浹. ���� ����: {playerTransformationController.IsTransformed()} " +
                $"/ �浹 ���� ���� ���� {collisionController.CanIgnoreMonster()}");

            if (collisionController != null && collisionController.CanIgnoreMonster())
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


}