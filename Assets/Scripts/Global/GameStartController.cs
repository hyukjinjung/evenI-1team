using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStartController : MonoBehaviour
{

    public Animator playerAnimaiton;
    public Animator monsterAnimation;

    private void Start()
    {
        if (playerAnimaiton == null)
        playerAnimaiton = GetComponentInChildren<Animator>();

        if (monsterAnimation == null)
        monsterAnimation = GetComponentInChildren<Animator>();
    }


    public void OpeningSequence()
    {
        StartCoroutine(WaitForAnimationAndStartGame());
    }


    private IEnumerator WaitForAnimationAndStartGame()
    {
        playerAnimaiton.SetTrigger("StartButtonClicked");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        playerAnimaiton.SetTrigger("SupriseAnim");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        monsterAnimation.SetTrigger("MonsterRoar");
        yield return new WaitForSeconds(monsterAnimation.GetCurrentAnimatorStateInfo(0).length);

        playerAnimaiton.SetTrigger("TurnAround");
        yield return new WaitForSeconds(playerAnimaiton.GetCurrentAnimatorStateInfo(0).length);

        GameManager.Instance.StartGame();
    }
}
