using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

클래스 설명:
변신 아이템을 먹으면 플레이어가 변신하도록 동작
 
*/

public class TransformationItem : MonoBehaviour
{
    public TransformationType type;

    public void ApplyTransformation(PlayerTransformationController controller)
    {
        controller.Transform(type);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerTransformationController controller = other.GetComponent<PlayerTransformationController>();
        if (controller != null)
        {
            controller.IsFeverBlockingTransform();

            ApplyTransformation(controller);
            Destroy(gameObject);
            GameManager.Instance.AddScore(3);
            SoundManager.Instance.PlayClip(7);
        }
    }
}
