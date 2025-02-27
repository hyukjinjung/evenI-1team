using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;
    private bool isVisible = true;

    [SerializeField] private float toggleInterval = 3f; // On/Off 간격
    [SerializeField] private float fadeDuration = 1f; // 서서히 사라지는 시간

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider = GetComponent<Collider2D>();

        StartCoroutine(TogglePlatformRoutine());
    }

    private IEnumerator TogglePlatformRoutine()
    {
        while (true)
        {
            if (isVisible)
            {
                yield return StartCoroutine(FadeOut());
            }
            else
            {
                yield return StartCoroutine(FadeIn());
            }

            isVisible = !isVisible;
            yield return new WaitForSeconds(toggleInterval);
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }
        platformCollider.enabled = false; // 충돌 방지
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }
        platformCollider.enabled = true; // 충돌 활성화
    }
}
