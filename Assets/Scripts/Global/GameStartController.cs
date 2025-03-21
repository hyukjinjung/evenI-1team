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
    public ChasingMonsterAnimationController animationController;



    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        chasingMonster = FindObjectOfType<ChasingMonster>();

        playerAnimaiton = GameObject.FindWithTag("Player")?.GetComponentInChildren<Animator>();

        monsterAnimation = chasingMonster?.GetComponentInChildren<Animator>();
    }


    public void OpeningSequence()
    {
        if (playerAnimaiton == null || monsterAnimation == null)
        {
            Debug.Log("애니메이션을 찾을 수 없음");
            return;
        }

        StartCoroutine(WaitForAnimationAndStartGame());

    }


    private IEnumerator WaitForAnimationAndStartGame()
    {
        playerAnimaiton.SetTrigger("SupriseAnim");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);


        animationController.PlayMonsterRoar();
        //SoundManager.Instance.PlayClip(5);
        yield return new WaitForSeconds(monsterAnimation.GetCurrentAnimatorStateInfo(0).length);



        monsterAnimation.SetTrigger("inMoving");

        playerAnimaiton.SetTrigger("TurnAround");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.StartGame();
    }
}
