using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public Button toggleButton;  // 버튼 컴포넌트
    public Image buttonImage;    // 이미지 컴포넌트 (버튼의 이미지)
    public Sprite activeSprite;  // 활성화된 상태 이미지
    public Sprite inactiveSprite; // 비활성화된 상태 이미지

    private bool isActive = true; // 초기 상태는 활성화 상태

    private void Start()
    {
        // 버튼 클릭 시 ToggleImage 함수 호출
        toggleButton.onClick.AddListener(ToggleImage);
    }

    // 버튼 클릭 시 이미지 전환
    private void ToggleImage()
    {
        // 이미지 상태 토글
        isActive = !isActive;

        if (isActive)
        {
            buttonImage.sprite = activeSprite;  // 활성화된 이미지로 설정
        }
        else
        {
            buttonImage.sprite = inactiveSprite;  // 비활성화된 이미지로 설정
        }
    }


    /*using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    public AudioSource audioSource;  // 소리 제어할 AudioSource
    public Button toggleButton;      // 버튼 컴포넌트
    public Image soundImage;         // 이미지 컴포넌트 (소리 아이콘)
    public Sprite soundOnSprite;     // 소리 활성화된 상태 이미지
    public Sprite soundOffSprite;    // 소리 비활성화된 상태 이미지
    private bool isSoundActive = true;  // 초기 상태는 소리 활성화

    private void Start()
    {
        // 버튼 클릭 시 ToggleSoundState 호출
        toggleButton.onClick.AddListener(ToggleSoundState);
        UpdateImage();  // 초기 이미지를 설정
    }

    // 버튼 클릭 시 호출되는 함수
    private void ToggleSoundState()
    {
        isSoundActive = !isSoundActive;  // 소리 상태를 반전

        // 소리 상태에 맞춰 소리 제어
        if (isSoundActive)
        {
            audioSource.Play();  // 소리 활성화
            Debug.Log("소리 활성화");
        }
        else
        {
            audioSource.Pause();  // 소리 비활성화
            Debug.Log("소리 비활성화");
        }

        UpdateImage();  // 이미지 업데이트
    }

    // 소리 상태에 맞춰 이미지를 업데이트하는 함수
    private void UpdateImage()
    {
        // 소리 상태에 따라 이미지 변경
        if (isSoundActive)
        {
            soundImage.sprite = soundOnSprite;  // 활성화된 이미지
        }
        else
        {
            soundImage.sprite = soundOffSprite;  // 비활성화된 이미지
        }
    }
}
*/
}



