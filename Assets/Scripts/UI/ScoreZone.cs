using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool hasScored = false;
    
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasScored && collision.CompareTag("Player"))
        {
            hasScored = true;
            GameManager.Instance.AddScore(1);
        }
    }    

    public void RestScoreZone()
    {
        hasScored = false;
    }

}
