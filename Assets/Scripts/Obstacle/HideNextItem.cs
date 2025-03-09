using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool isCollisionDiabled = false;
    private bool canIgnoreMonster = false;

    private PlayerTransformationController playerTransformationController;
    [SerializeField] private CanvasGroup DarkOverlay; // ������ �������� UI


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerTransformationController = GetComponent<PlayerTransformationController>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HideNext"))
        {
            Debug.Log("✅ HideNext 아이템 획득! 주변이 어두워집니다.");

            if (DarkOverlayController.Instance != null)
            {
                DarkOverlayController.Instance.ActivateDarkness(); // 어두운 효과 적용
            }
            else
            {
                Debug.LogError("❌ DarkOverlayController가 씬에서 찾을 수 없습니다! 씬에 추가하세요.");
            }

            Destroy(collision.gameObject); // 아이템 삭제
        }

        if (collision.gameObject.CompareTag("HideNext"))
        {
            Debug.Log("HideNext ������ ȹ��! ��ο� ȿ�� ����");
            StartCoroutine(ApplyDarkEffect(5f)); // 5�� ���� ȿ�� ����
            Destroy(collision.gameObject); // ������ ����
        }
    }

    private IEnumerator ApplyDarkEffect(float duration)
    {
        if (DarkOverlay == null)
        {
            Debug.LogError("DarkOverlay�� �������� �ʾҽ��ϴ�! Unity���� �����ϼ���.");
            yield break;
        }

        // ? ��ο� ȿ�� ����
        DarkOverlay.alpha = 1f;

        yield return new WaitForSeconds(duration);

        // ? ȿ�� ����
        DarkOverlay.alpha = 0f;
    }




    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("몬스터 충돌 비활성화");

    }

    

}
