using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float scrollSpeed = 2f;//
    private float backgroundHeight;//배경화면의 가로 사이즈를 가지고 있어야됨



    // Start is called before the first frame update
    void Start()//최초 첫 페이지 마다 가로 사이즈를 불러옴 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//랜더러 안에 있는 값에개 물어볼거야 
        backgroundHeight = spriteRenderer.bounds.size.y;//엑스 사이즈를 불러옴

    }

    // Update is called once per frame
    // void Update()
    // {
    //     transform.Translate(Vector2.up * scrollSpeed * Time.deltaTime);
    //     //위치를 담당하는 트랜스폽, ｙ값 * 
    //
    //     if(transform.position.y < -backgroundHeight)//왼쪽으로 가다가 자기의 수만큼 움직이면 옮기는
    //
    //     {
    //         RepositionBackground();
    //     }
    // }

    void RepositionBackground()//돌아오는 기능 
    {
        Vector3 offset = new Vector3(0, backgroundHeight * 3f - 0.01f); //프래임 사이 중간 공백 한픽셀정도 겹치게 만들려고 0.01뺴줌 
        transform.position += offset;//포지션은 2디지만백터는 2임  백서2로 잡아야된 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RepositionBackground();
    }
}
