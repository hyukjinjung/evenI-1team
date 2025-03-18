using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStartController : MonoBehaviour
{

    public Animator playerAnimaiton;
    public Animator monsterAnimation;

    public CinemachineVirtualCamera virtualCamera;
    public Transform monsterTransform;

    public UIManager uiManager;
    public ChasingMonster chasingMonster;



    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        chasingMonster = FindObjectOfType<ChasingMonster>();

        playerAnimaiton = GetComponentInChildren<Animator>();

        monsterAnimation = GetComponentInChildren<Animator>();
    }


    public void OpeningSequence()
    {
        StartCoroutine(WaitForAnimationAndStartGame());
    }


    private IEnumerator WaitForAnimationAndStartGame()
    {
        //playerAnimaiton.SetTrigger("StartButtonClicked");
        //yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        playerAnimaiton.SetTrigger("SupriseAnim");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        monsterAnimation.SetTrigger("MonsterRoar");
        yield return new WaitForSeconds(monsterAnimation.GetCurrentAnimatorStateInfo(0).length);

        playerAnimaiton.SetTrigger("TurnAround");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.StartGame();
    }
}
