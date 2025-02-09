using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float JumpForce = 3f;
    public Animator animator;
    // public AudioSource jumpSound; ���� ����

    private Rigidbody2D rb;
    private bool isJumping = false;

    public LayerMask monsterLayer; // ���Ϳ� �浹 ������ ���̾�

    public Button leftButton;
    public Button rightButton;

    private Vector2 leftDirection = new Vector2(-0.75f, 0.45f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����
    private Vector2 rightDirection = new Vector2(0.75f, 0.45f); // ��ǥ�� Ÿ�� ���ݿ� ���� ����

    private TileChecker tileChecker; // Ÿ�� ���� ��ũ��Ʈ

    private int lastJumpDirection = 0; // ������ ���� ������ ������ ���� �߰�
                                       // -1: ����, 1: ������

    private bool isGameOver = false; // ���� ���� �ߺ� ����

    public GameObject jumpEffectPrefab; // ���� ����Ʈ ������


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tileChecker = GetComponent<TileChecker>(); // TileChecker ����

        animator.updateMode = AnimatorUpdateMode.UnscaledTime; // Ÿ�� ������ ������ ���� �ʵ���

        // ��ư �̺�Ʈ ���
        leftButton.onClick.AddListener(JumpLeft);
        rightButton.onClick.AddListener(JumpRight);

        animator.ResetTrigger("GameOver"); // ���� ���� �� Ʈ���� �ʱ�ȭ
    }



    void Update()
    {
        // ���� ���� ��, �̵� ���� ȿ��
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }



    void JumpLeft()
    {
        // ���� ���� ����, ����
        if (!isJumping)
        {
            Vector2 previousPosition = transform.position; // ���� ��ġ ����
            Vector2 targetPosition = (Vector2)transform.position + leftDirection;
            targetPosition.y += 0.5f;

            isJumping = true;
            rb.velocity = new Vector2(leftDirection.x * moveSpeed, JumpForce);
            transform.position = targetPosition; // Ÿ�� ��ġ ����

            lastJumpDirection = -1; // ������ ���� ���� = ����

            SpawnJumpEffect(previousPosition); // ���� ��ġ�� ����Ʈ ����

            // ���� �� Ÿ�� üũ (0.1�� �� ����)
            Invoke(nameof(CheckTileAfterJump), 0.1f);
        }
    }

    void JumpRight()
    {
        if (!isJumping)
        {
            Vector2 previousPosition = transform.position; // ���� ��ġ ����
            Vector2 targetPosition = (Vector2)transform.position + rightDirection;
            targetPosition.y += 0.5f;

            isJumping = true;
            rb.velocity = new Vector2(rightDirection.x * moveSpeed, JumpForce);
            transform.position = targetPosition; // Ÿ�� ��ġ ����

            lastJumpDirection = 1; // ������ ���� ���� = ������

            SpawnJumpEffect(previousPosition); // ���� ��ġ�� ����Ʈ ����

            // ���� �� Ÿ�� üũ (0.1�� �� ����)
            Invoke(nameof(CheckTileAfterJump), 0.1f);
        }
    }



    // ���� �� Ÿ���� �˻��Ͽ� ���� ���� ó��
    void CheckTileAfterJump()
    {
        bool isTileLeft = tileChecker.IsTileInFrontLeft(); // Ÿ���� ���ʿ� �ִ��� Ȯ��
        bool isTileRight = tileChecker.IsTileInFrontRight(); // Ÿ���� �����ʿ� �ִ��� Ȯ��
        bool isTileBelow = tileChecker.IsTileBelow(); // �Ʒ� Ÿ�� ����

        Debug.Log($"Ÿ�� ���� ��� - ����: {isTileLeft}, ������: {isTileRight}, �Ʒ�: {isTileBelow}");


        // �� Ÿ�Ͽ� ������ ���: �밢�� Ÿ���� ��� �Ʒ� Ÿ�ϸ� ������ ���� ����
        if (lastJumpDirection == -1) // ���� �� Ÿ�Ͽ� ������ ���
        {
            if (!isTileBelow) // �Ʒ� Ÿ���� ���ٸ�
            {
                GameOver(); // ���� ���� ó��
                return;
            }
        }
        else if (lastJumpDirection == 1) // ������ �� Ÿ�Ͽ� ������ ���
        {
            if (!isTileBelow) // �Ʒ� Ÿ���� ���ٸ�
            {
                GameOver(); // ���� ���� ó��
                return;
            }
        }


        // �� Ÿ�Ͽ��� ���� �� �밢�� Ÿ���� ��� �Ʒ� Ÿ�ϸ� ������ ���� ����
        if (!isTileBelow)
        {
            // �밢�� Ÿ���� üũ���� �ʰ� �Ʒ� Ÿ�ϸ� üũ
            if (lastJumpDirection == -1 && !isTileLeft) // ���� ������ ���� �� ���� Ÿ���� ���ٸ� ���� ����
            {
                GameOver();
                return;
            }
            else if (lastJumpDirection == 1 && !isTileRight) // ������ ������ ���� �� ������ Ÿ���� ���ٸ� ���� ����
            {
                GameOver();
                return;
            }
        }

        // ���� ���� �� isJumping ����
        isJumping = false;
        animator.SetBool("IsJumping", false);

    }



    void GameOver()
    {
        if (isGameOver) return; // �̹� ���� ������ ��� ���� ����

        isGameOver = true; // ���� ���� ���� ����
        Debug.Log("���� ����");
        GameManager.instance.GameOver();
        animator.SetTrigger("GameOver");
        animator.Play("Die");
    }



    // ���Ϳ� �浹 �� ���� ���� ó��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ���̾�� �浹�� ���
        if (((1 << collision.gameObject.layer) & monsterLayer) != 0)
        {
            Debug.Log("���� �浹 ����! ���� ����");
            // �ִϸ��̼� ���� Ȯ��
            Debug.Log("Current Animator State: " + animator.GetNextAnimatorStateInfo(0).IsName("Die"));


            // ���� ���� ó��
            GameManager.instance.GameOver();
            animator.SetTrigger("GameOver");

            animator.Play("Die");
        }
        else if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("�÷��̾ ������ Ÿ��: " + collision.gameObject.name);
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void SpawnJumpEffect(Vector2 position)
    {
        GameObject effect = Instantiate(jumpEffectPrefab, position, Quaternion.identity);
        Animator effectAnimator = effect.GetComponent<Animator>();
        effectAnimator.SetTrigger("jumpEffectPrefab");

        // ����Ʈ�� �ڵ����� ��������� ���� (���� ����)
        Destroy(effect, effectAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}