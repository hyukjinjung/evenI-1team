using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CoinItem : MonoBehaviour
{
    [SerializeField] private int coinvalue = 1;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collection();
        }
    }
    
    
    private void Collection()
    {
        GameManager.Instance.AddCoins(coinvalue);

        //SoundManager.Instance.PlayClip();

        //UI

        gameObject.SetActive(false);
    }

}
