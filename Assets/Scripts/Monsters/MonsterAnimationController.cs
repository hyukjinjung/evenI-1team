using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public void PlayMonsterDieAnimation()
    //{
    //    animator.SetTrigger("Death");
    //}


    public void StartDeath(Monster monster)
    {
        StartCoroutine(DestrotyAfterAnimation(monster));
    }
    

    private IEnumerator DestrotyAfterAnimation(Monster monster)
    {
        // Death 애니메이션 트리거 실행
        //PlayMonsterDieAnimation();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float deathAnimationLength = stateInfo.length;

        if (stateInfo.IsName("Death"))
        {
            deathAnimationLength = stateInfo.length;
        }

        yield return new WaitForSeconds(deathAnimationLength);

    }
}
