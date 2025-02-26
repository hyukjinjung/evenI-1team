using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{

    private bool isAttacking = false; // 공격 중인지 체크
    private bool isTransformed = false; // 변신 상태 여부

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;
    private PlayerTransformationController playerTransformationController;

    [SerializeField] private int currentFloor = 0;

    private GameManager gameManager; // GamaeManager 인스턴스 추가

    // 변신 상태에서 사용할 특수 능력 (ScriptableObject)
    public SpecialAbilityData specialAbilityData;


    private void Start()
    {
        gameManager = GameManager.Instance; // GameManager 가져오기

        if (gameManager == null)
            return;

        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        playerTransformationController = GetComponent<PlayerTransformationController>();

        // 중복 실행 방지
        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;


    }


    // 외부에서 변신 상태를 변경할 수 있도록
    public void SetTransformedState(bool transformed, SpecialAbilityData ability)
    {
        Debug.Log($"변신 상태 변경 - 변신 여부: {transformed}, 특수 능력: {(ability != null ? "설정됨" : "NULL")}");

        isTransformed = transformed;
        specialAbilityData = ability;
    }


    // 변신 상태인지 확인 후 특수 공격 or 일반 공격을 실행
    void PerformAttack(bool isleft)
    {
        // 공격 중일 때는 추가 공격을 막음
        if (isAttacking)
        {
            Debug.Log("공격 중일 때 추가 공격 불가능");
            return;
        }

        Debug.Log($"변신 여부: {isTransformed}, 특수 능력: {(specialAbilityData != null ? "설정됨" : "NULL")}");

        // 특수 공격
        // 변신 상태라면 특수 공격 실행
        if (isTransformed && specialAbilityData != null)
        {

            // 특수 능력 활성화
            Debug.Log("특수 능력 활성화");
            specialAbilityData.ActivateAbility(transform);

            // 특수 공격에 맞는 애니메이션 처리 가능
        }
        else
        {
            NormalAttack(isleft);
        }



        // 일반 공격
        // NormalFrog 상태의 일반 공격 실행
        void NormalAttack(bool isleft)
        {
            Tile tile = testTileManager.GetTile(currentFloor);
            if (tile == null) return;


            // 타일 정보를 바탕을 왼쪽 공격 여부 결정
            bool attackLeft = tile.TileOnLeft(transform);
            // 일반 공격 애니메이션 실행
            playerAnimationController.SetAttacking(attackLeft);


            isAttacking = true;
            Debug.Log("공격 시작");
            StartCoroutine(ResetAttackFlag());


        }
    }


        // 공격 중 상태를 리셋하는 코루틴
        IEnumerator ResetAttackFlag()
        {
            // 애니메이션 길이를 가져옴
            float attackAnimationLength = playerAnimationController.GetAttackAniamtionLength();

            // 변신 상태의 특수 공격 애니메이션 길이

            yield return new WaitForSeconds(attackAnimationLength);

            isAttacking = false; // 공격 가능 상태로 복구
            Debug.Log("다음 공격 준비");
        }
    }
