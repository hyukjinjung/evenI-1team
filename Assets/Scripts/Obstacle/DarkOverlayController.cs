using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DarkOverlayController : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static DarkOverlayController Instance { get; private set; }

    [Header("Darkness Settings")]
    [SerializeField] private float darknessDuration = 5f;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 0.5f;
    [SerializeField] private float maxAlpha = 0.8f;

    private CanvasGroup canvasGroup;
    private float remainingTime;
    private bool isActive = false;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // CanvasGroup 컴포넌트 가져오기
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 초기 설정 - 시작 시 완전 투명
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        isActive = false;
    }

    // 플레이어가 HideNext 아이템을 먹었을 때만 호출되는 메서드
    public void ActivateDarkness()
    {
        // 이미 활성화된 상태라면 시간만 리셋
        if (isActive)
        {
            remainingTime = darknessDuration;
            return;
        }

        // 이미 실행 중인 페이드 코루틴 중지
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        // 페이드 인 시작
        fadeCoroutine = StartCoroutine(FadeIn());
        
        isActive = true;
        remainingTime = darknessDuration;
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        canvasGroup.alpha = 0;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeInTime);
            canvasGroup.alpha = Mathf.Lerp(0, maxAlpha, normalizedTime);
            yield return null;
        }

        canvasGroup.alpha = maxAlpha;
        fadeCoroutine = null;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeOutTime);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, normalizedTime);
            yield return null;
        }

        canvasGroup.alpha = 0;
        isActive = false;
        fadeCoroutine = null;
    }

    private void Update()
    {
        // 활성화 상태가 아니면 아무것도 하지 않음
        if (!isActive) return;

        // 남은 시간 감소
        remainingTime -= Time.deltaTime;
        
        // 시간이 다 되면 페이드 아웃
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
