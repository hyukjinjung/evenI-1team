using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;

    private GameObject currentCharacter;        // ���� ���ŵ� ĳ����
    private GameObject originalCharacterPrefab;       // �⺻ ������ ĳ����

    private UIManager uiManager;
    private PlayerTransformationController playerTransformController;
    public PlayerInputController playerInputController;

    private GameManager gameManager;




    //public PlayerAnimationController AnimationController;     // �ִϸ��̼�

    //public SpecialAbilityData currentAbility;       // �ν����Ϳ��� ScriptableObject ����



    void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
            return;

        // UI �� �Է� �ý��� �ڵ� ����
        uiManager = gameManager.uiManager;

        if (uiManager != null)
        {
            playerInputController = uiManager.playerInputController;
        }

        currentState = new NormalState(this);
        currentCharacter = gameObject;      // ������ �� ���� ������Ʈ ����


        //AnimationController = GetComponent<PlayerAnimationController>();      // �ִϸ��̼�
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

        Debug.Log("[PlayerTransformation] ���� ����");

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
                newController.uiManager = GameManager.Instance.uiManager; // ���� UIManager ���� ����
                newController.playerInputController = GameManager.Instance.uiManager.playerInputController; // ���� �Է� �ý��� ���� ����
            }

            // ���� �� ���ο� PlayerInputController���� �̺�Ʈ �ٽ� ���
            newController.playerInputController.AssignPlayerMovement();
            GameManager.Instance.uiManager.UpdatePlayerInputController();

            Destroy(gameObject);        // ���� ĳ���� ����

        }
    }



    public void RevertToOriginalCharacter()
    {
        if (originalCharacterPrefab != null)
        {
            GameObject originalCharacter = Instantiate(originalCharacterPrefab, transform.position, 
                Quaternion.identity);

            // ���� ĳ���Ϳ� UI �� �Է� �ý��� ���� ����
            PlayerTransformationController originalController = originalCharacter.GetComponent<PlayerTransformationController>();
            
            if (originalController != null)
            {
                originalController.uiManager = uiManager;
                originalController.playerTransformController = playerTransformController;
            }
            
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
