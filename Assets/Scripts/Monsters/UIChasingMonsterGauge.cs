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
                Debug.Log(" ���Ϳ��� �Ÿ� 80ĭ �ʰ�");
                break;
            case ChasingMonsterDistanceState.Medium:
                gaugeImage.sprite = mediumSprite;
                Debug.Log(" ���Ϳ��� �Ÿ� 21~80ĭ");
                break;
            case ChasingMonsterDistanceState.Close:
                gaugeImage.sprite = closeSprite;
                Debug.Log(" ���Ϳ��� �Ÿ� 20ĭ �̸�");
                SoundManager.Instance.PlayClip(26);
                break;
        }
    }
}
