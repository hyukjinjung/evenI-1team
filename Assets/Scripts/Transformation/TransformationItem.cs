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

    public void ApplyTransformation(PlayerTransformationController controller)
    {
        controller.Transform(transformationData);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTransformationController controller = other.GetComponent<PlayerTransformationController>();

            Debug.Log("�÷��̾�� �浹");
            if (controller != null)
            {
                ApplyTransformation(controller);
                //controller.Transform(transformationData);

                Destroy(gameObject);
            }
        }
    }
}
