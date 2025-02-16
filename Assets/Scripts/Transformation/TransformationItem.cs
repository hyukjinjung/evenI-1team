using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationItem : MonoBehaviour
{
    public TransformationData transformationData;



    public void ApplyTransformation(PlayerTransformationController controller)
    {
        controller.StartTransformation(transformationData);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[TransformationItem] �÷��̾�� �浹");
            PlayerTransformationController player = other.GetComponent<PlayerTransformationController>();
            if (player != null)
            {
                Debug.Log("[TransformationItem] ���� ����");
                player.StartTransformation(transformationData);

                Debug.Log("[TransformationItem] ������ ����");
                Destroy(gameObject);
            }
        }
    }
}
