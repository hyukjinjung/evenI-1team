using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float scrollSpeed = 2f;
    private float backgroundHeight;
    public bool isGameStarted = false;

    void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            backgroundHeight = spriteRenderer.bounds.size.y;
        }

        void Update()
        {
        if (isGameStarted) // 게임이 시작되었을 때만 스크롤
        {
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            if (transform.position.y < -backgroundHeight)
            {
                RepositionBackground();
            }
        }
    }

        void RepositionBackground()
        {
            Vector3 offset = new Vector3(0, backgroundHeight * 3f);
            transform.position += offset;
        }


        //private SpriteRenderer spriteRenderer;
        //public float scrollSpeed = 2f;
        //private float backgroundHeight;



        //// Start is called before the first frame update
        //void Start()
        //{
        //    spriteRenderer = GetComponent<SpriteRenderer>();
        //    backgroundHeight = spriteRenderer.bounds.size.y;

        //}



        //void RepositionBackground()
        //{
        //    Vector3 offset = new Vector3(0, backgroundHeight * 3f - 0.01f); //프래임 사이 중간 공백 한픽셀정도 겹치게 만들려고 0.01뺴줌 
        //    transform.position += offset;
        //}

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    RepositionBackground();
        //}
}
