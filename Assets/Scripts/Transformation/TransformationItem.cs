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
            Debug.Log("플레이어와 충돌");
            PlayerTransformationController player = other.GetComponent<PlayerTransformationController>();
            if (player != null)
            {
                Debug.Log("변신 시작");
                player.StartTransformation(transformationData);

                Debug.Log("아이템 제거");
                Destroy(gameObject);
            }
        }
    }
}
