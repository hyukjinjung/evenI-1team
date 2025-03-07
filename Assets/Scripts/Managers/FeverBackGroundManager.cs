using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBackGroundManager : MonoBehaviour
{
    private BackGroundScroller backGroundScroller;

    [SerializeField] private GameObject normalBackground;
    [SerializeField] private GameObject feverBackground;


    void Start()
    {
        backGroundScroller = FindObjectOfType<BackGroundScroller>();

        normalBackground.SetActive(true);
        feverBackground.SetActive(false);
    }


    public void SetFeverMode(bool isFeverActive)
    {
        if (isFeverActive)
        {
            normalBackground.SetActive(false);
            feverBackground.SetActive(true);
        }
        else
        {
            normalBackground.SetActive(true);
            feverBackground.SetActive(false);
        }
    }
}
