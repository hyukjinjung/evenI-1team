using UnityEngine;
using System.Collections;

public class DarkOverlayController : MonoBehaviour
{
    public static DarkOverlayController Instance { get; private set; }

    [SerializeField] private CanvasGroup darkOverlay;  // ✅ Unity에서 수동으로 할당할 CanvasGroup

    [Header("Darkness Settings")]
    [SerializeField] private float darknessDuration = 5f;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 0.5f;
    [SerializeField] private float maxAlpha = 0.8f;

    private float remainingTime;
    private bool isActive = false;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // ✅ 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ✅ CanvasGroup이 연결되지 않았다면 자동으로 찾기
        if (darkOverlay == null)
        {
            darkOverlay = GetComponent<CanvasGroup>();

            if (darkOverlay == null)
            {
                Debug.LogError("❌ DarkOverlay가 설정되지 않았습니다! Unity에서 연결하세요.");
                return;
            }
        }

        // ✅ 초기 설정 - 시작 시 완전 투명
        darkOverlay.alpha = 0;
        darkOverlay.blocksRaycasts = false; // 어두워지더라도 UI 입력 가능하도록 설정
    }

    // ✅ HideNext 아이템을 먹었을 때 호출
    public void ActivateDarkness()
    {
        if (isActive)
        {
            remainingTime = darknessDuration;
            return;
        }

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeIn());
        isActive = true;
        remainingTime = darknessDuration;
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        darkOverlay.alpha = 0;
        darkOverlay.blocksRaycasts = true; // UI 차단 설정

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            darkOverlay.alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeInTime);
            yield return null;
        }

        darkOverlay.alpha = maxAlpha;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        float startAlpha = darkOverlay.alpha;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            darkOverlay.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeOutTime);
            yield return null;
        }

        darkOverlay.alpha = 0;
        darkOverlay.blocksRaycasts = false; // UI 다시 활성화
        isActive = false;
    }

    private void Update()
    {
        if (!isActive) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
