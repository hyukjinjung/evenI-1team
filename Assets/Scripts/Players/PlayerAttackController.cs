using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{

    private bool isAttacking = false; // ���� ������ üũ
    private bool isTransformed = false; // ���� ���� ����

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;


    [SerializeField] private int currentFloor = 0;

    private GameManager gameManager; // GamaeManager �ν��Ͻ� �߰�

    // ���� ���¿��� ����� Ư�� �ɷ� (ScriptableObject)
    public SpecialAbilityData specialAbilityData;


    private void Start()
    {
        gameManager = GameManager.Instance; // GameManager ��������

        if (gameManager == null)
            return;

        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();

        // �ߺ� ���� ����
        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;


    }


    // �ܺο��� ���� ���¸� ������ �� �ֵ���
    public void SetTransformedState(bool transformed)
    {
        isTransformed = transform;
    }


    void PerformAttack(bool isleft)
    {
        // ���� ���� ���� �߰� ������ ����
        if (isAttacking)
        {
            Debug.Log("is Attacking");
            return;
        }



        // Ư�� ����
        // ���� ���¶�� Ư�� ���� ����
        if (isTransformed)
        {
            if (specialAbilityData != null)
            {
                // Ư�� �ɷ� Ȱ��ȭ
                specialAbilityData.ActivateAbility(transform);

                // Ư�� ���ݿ� �´� �ִϸ��̼� ó�� ����

            }
        }



        // �Ϲ� ����
        // NormalFrog ������ �Ϲ� ���� ����
        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null)
        {
            Debug.Log("Ÿ�� null");
            return;
        }


        // Ÿ�� ������ ������ ���� ���� ���� ����
        bool isLeft = tile.TileOnLeft(transform);


        // �Ϲ� ���� �ִϸ��̼� ����
        playerAnimationController.SetAttacking(isLeft);
        isAttacking = true;
        Debug.Log("Attack started");
        StartCoroutine(ResetAttackFlag());

    }



    // ���� �� ���¸� �����ϴ� �ڷ�ƾ
    IEnumerator ResetAttackFlag()
    {
        // �ִϸ��̼� ���̸� ������
        float attackAnimationLength = playerAnimationController.GetAttackAniamtionLength();

        // ���� ������ Ư�� ���� �ִϸ��̼� ����

        yield return new WaitForSeconds(attackAnimationLength);

        isAttacking = false; // ���� ���� ���·� ����
        Debug.Log("���� ���� �غ�");
    }
}
