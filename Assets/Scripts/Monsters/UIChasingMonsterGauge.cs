using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChasingMonsterGauge : MonoBehaviour
{
    [SerializeField] private Image gaugeImage;
    [SerializeField] private Sprite farSprite;
    [SerializeField] private Sprite MidiumSprite;
    [SerializeField] private Sprite CloseSprite;


    public void UpdateGauge(ChasingMonsterDistanceState state)
    {
        switch (state)
        {
            case ChasingMonsterDistanceState.Far:
                gaugeImage.sprite = farSprite;
                break;
            case ChasingMonsterDistanceState.Medium:
                gaugeImage.sprite = MidiumSprite;
                break;
            case ChasingMonsterDistanceState.Colse:
                gaugeImage.sprite = CloseSprite;
                break;
        }
    }
}
