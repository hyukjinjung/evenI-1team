using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNextItem : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool canIgnoreMonster = false;

    private PlayerTransformationController playerTransformationController;
    [SerializeField] private CanvasGroup DarkOverlay; 


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerTransformationController = GetComponent<PlayerTransformationController>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("✅ HideNext 아이템 획득! 화면이 어두워집니다.");

            // ✅ DarkOverlayController를 찾아서 실행
            if (DarkOverlayController.Instance != null)
            {
                // 효과 활성화 (이미 활성화되어 있어도 갱신됨)
                DarkOverlayController.Instance.ActivateDarkness();
                Debug.Log($"화면 어둡게 효과 적용됨. 현재 상태: {DarkOverlayController.Instance.IsDarknessActive()}");
            }
            else
            {
                Debug.LogError("❌ DarkOverlayController가 씬에서 찾을 수 없습니다!");
            }

            // ✅ 아이템 제거
            Destroy(gameObject);
        }
    }

    
    private IEnumerator ApplyDarkEffect(float duration)
    {
        if (DarkOverlay == null)
        {
            Debug.LogError("화면이 어두워집니다.");
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
