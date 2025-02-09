using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{
    public Animator animator; // ���� �ִϸ�����
    public Button attackButton; // ���� UI ��ư
    public LayerMask targetLayer; // Ÿ�� �� ���� ���̾�
    public float attackRange = 5f; // ���� ����

    private bool isAttacking = false; // ���� ������ üũ

    // Ÿ�� üũ�� ����ϴ� ������Ʈ
    private TileChecker tileChecker;



    private void Start()
    {
        // ���� ��ư Ŭ�� �� PerformAttack ȣ��
        attackButton.onClick.AddListener(PerformAttack);

        // TileChecker ������Ʈ ��������
        tileChecker = GetComponent<TileChecker>();

        Debug.Log("Attack button listener added");
    }

    void PerformAttack()
    {
        // ���� ���� ���� �߰� ������ ����
        if (isAttacking)
        {
            Debug.Log("Attack already in progress");
            return;
        }


        // ���� �� ���� ����
        isAttacking = true;
        Debug.Log("Attack started");


        bool attackLeft = tileChecker.IsTileInFrontLeft();

        if (attackLeft)
        {
            animator.SetTrigger("AttackLeft");
            Debug.Log("Attack left animation triggered");
        }

        if (!attackLeft) // ���ʿ� Ÿ���� ������ ������ ������ ����
        {
            animator.SetTrigger("AttackRight");
            Debug.Log("Attack right animation triggered");
        }

        // �ִϸ��̼��� ���� ������ ��ٸ���, �ٽ� ���� �����ϵ��� ����
        StartCoroutine(ResetAttackFlag());
    }



    // ���� �� ���¸� �����ϴ� �ڷ�ƾ
    IEnumerator ResetAttackFlag()
    {
        // ���� �ִϸ��̼� ���� ��ŭ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        // ���� �� ���¸� ����
        isAttacking = false;
        Debug.Log("Attack ended. Ready for next Attack");
    }
}
