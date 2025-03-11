using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    [SerializeField] private GameObject shieldVisualEffect; // 쉴드 시각 효과
    [SerializeField] private float shieldDuration = 5f; // 쉴드 지속 시간 (옵션)
    
    private bool hasShield = false;
    private SpriteRenderer playerSprite;
    
    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        if (shieldVisualEffect != null)
        {
            shieldVisualEffect.SetActive(false);
        }
    }
    
    public void ActivateShield()
    {
        Debug.Log("✅ 쉴드 활성화!");
        hasShield = true;
        
        // 쉴드 시각 효과 표시
        if (shieldVisualEffect != null)
        {
            shieldVisualEffect.SetActive(true);
        }
        
        // 플레이어 스프라이트에 쉴드 효과 적용 (옵션)
        if (playerSprite != null)
        {
            playerSprite.color = new Color(1f, 1f, 1f, 0.8f);
        }
        
        // 지속 시간이 설정된 경우 자동 비활성화
        if (shieldDuration > 0)
        {
            Invoke(nameof(DeactivateShield), shieldDuration);
        }
    }
    
    public void DeactivateShield()
    {
        Debug.Log("쉴드 비활성화");
        hasShield = false;
        
        // 쉴드 시각 효과 제거
        if (shieldVisualEffect != null)
        {
            shieldVisualEffect.SetActive(false);
        }
        
        // 플레이어 스프라이트 원래대로
        if (playerSprite != null)
        {
            playerSprite.color = Color.white;
        }
    }
    
    // 쉴드 상태 확인
    public bool HasShield()
    {
        return hasShield;
    }
    
    // 쉴드 사용 (충돌 시 호출)
    public void UseShield()
    {
        if (hasShield)
        {
            Debug.Log("쉴드로 충돌 방어!");
            DeactivateShield();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShieldState shieldState = collision.GetComponent<ShieldState>();
            
            // 쉴드가 있는 경우
            if (shieldState != null && shieldState.HasShield())
            {
                Debug.Log("쉴드로 방해물 충돌 방어!");
                shieldState.UseShield();
                return;
            }
            
            // 쉴드가 없는 경우 - 기존 충돌 처리 로직 실행
            // 예: GameManager.Instance.GameOver(); 등
        }
    }
} 