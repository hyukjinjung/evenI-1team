using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;

    private GameObject currentCharacter;        // 현재 변신된 캐릭터
    private GameObject originalCharacterPrefab;       // 기본 상태의 캐릭터

    private UIManager uiManager;
    private PlayerTransformationController playerTransformController;
    public PlayerInputController playerInputController;

    private GameManager gameManager;




    //public PlayerAnimationController AnimationController;     // 애니메이션

    //public SpecialAbilityData currentAbility;       // 인스펙터에서 ScriptableObject 연결



    void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
            return;

        // UI 및 입력 시스템 자동 참조
        uiManager = gameManager.uiManager;

        if (uiManager != null)
        {
            playerInputController = uiManager.playerInputController;
        }

        currentState = new NormalState(this);
        currentCharacter = gameObject;      // 시작할 때 현재 오브젝트 저장


        //AnimationController = GetComponent<PlayerAnimationController>();      // 애니메이션
    }



    public void ChangeState(ITransformation newState)
    {
        currentState = newState;
        currentState.Activate();
    }

    

    public void StartTransformation(TransformationData transformationData)
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        Debug.Log("[PlayerTransformation] 변신 시작");

        ITransformation newTransformation = new TransformationState(this, transformationData);
        ChangeState(newTransformation);


        if (transformationData.transformationPrefab != null)
        {
            // 기존 캐릭터 삭제 후 변신 프리팹 생성
            GameObject newCharacter = Instantiate(transformationData.transformationPrefab,
                transform.position, Quaternion.identity);
            PlayerTransformationController newController = newCharacter.GetComponent<PlayerTransformationController>();
            
            
            if (newController != null)
            {
                // 원래 캐릭터 저장
                newController.uiManager = GameManager.Instance.uiManager; // 기존 UIManager 참조 유지
                newController.playerInputController = GameManager.Instance.uiManager.playerInputController; // 기존 입력 시스템 참조 유지
            }

            // 변신 후 새로운 PlayerInputController에서 이벤트 다시 등록
            newController.playerInputController.AssignPlayerMovement();
            GameManager.Instance.uiManager.UpdatePlayerInputController();

            Destroy(gameObject);        // 기존 캐릭터 삭제

        }
    }



    public void RevertToOriginalCharacter()
    {
        if (originalCharacterPrefab != null)
        {
            GameObject originalCharacter = Instantiate(originalCharacterPrefab, transform.position, 
                Quaternion.identity);

            // 원래 캐릭터에 UI 및 입력 시스템 참조 유지
            PlayerTransformationController originalController = originalCharacter.GetComponent<PlayerTransformationController>();
            
            if (originalController != null)
            {
                originalController.uiManager = uiManager;
                originalController.playerTransformController = playerTransformController;
            }
            
            Destroy(gameObject);        // 변신된 캐릭터 삭제

        }
    }


    public void UseSpecialAbility()
    {
        //currentAbility.ActivateAbility(transform);
    }





    public void StartTransformationTimer(ITransformation transformation)
    {
        StartCoroutine(TransformationTimer(transformation));
    }


    private IEnumerator TransformationTimer(ITransformation transformation)
    {
        yield return new WaitForSeconds(5f);
        transformation.Deactivate();
    }
}
