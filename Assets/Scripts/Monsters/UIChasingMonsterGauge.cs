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


    public void UpdateGauge(ChasingMonsterDistanceState state)
    {
        if (state == lastState) return;
        lastState = state;

        switch (state)
        {
            case ChasingMonsterDistanceState.Far:
                gaugeImage.sprite = farSprite;
                Debug.Log(" 몬스터와의 거리 80칸 초과");
                break;
            case ChasingMonsterDistanceState.Medium:
                gaugeImage.sprite = mediumSprite;
                Debug.Log(" 몬스터와의 거리 21~80칸");
                break;
            case ChasingMonsterDistanceState.Close:
                gaugeImage.sprite = closeSprite;
                Debug.Log(" 몬스터와의 거리 20칸 미만");
                SoundManager.Instance.PlayClip(26);
                break;
        }
    }
}
