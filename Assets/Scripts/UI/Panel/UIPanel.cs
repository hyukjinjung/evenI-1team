using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected UIManager uiManager;
    public virtual void Initialize(UIManager manager)
    {
        // 모든 패널 공통으로 초기화
        uiManager = manager;
    }
    
    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
