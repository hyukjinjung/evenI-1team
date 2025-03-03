using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

클래스 설명:
변신 아이템을 먹으면 플레이어가 변신하도록 동작
 
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

            Debug.Log("플레이어와 충돌");
            if (controller != null)
            {
                ApplyTransformation(controller);
                //controller.Transform(transformationData);

                Destroy(gameObject);
            }
        }
    }
}
