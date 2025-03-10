using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffectSpawner : MonoBehaviour
{
    public GameObject jumpEffectPrefab;


    public void SpawnJumpEffect(Vector2 position)
    {
        GameObject effect = Instantiate(jumpEffectPrefab, position, Quaternion.identity);
        Animator effectAnimator = effect.GetComponent<Animator>();
        effectAnimator.SetTrigger("jumpEffectPrefab");

        // ����Ʈ�� �ڵ����� ��������� ����
        Destroy(effect, effectAnimator.GetCurrentAnimatorStateInfo(0).length);

       
    }
}
