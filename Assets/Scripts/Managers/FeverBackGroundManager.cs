using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBackGroundManager : MonoBehaviour
{

    [SerializeField] private BackGroundScroller backGroundScroller;
    [SerializeField] private GameObject normalBackground;
    [SerializeField] private GameObject feverBackground;

    void Start()
    {
        normalBackground.SetActive(true);
        feverBackground.SetActive(false);
    }

    public void SetFeverMode(bool isFeverActive)
    {
        normalBackground.SetActive(!isFeverActive);
        feverBackground.SetActive(isFeverActive);

        //�ʿ��ϴٸ� �߰������� ��� ��ȯ �ִϸ��̼� ȿ���� �̰��� �����մϴ�.
    }


    //private BackGroundScroller backGroundScroller;

    //[SerializeField] private GameObject normalBackground;
    //[SerializeField] private GameObject feverBackground;


    //void Start()
    //{
    //    backGroundScroller = FindObjectOfType<BackGroundScroller>();

    //    normalBackground.SetActive(true);
    //    feverBackground.SetActive(false);
    //}


    //public void SetFeverMode(bool isFeverActive)
    //{
    //    if (isFeverActive)
    //    {
    //        normalBackground.SetActive(false);
    //        feverBackground.SetActive(true);
    //    }
    //    else
    //    {
    //        normalBackground.SetActive(true);
    //        feverBackground.SetActive(false);
    //    }
    //}
}
