using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TogglePlatform : MonoBehaviour
{
    // 싱글톤 인스턴스 - 모든 발판의 타이밍 동기화를 위함
    public static TogglePlatformManager Manager { get; private set; }

    [Header("Toggle Settings")]
    [SerializeField] private float fadeTime = 0.1f;       // 페이드 효과 시간

    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;
    private bool isVisible = true;
    private Color originalColor;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // 매니저가 없으면 생성
        if (Manager == null)
        {
            GameObject managerObj = new GameObject("TogglePlatformManager");
            Manager = managerObj.AddComponent<TogglePlatformManager>();
            DontDestroyOnLoad(managerObj);
        }
    }

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
        
        // 매니저에 이 발판 등록 및 현재 상태 동기화
        Manager.RegisterPlatform(this);
        
        // 현재 매니저의 상태에 맞게 초기 상태 설정
        SyncWithManager();
    }

    // 매니저의 현재 상태와 동기화
    public void SyncWithManager()
    {
        if (Manager == null) return;
        
        // 매니저의 현재 상태 확인
        bool shouldBeVisible = Manager.GetCurrentVisibilityState();
        
        // 현재 상태와 다르면 즉시 상태 변경 (페이드 없이)
        if (isVisible != shouldBeVisible)
        {
            isVisible = shouldBeVisible;
            
            if (isVisible)
            {
                // 보이는 상태로 설정
                spriteRenderer.color = originalColor;
                platformCollider.enabled = true;
            }
            else
            {
                // 안 보이는 상태로 설정
                Color invisibleColor = originalColor;
                invisibleColor.a = 0f;
                spriteRenderer.color = invisibleColor;
                platformCollider.enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        // 매니저에서 이 발판 제거
        if (Manager != null)
        {
            Manager.UnregisterPlatform(this);
        }
        
        // 실행 중인 코루틴 중지
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
    }

    // 매니저에 의해 호출되는 토글 메서드
    public void Toggle()
    {
        // 이미 실행 중인 코루틴이 있으면 중지
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        
        fadeCoroutine = StartCoroutine(ToggleRoutine());
    }

    private IEnumerator ToggleRoutine()
    {
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
        fadeCoroutine = null;
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
            CheckPlayerOnPlatform();
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

    public bool IsVisible()
    {
        return isVisible;
    }

    private void CheckPlayerOnPlatform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
            float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

            if (distanceX < 0.5f && distanceY < 0.5f)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}

// 모든 TogglePlatform을 관리하는 매니저 클래스
public class TogglePlatformManager : MonoBehaviour
{
    [Header("Toggle Settings")]
    [SerializeField] private float toggleInterval = 3f;  // 기본값 3초
    
    private List<TogglePlatform> platforms = new List<TogglePlatform>();
    private float nextToggleTime = 0f;
    private bool currentVisibilityState = true;  // 현재 발판들이 보이는 상태인지 여부
    private float cycleStartTime;  // 토글 사이클 시작 시간

    private void Start()
    {
        // 게임 시작 시 첫 번째 토글 시간 설정
        cycleStartTime = Time.time;
        nextToggleTime = cycleStartTime + toggleInterval;
    }

    private void Update()
    {
        // 토글 시간이 되면 모든 플랫폼 토글
        if (Time.time >= nextToggleTime)
        {
            ToggleAllPlatforms();
            nextToggleTime = Time.time + toggleInterval;
            currentVisibilityState = !currentVisibilityState;
        }
    }

    public void RegisterPlatform(TogglePlatform platform)
    {
        if (!platforms.Contains(platform))
        {
            platforms.Add(platform);
        }
    }

    public void UnregisterPlatform(TogglePlatform platform)
    {
        platforms.Remove(platform);
    }

    private void ToggleAllPlatforms()
    {
        foreach (var platform in platforms)
        {
            platform.Toggle();
        }
    }

    public void SetToggleInterval(float interval)
    {
        toggleInterval = interval;
        // 간격 변경 시 다음 토글 시간도 업데이트
        nextToggleTime = Time.time + toggleInterval;
    }
    
    // 현재 발판들이 보이는 상태인지 여부 반환
    public bool GetCurrentVisibilityState()
    {
        return currentVisibilityState;
    }
}
