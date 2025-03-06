using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int feverScore = 300;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FeverSystem feverSystem = FindObjectOfType<FeverSystem>();

            if (feverSystem != null)
            {
                feverSystem.AddFeverScore(FeverScoreType.TestCoin);
                Debug.Log($"테스트 코인 획득. 피버 점수 +{feverScore}");
            }

            Destroy(gameObject);
        }
    }
}
