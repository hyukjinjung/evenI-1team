using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool isCollisionDiabled = false; // �浹 ��ȿȭ (�⺻ false)
    private bool canIgnoreMonster = false; // �нú� ȿ���� ���� ����


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        
    }


    // ���Ϳ� �浹�� �����ϴ� �нú� ȿ�� ����
    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;
        Debug.Log("���Ϳ� �浹 ���� Ȱ��ȭ");
        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    // ���� �ð��� ������ ���� �浹 �ٽ� Ȱ��ȭ
    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("���� �浹 ��Ȱ��ȭ");

    }

    
    // ���� ���� �浹 �� ���� �������� Ȯ��
    public bool CanIgnoreMonster()
    {
        return canIgnoreMonster;
    }
}
