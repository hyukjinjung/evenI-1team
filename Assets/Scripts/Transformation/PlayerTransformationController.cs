using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;

    private GameObject currentCharacter;        // 현재 변신된 캐릭터
    private GameObject originalCharacterPrefab;       // 기본 상태의 캐릭터

    //public PlayerAnimationController AnimationController;

    //public SpecialAbilityData currentAbility;       // 인스펙터에서 ScriptableObject 연결






    void Start()
    {
        currentState = new NormalState(this);
        currentCharacter = gameObject;      // 시작할 때 현재 오브젝트 저장



        //AnimationController = GetComponent<PlayerAnimationController>();
    }



    public void ChangeState(ITransformation newState)
    {
        currentState = newState;
        currentState.Activate();
    }

    

    public void StartTransformation(TransformationData transformationData)
    {
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
                newController.originalCharacterPrefab = originalCharacterPrefab;
                newController.currentCharacter = newCharacter;
            }

            Destroy(gameObject);        // 기존 캐릭터 삭제

        }
    }



    public void ReverToOriginalCharacter()
    {
        if (originalCharacterPrefab != null)
        {
            Instantiate(originalCharacterPrefab, transform.position, Quaternion.identity);
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
