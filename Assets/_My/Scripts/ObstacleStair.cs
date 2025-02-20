using System.Collections;
using UnityEngine;

public class ObstacleStair : MonoBehaviour
{
    public enum ObstacleType { ReverseControl, Transparent, Sticky, HideNext }
    public ObstacleType obstacleType;

    private Renderer rend;
    private Collider col;
    private bool isHidden = false;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>(); // 하위 오브젝트까지 탐색
        col = GetComponent<Collider>();

        if (rend == null)
        {
            Debug.LogError($"{gameObject.name} - Renderer가 존재하지 않습니다!");
        }
    }

    public void AssignRandomObstacle()
    {
        obstacleType = (ObstacleType)Random.Range(0, 4);

        if (rend != null && rend.material != null)
        {
            Material newMaterial = new Material(rend.material); // 개별 인스턴스 생성
            rend.material = newMaterial; // 새롭게 생성한 Material을 적용

            switch (obstacleType)
            {
                case ObstacleType.ReverseControl:
                    rend.material.color = Color.blue; // 방향키 반전
                    break;

                case ObstacleType.Transparent:
                    rend.material.color = Color.magenta; // 투명 발판
                    StartCoroutine(ToggleTransparency());
                    break;

                case ObstacleType.Sticky:
                    rend.material.color = Color.yellow; // 끈끈이 발판
                    break;

                case ObstacleType.HideNext:
                    rend.material.color = Color.black; // 다음 발판 숨김
                    StartCoroutine(HideNextStair());
                    break;
            }
        }
    }

    private IEnumerator ToggleTransparency()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (rend != null)
            {
                Color color = rend.material.color;
                color.a = color.a == 1f ? 0.2f : 1f;
                rend.material.color = color;
            }
        }
    }

    private IEnumerator HideNextStair()
    {
        yield return new WaitForSeconds(1f);

        GameObject nextStair = GameObject.FindWithTag("Stair");
        if (nextStair != null && !isHidden)
        {
            Renderer nextRend = nextStair.GetComponent<Renderer>();
            if (nextRend != null)
            {
                nextRend.enabled = false;
                isHidden = true;
                yield return new WaitForSeconds(3f);
                nextRend.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                switch (obstacleType)
                {
                    case ObstacleType.ReverseControl:
                        player.InvertControls(3f);
                        break;

                    case ObstacleType.Sticky:
                        player.ModifySpeed(0.7f, 3f);
                        break;
                }
            }
        }
    }
}
