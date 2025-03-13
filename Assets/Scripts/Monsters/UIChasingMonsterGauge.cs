using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChasingMonsterGauge : MonoBehaviour
{
    [SerializeField] private Image gaugeImage;
    [SerializeField] private Sprite farSprite;
    [SerializeField] private Sprite mediumSprite;
    [SerializeField] private Sprite closeSprite;

    private ChasingMonsterDistanceState lastState = ChasingMonsterDistanceState.Far;
    private RectTransform gaugeTransform;
    private Coroutine shakeCoroutine;


    private void Awake()
    {
        gaugeTransform = GetComponent<RectTransform>();
    }

    public void UpdateGauge(ChasingMonsterDistanceState state)
    {
        if (state == lastState) return;
        lastState = state;

        switch (state)
        {
            case ChasingMonsterDistanceState.Far:
                gaugeImage.sprite = farSprite;
                StartShakeEffect(4f, 6f, 0.2f);
                Debug.Log(" 몬스터와의 거리 80칸 초과");
                break;
            case ChasingMonsterDistanceState.Medium:
                gaugeImage.sprite = mediumSprite;
                Debug.Log(" 몬스터와의 거리 21~80칸");
                StartShakeEffect(5f, 7f, 0.1f);
                break;
            case ChasingMonsterDistanceState.Close:
                gaugeImage.sprite = closeSprite;
                Debug.Log(" 몬스터와의 거리 20칸 미만");
                StartShakeEffect(6f, 9f, 0.02f);
                SoundManager.Instance.PlayClip(26);
                break;
        }
    }


    private void StartShakeEffect(float minshake, float maxshake, float interval)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(ShakeEffect(minshake, maxshake, interval));
    }


    private IEnumerator ShakeEffect(float minshake, float maxShake, float interval)
    {
        while (true)
        {
            Vector2 originalPos = gaugeTransform.anchoredPosition;
            float shakeX = Random.Range(-maxShake, maxShake);
            float shakeY = Random.Range(-minshake, minshake);

            gaugeTransform.anchoredPosition = originalPos + new Vector2(shakeX, shakeY);

            yield return new WaitForSeconds(interval);

            gaugeTransform.anchoredPosition = originalPos;
        }
    }
}
