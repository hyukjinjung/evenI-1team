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

    [SerializeField] private CanvasGroup DarkOverlay; // 검은색 오버레이 UI


    private bool isJumping = false;
    private bool isGameOver = false; // 게임 오버 중복 방지
    private float deathHeight = -5f; // 캐릭터가 떨어지면 게임 오버되는 높이

    
    public bool isJumping { get; private set; } = false;
    public bool isGameOver { get; private set; } = false; // 게임 오버 중복 방지


    private readonly Vector2 leftDirection = new Vector2(-1f, 1f); // 좌표는 타일 간격에 따라 변동
    private readonly Vector2 rightDirection = new Vector2(1f, 1f); // 좌표는 타일 간격에 따라 변동

    private Rigidbody2D rb;
    private PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private PlayerAttackController attackController;
        
    [SerializeField] TestTileManager testTileManager;
    [SerializeField] private int currentFloor = 0;

    public int CurrentFloor { get => currentFloor; }

    [SerializeField] private JumpEffectSpawner jumpEffectSpawner;


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



    void Update()
    {
        if (isGameOver) return;

        if (transform.position.y < -0.5f)
        {
            Debug.Log("플레이어 추락. 게임 오버");
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

            Debug.Log("NinjaFrog 상태. 몬스터 무시하고 점프");
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
  

        if (targetTile != null && targetTile.HasMonster())
        {
            if (CanIgnoreMonster())
            {
                Debug.Log("NinjaFrog 상태. 몬스터 타일 위로 착지");
                targetPosition = targetTile.transform.position;
            }
            else
            {
                Debug.Log("NormalFrog 상태. 몬스터 타일을 밟을 수 없음");
                GameManager.Instance.GameOver();
            }

        }

        //transform.position = targetPosition;
        StartCoroutine(JumpSmoothly(previousPosition, targetPosition));

        jumpEffectSpawner.SpawnJumpEffect(previousPosition);
        currentFloor++;

    }


    private IEnumerator JumpSmoothly(Vector3 start, Vector3 end, float speed = 20f)
    {
        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(start, end, t);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        transform.position = end;
        isJumping = false;

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

            Debug.Log($"몬스터와 충돌. 변신 상태: {playerTransformationController.IsTransformed()} " +
                $"/ 충돌 무시 가능 여부 {CanIgnoreMonster()}");

            if (canIgnoreMonster)
            {
                Debug.Log("NinjaFrog 상태. 몬스터와 충돌 무시");
                return;
            }

            Debug.Log("NormalFrog 상태. 몬스터와 충돌. 게임 오버");
            GameManager.Instance.GameOver();
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("TransformationItem"))
        {
            Debug.Log("변신 아이템 획득");
        }

        // ? HideNext 아이템 충돌 감지
        if (collision.gameObject.CompareTag("HideNext"))
        {
            Debug.Log("HideNext 아이템 획득! 어두운 효과 적용");
            StartCoroutine(ApplyDarkEffect(5f)); // 5초 동안 효과 유지
            Destroy(collision.gameObject); // 아이템 제거
        }
    }

    private IEnumerator ApplyDarkEffect(float duration)
    {
        if (DarkOverlay == null)
        {
            Debug.LogError("DarkOverlay가 설정되지 않았습니다! Unity에서 연결하세요.");
            yield break;
        }

        // ? 어두운 효과 적용
        DarkOverlay.alpha = 1f;

        yield return new WaitForSeconds(duration);

        // ? 효과 해제
        DarkOverlay.alpha = 0f;
    }


    void CheckGameOver(bool isLeft, bool jumpLeft)
    {
        if (isLeft == jumpLeft)
        {
            Debug.Log("정상 이동. 게임 오버 아님");
            return;
        }

        Debug.Log("잘못된 점프 방향. 게임 오버 처리됨");
        GameManager.Instance.GameOver();
    }


    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;
        Debug.Log($"몬스터와 충돌 무시 활성화. 지속 시간 {duration}");

        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("몬스터 충돌 비활성화");

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