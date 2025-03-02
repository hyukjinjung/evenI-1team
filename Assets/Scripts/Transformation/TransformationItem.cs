using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Ŭ���� ����:
���� �������� ������ �÷��̾ �����ϵ��� ����
 
*/

public class TransformationItem : MonoBehaviour
{
    public TransformationData transformationData;


    // ���� ����
    public void ApplyTransformation(PlayerTransformationController controller)
    {
        controller.Transform(transformationData);
    }


    // �÷��̾ �����۰� �浹�ϸ� ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹");
            PlayerTransformationController player = other.GetComponent<PlayerTransformationController>();
            if (player != null)
            {
                Debug.Log("���� ����");
                player.Transform(transformationData);

                Debug.Log("������ ����");
                Destroy(gameObject);
            }
        }
    }
}
