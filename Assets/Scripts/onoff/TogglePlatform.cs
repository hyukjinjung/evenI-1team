using System.Collections;
using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;
    private bool isVisible = true;
    private float toggleInterval = 3f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError($" {gameObject.name}에는 SpriteRenderer가 없습니다! Inspector에서 추가하세요.");
        }

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
                SetPlatformState(false);
            }
            else
            {
                SetPlatformState(true);
            }

            isVisible = !isVisible;
        }
    }

    private void SetPlatformState(bool state)
    {
        if (spriteRenderer == null || platformCollider == null) return;

        spriteRenderer.enabled = state;
        platformCollider.enabled = state;
    }
}
