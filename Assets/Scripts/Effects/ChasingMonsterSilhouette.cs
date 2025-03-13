using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonsterSilhouette : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    private bool isSilhouetteActive = false;



    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.SetActive(false);
    }


    void Update()
    {
        if (isSilhouetteActive)
        {
            transform.position = player.position + new Vector3(0, -0.5f, 0);
        }
    }


    public void ShowSilhouette()
    {
        if (!isSilhouetteActive)
        {
            gameObject.SetActive(true);
            animator.SetTrigger("ShowSilhouette");
            isSilhouetteActive = true;
        }
    }


    public void HideSilhouette()
    {
        if (isSilhouetteActive)
        {
            animator.SetTrigger("HideSilhouette");
            isSilhouetteActive = false;
            StartCoroutine(DisableAfterAnimation());
        }
    }


    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
