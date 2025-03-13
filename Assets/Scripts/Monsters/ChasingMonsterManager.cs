using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonsterManager : MonoBehaviour
{
    public static ChasingMonsterManager Instance { get; private set; }

    public GameObject monsterPrefab;
    public Transform player;
    private ChasingMonster currentMonster;

    public float followDistance = 100f;

    private ChasingMonsterSilhouette chasingMonsterSilhouette;
    public CameraController cameraController;
    private UIPlayingPanel uiPlayingPanel;
    private UIChasingMonsterGauge uiChasingMonsterGauge;
    //private ChasingMonsterAnimationController animController;


    private ChasingMonsterDistanceState lastState = ChasingMonsterDistanceState.Far;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    private void Start()
    {
        uiPlayingPanel = FindObjectOfType<UIPlayingPanel>();
        chasingMonsterSilhouette = FindObjectOfType<ChasingMonsterSilhouette>();
    }



    private void Update()
    {
        if (currentMonster == null || player == null) return;

        float distance = Vector3.Distance(player.position, currentMonster.transform.position);
        
        ChasingMonsterDistanceState state = GetDistanceState(distance);

        if (uiChasingMonsterGauge != null) 
        {
            uiChasingMonsterGauge.UpdateGauge(state);
        }

        if (chasingMonsterSilhouette != null)
        {
            if (state == ChasingMonsterDistanceState.Close)
            {
                chasingMonsterSilhouette.ShowSilhouette();
            }
            else
            {
                chasingMonsterSilhouette.HideSilhouette();
            }
        }
    }



    public void Initialize(Transform playerTransform, CameraController cameraController,
        UIChasingMonsterGauge gauge)
    {
        player = playerTransform;
        this.cameraController = cameraController;
        this.uiChasingMonsterGauge = gauge;

        SpawnMonster();
    }



    public void SpawnMonster()
    {
        if (monsterPrefab == null || player == null)
            return;

        if (currentMonster != null)
            return;

        GameObject monsterObj = Instantiate(monsterPrefab);
        Vector3 spawnPosition = player.position - new Vector3(0, followDistance, 0);
        monsterObj.transform.position = spawnPosition;

        currentMonster = monsterObj.GetComponent<ChasingMonster>();
        if (currentMonster != null)
        {
            Debug.Log($"몬스터가 생성됨. {spawnPosition}");
            currentMonster.Initialize(player);
        }

        if (cameraController != null)
        {
            cameraController.chasingMonster = monsterObj.transform;
        }
    }


    private ChasingMonsterDistanceState GetDistanceState(float distance)
    {
        if (distance > 80f)
            return ChasingMonsterDistanceState.Far;
        else if (distance > 20f)
            return ChasingMonsterDistanceState.Medium;
        else
            return ChasingMonsterDistanceState.Close;
    }
}
