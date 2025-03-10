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
    private bool isFading = false;

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
        Debug.Log("DarkOverlayController: 화면 어둡게 효과 활성화 요청됨");

        // 항상 타이머 리셋
        remainingTime = darknessDuration;

        // 이미 활성화 상태라면 타이머만 리셋하고 리턴
        if (isActive && !isFading)
        {
            Debug.Log("DarkOverlayController: 이미 어둡게 효과가 활성화되어 있음. 타이머만 리셋");
            return;
        }

        // 페이드 중이거나 비활성화 상태라면 새로운 페이드 인 시작
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        Debug.Log("DarkOverlayController: 새로운 페이드 인 시작");
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0;
        float startAlpha = darkOverlay.alpha;
        darkOverlay.blocksRaycasts = true; // UI 차단 설정

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            darkOverlay.alpha = Mathf.Lerp(startAlpha, maxAlpha, elapsedTime / fadeInTime);
            yield return null;
        }

        darkOverlay.alpha = maxAlpha;
        isActive = true;
        isFading = false;
        Debug.Log("DarkOverlayController: 페이드 인 완료");
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
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
        isFading = false;
        Debug.Log("DarkOverlayController: 페이드 아웃 완료");
    }

    private void Update()
    {
        if (!isActive || isFading) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            Debug.Log("DarkOverlayController: 타이머 종료, 페이드 아웃 시작");
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    // 현재 어둡게 효과가 활성화되어 있는지 확인
    public bool IsDarknessActive()
    {
        return isActive;
    }

    // 게임 오버 시 호출할 메서드 추가
    public void DeactivateDarkness()
    {
        Debug.Log("DarkOverlayController: 화면 어둡게 효과 강제 비활성화");
        
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
        
        // 즉시 투명하게 설정
        darkOverlay.alpha = 0;
        darkOverlay.blocksRaycasts = false;
        isActive = false;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}