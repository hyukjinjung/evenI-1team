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
    private PlayerTransformationController playerTransformationController;

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
        playerTransformationController = GetComponent<PlayerTransformationController>();

        // �ߺ� ���� ����
        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;


    }


    // �ܺο��� ���� ���¸� ������ �� �ֵ���
    public void SetTransformedState(bool transformed, SpecialAbilityData ability)
    {
        Debug.Log($"���� ���� ���� - ���� ����: {transformed}, Ư�� �ɷ�: {(ability != null ? "������" : "NULL")}");

        isTransformed = transformed;
        specialAbilityData = ability;
    }


    // ���� �������� Ȯ�� �� Ư�� ���� or �Ϲ� ������ ����
    void PerformAttack(bool isleft)
    {
        // ���� ���� ���� �߰� ������ ����
        if (isAttacking)
        {
            Debug.Log("���� ���� �� �߰� ���� �Ұ���");
            return;
        }

        Debug.Log($"���� ����: {isTransformed}, Ư�� �ɷ�: {(specialAbilityData != null ? "������" : "NULL")}");

        // Ư�� ����
        // ���� ���¶�� Ư�� ���� ����
        if (isTransformed && specialAbilityData != null)
        {

            // Ư�� �ɷ� Ȱ��ȭ
            Debug.Log("Ư�� �ɷ� Ȱ��ȭ");
            specialAbilityData.ActivateAbility(transform);

            // Ư�� ���ݿ� �´� �ִϸ��̼� ó�� ����
        }
        else
        {
            NormalAttack(isleft);
        }



        // �Ϲ� ����
        // NormalFrog ������ �Ϲ� ���� ����
        void NormalAttack(bool isleft)
        {
            Tile tile = testTileManager.GetTile(currentFloor);
            if (tile == null) return;


            // Ÿ�� ������ ������ ���� ���� ���� ����
            bool attackLeft = tile.TileOnLeft(transform);
            // �Ϲ� ���� �ִϸ��̼� ����
            playerAnimationController.SetAttacking(attackLeft);


            isAttacking = true;
            Debug.Log("���� ����");
            StartCoroutine(ResetAttackFlag());


        }
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
