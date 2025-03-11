using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Opaint : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private GameObject shieldEffectPrefab; // 쉴드 이펙트 프리팹
    [SerializeField] private float shieldDuration = 5f; // 쉴드 지속 시간 (옵션)
    
    private bool hasShield = false;
    private SpriteRenderer playerSprite;
    private GameObject activeShieldEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("✅ 쉴드 아이템(Opaint) 획득!");
            
            // 플레이어의 컴포넌트 가져오기
            playerSprite = collision.GetComponent<SpriteRenderer>();
            
            // 쉴드 활성화
            ActivateShield(collision.gameObject);
            
            // 아이템 제거
            Destroy(gameObject);
        }
    }
    
    private void ActivateShield(GameObject player)
    {
        Debug.Log("✅ 쉴드 활성화!");
        hasShield = true;
        
        // 쉴드 시각 효과 생성
        if (shieldEffectPrefab != null)
        {
            activeShieldEffect = Instantiate(shieldEffectPrefab, player.transform);
            activeShieldEffect.transform.localPosition = Vector3.zero; // 플레이어 중심에 위치
        }
        
        // 플레이어 스프라이트에 쉴드 효과 적용 (옵션)
        if (playerSprite != null)
        {
            playerSprite.color = new Color(1f, 1f, 1f, 0.8f);
        }
        
        // 플레이어에게 쉴드 상태 부여
        ShieldState shieldState = player.GetComponent<ShieldState>();
        if (shieldState == null)
        {
            shieldState = player.AddComponent<ShieldState>();
        }
        shieldState.ActivateShield(shieldDuration);
    }
}

// 플레이어의 쉴드 상태를 관리하는 컴포넌트
public class ShieldState : MonoBehaviour
{
    private bool hasShield = false;
    private GameObject shieldEffect;
    private SpriteRenderer playerSprite;
    
    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }
    
    public void ActivateShield(float duration)
    {
        hasShield = true;
        
        // 기존 코루틴이 있다면 중지
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(DeactivateShieldAfterDelay(duration));
        }
    }
    
    private System.Collections.IEnumerator DeactivateShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DeactivateShield();
    }
    
    public void DeactivateShield()
    {
        Debug.Log("쉴드 비활성화");
        hasShield = false;
        
        // 쉴드 시각 효과 제거
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Contains("Shield"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
        
        // 플레이어 스프라이트 원래대로
        if (playerSprite != null)
        {
            playerSprite.color = Color.white;
        }
    }
    
    public bool HasShield()
    {
        return hasShield;
    }
    
    public void UseShield()
    {
        if (hasShield)
        {
            Debug.Log("쉴드로 충돌 방어!");
            DeactivateShield();
        }
    }
    
    private void OnDestroy()
    {
        // 컴포넌트가 제거될 때 쉴드 효과도 제거
        DeactivateShield();
    }
}
