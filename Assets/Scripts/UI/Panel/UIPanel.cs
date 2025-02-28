using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected UIManager uiManager;
    public virtual void Initialize(UIManager manager)
    {
        // ��� �г� �������� �ʱ�ȭ
        uiManager = manager;
    }
    
    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
