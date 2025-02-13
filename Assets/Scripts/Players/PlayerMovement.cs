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

    public Button leftButton;
    public Button rightButton;

    private Vector2 leftDirection = new Vector2(-1f, 0.5f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private Vector2 rightDirection = new Vector2(1f, 0.5f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

    public int lastJumpDirection { get; private set; } // ������ ���� ������ ������ ���� �߰�
                                                       // -1: ����, 1: ������
    public JumpEffectSpawner jumpEffectSpawner;

    [SerializeField] private PlayerAnimationController playerAnimationController;


    private int currentFloor = 0;
    [SerializeField] TestTileManager testTileManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        
        // ��ư �̺�Ʈ ���
        leftButton.onClick.AddListener(() => Jump(-1));
        rightButton.onClick.AddListener(() => Jump(1));

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



    void Jump(int direction)
    {
        if (isGameOver || isJumping) return;

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


    //// Ÿ�� �ݴ� ������ ������ ��� ���� ����
    //void CheckGameOverCondition()
    //{
    //    if (isGameOver) return; // �̹� ���� ���� ���¶�� üũ���� ����.

    //    // ���ʰ� ������ Ÿ�� ã��
    //    GameObject leftTile = FindNextTile(-1);
    //    GameObject rightTile = FindNextTile(1);

    //    // �÷��̾ ������ ������ Ÿ���� �ִ� ����� �ݴ����� Ȯ��
    //    if (lastJumpDirection == -1 && rightTile != null && leftTile == null)
    //    {
    //        // �������� ����������, �����ʿ��� Ÿ���� ���� ��� ���� ����
    //        GameManager.instance.GameOver();
    //        isGameOver = true;
    //    }
    //    else if (lastJumpDirection == 1 &&  leftTile != null && rightTile == null)
    //    {
    //        // ���������� ����������, ���ʿ��� Ÿ���� ���� ��� ���� ����
    //        GameManager.instance.GameOver();
    //        isGameOver = true;
    //    }
    //}



    //GameObject FindNextTile(int direction)
    //{
    //    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); // ���� ���� ���� ���� ���
    //    GameObject targetTile = null;
    //    float closestDistance = Mathf.Infinity;

    //    Vector2 nextPosition = (Vector2)transform.position + (direction == -1 ? leftDirection : rightDirection);

    //    foreach (GameObject tile in tiles)
    //    {
    //        float tileX = tile.transform.position.x;
    //        float tileY = tile.transform.position.y;
    //        float nextX = nextPosition.x;

    //        // Ÿ���� ���� ��ġ���� �Ʒ��� ������ ����
    //        if (tileY < transform.position.y - 0.2f) continue;

    //        if (tileY > transform.position.y + 0.7f) continue;

    //        float distance = Vector2.Distance(new Vector2(tileX, tileY), nextPosition);

    //        if (distance < closestDistance)
    //        {

    //            closestDistance = distance;
    //            targetTile = tile;

    //        }
    //    }

    //    return targetTile;
    //}





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