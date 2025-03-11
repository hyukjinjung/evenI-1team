using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Ŭ���� ����:
���� �������� ������ �÷��̾ �����ϵ��� ����
 
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
