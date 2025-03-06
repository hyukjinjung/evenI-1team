using System.Collections;
using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    [Header("Toggle Settings")]
    [SerializeField] private float toggleInterval = 0.1f;   // 상태 전환 간격
    [SerializeField] private float fadeTime = 0.11f;         // 페이드 효과 시간

    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;
    private bool isVisible = true;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError($" {gameObject.name}에는 SpriteRenderer가 없습니다! Inspector에서 추가하세요.");
            return;
        }

        originalColor = spriteRenderer.color;
        StartCoroutine(TogglePlatformRoutine());
    }

    public void SetToggleInterval(float interval)
    {
        toggleInterval = interval;
    }

    private IEnumerator TogglePlatformRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);

            if (isVisible)
            {
                // 페이드 아웃
                yield return StartCoroutine(FadeRoutine(false));
            }
            else
            {
                // 페이드 인
                yield return StartCoroutine(FadeRoutine(true));
            }

            isVisible = !isVisible;
        }
    }

    private IEnumerator FadeRoutine(bool fadeIn)
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color endColor = originalColor;

        // 페이드 아웃일 경우
        if (!fadeIn)
        {
            endColor.a = 0f;
        }

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            // 현재 색상 계산
            spriteRenderer.color = Color.Lerp(startColor, endColor, normalizedTime);

            // 콜라이더 상태 설정 (투명도 50% 기준)
            if (fadeIn && normalizedTime > 0.5f)
            {
                platformCollider.enabled = true;
            }
            else if (!fadeIn && normalizedTime > 0.5f)
            {
                platformCollider.enabled = false;
            }

            yield return null;
        }

        // 최종 상태 설정
        spriteRenderer.color = endColor;
        platformCollider.enabled = fadeIn;
    }
}
