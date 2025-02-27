using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public Button toggleButton;  // ��ư ������Ʈ
    public Image buttonImage;    // �̹��� ������Ʈ (��ư�� �̹���)
    public Sprite activeSprite;  // Ȱ��ȭ�� ���� �̹���
    public Sprite inactiveSprite; // ��Ȱ��ȭ�� ���� �̹���

    private bool isActive = true; // �ʱ� ���´� Ȱ��ȭ ����

    private void Start()
    {
        // ��ư Ŭ�� �� ToggleImage �Լ� ȣ��
        toggleButton.onClick.AddListener(ToggleImage);
    }

    // ��ư Ŭ�� �� �̹��� ��ȯ
    private void ToggleImage()
    {
        // �̹��� ���� ���
        isActive = !isActive;

        if (isActive)
        {
            buttonImage.sprite = activeSprite;  // Ȱ��ȭ�� �̹����� ����
        }
        else
        {
            buttonImage.sprite = inactiveSprite;  // ��Ȱ��ȭ�� �̹����� ����
        }
    }


    /*using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    public AudioSource audioSource;  // �Ҹ� ������ AudioSource
    public Button toggleButton;      // ��ư ������Ʈ
    public Image soundImage;         // �̹��� ������Ʈ (�Ҹ� ������)
    public Sprite soundOnSprite;     // �Ҹ� Ȱ��ȭ�� ���� �̹���
    public Sprite soundOffSprite;    // �Ҹ� ��Ȱ��ȭ�� ���� �̹���
    private bool isSoundActive = true;  // �ʱ� ���´� �Ҹ� Ȱ��ȭ

    private void Start()
    {
        // ��ư Ŭ�� �� ToggleSoundState ȣ��
        toggleButton.onClick.AddListener(ToggleSoundState);
        UpdateImage();  // �ʱ� �̹����� ����
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    private void ToggleSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");
        }

        UpdateImage();  // �̹��� ������Ʈ
    }

    // �Ҹ� ���¿� ���� �̹����� ������Ʈ�ϴ� �Լ�
    private void UpdateImage()
    {
        // �Ҹ� ���¿� ���� �̹��� ����
        if (isSoundActive)
        {
            soundImage.sprite = soundOnSprite;  // Ȱ��ȭ�� �̹���
        }
        else
        {
            soundImage.sprite = soundOffSprite;  // ��Ȱ��ȭ�� �̹���
        }
    }
}
*/
}



