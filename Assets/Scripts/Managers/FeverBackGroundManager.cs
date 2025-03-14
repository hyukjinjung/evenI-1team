using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FeverBackGroundManager : MonoBehaviour
{
    private SpriteRenderer[] backGroundSprites;
    
    [SerializeField] private Sprite backGroundSprite;
    [SerializeField] private Sprite feverGroundSprite;

    private GameManager gameManager;

    private bool currentFeverState = false;

    void Start()
    {
        gameManager = GameManager.Instance;

        backGroundSprites = GetComponentsInChildren<SpriteRenderer>();
    }


    public void SetFeverMode(bool isFeverActive)
    {
        if (currentFeverState == isFeverActive) return;

        currentFeverState = isFeverActive;

        if (isFeverActive)
        {
            foreach (var spriteRenderer in backGroundSprites)
            {
                spriteRenderer.sprite = feverGroundSprite;
            }
        }
        else
        {
            foreach (var spriteRenderer in backGroundSprites)
            {
                spriteRenderer.sprite = backGroundSprite;
            }
        }
    }
}
