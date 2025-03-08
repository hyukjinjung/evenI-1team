using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBackGroundManager : MonoBehaviour
{
    private SpriteRenderer[] backGroundSprites;
    
    [SerializeField] private Sprite backGroundSprite;
    [SerializeField] private Sprite feverGroundSprite;

    void Start()
    {
        backGroundSprites = GetComponentsInChildren<SpriteRenderer>();
    }


    public void SetFeverMode(bool isFeverActive)
    {
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
