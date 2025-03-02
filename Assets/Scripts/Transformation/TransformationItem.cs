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


    // 변신 시작
    public void ApplyTransformation(PlayerTransformationController controller)
    {
        controller.Transform(transformationData);
    }


    // 플레이어가 아이템과 충돌하면 변신
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            PlayerTransformationController player = other.GetComponent<PlayerTransformationController>();
            if (player != null)
            {
                Debug.Log("변신 시작");
                player.Transform(transformationData);

                Debug.Log("아이템 제거");
                Destroy(gameObject);
            }
        }
    }
}
