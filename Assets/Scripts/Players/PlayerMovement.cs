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

        if (transform.position.y < -0.5f)
        {
            Debug.Log("�÷��̾� �߶�. ���� ����");
            GameManager.Instance.GameOver();
        }
    }



    public void Jump(bool jumpLeft)
    {
        if (isGameOver || isJumping) return;

        Tile tile = testTileManager.GetForwardTile(transform.position);

        if (tile == null) return;

        bool isLeft = tile.TileOnLeft(transform);

        //if (isAutoMode)
        //{
        //    Tile temp = testTileManager.GetNextTile(currentFloor);
        //    jumpLeft =  transform.position.x > temp.transform.position.x;
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
        
        gameManager.AddScore(1);
        
        isJumping = true;

        Vector2 previousPosition = transform.position;                                                              
        Vector2 jumpDirection = jumpLeft ? leftDirection : rightDirection;
        Vector2 targetPosition = (Vector2)transform.position + jumpDirection;
        targetPosition.y += 0.5f;
        
        Tile targetTile = testTileManager.GetNextTile(currentFloor);
  

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
        jumpEffectSpawner.SpawnJumpEffect(previousPosition);
        currentFloor++;

        isJumping = false;

        playerAnimationController.SetJumping(false);
        playerAnimationController.SetJumpWait();
    }



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
}