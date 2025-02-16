using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;

    private GameObject currentCharacter;        // ���� ���ŵ� ĳ����
    private GameObject originalCharacterPrefab;       // �⺻ ������ ĳ����

    //public PlayerAnimationController AnimationController;

    //public SpecialAbilityData currentAbility;       // �ν����Ϳ��� ScriptableObject ����






    void Start()
    {
        currentState = new NormalState(this);
        currentCharacter = gameObject;      // ������ �� ���� ������Ʈ ����



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
            // ���� ĳ���� ���� �� ���� ������ ����
            GameObject newCharacter = Instantiate(transformationData.transformationPrefab,
                transform.position, Quaternion.identity);

            PlayerTransformationController newController = newCharacter.GetComponent<PlayerTransformationController>();
            if (newController != null)
            {
                // ���� ĳ���� ����
                newController.originalCharacterPrefab = originalCharacterPrefab;
                newController.currentCharacter = newCharacter;
            }

            Destroy(gameObject);        // ���� ĳ���� ����

        }
    }



    public void ReverToOriginalCharacter()
    {
        if (originalCharacterPrefab != null)
        {
            Instantiate(originalCharacterPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);        // ���ŵ� ĳ���� ����
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
