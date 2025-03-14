using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonsterManager : MonoBehaviour
{
    public static ChasingMonsterManager Instance { get; private set; }

    public GameObject monsterPrefab;
    public Transform player;
    private ChasingMonster currentMonster;

    public CameraController cameraController;
    private UIPlayingPanel uiPlayingPanel;
    private UIChasingMonsterGauge uiChasingMonsterGauge;

    private ChasingMonsterSilhouette silhouette;
    private ChasingMonsterDistanceState lastState = ChasingMonsterDistanceState.Far;

    [Header("ChasingMonster Distance Settings")]
    [SerializeField] private float followDistance = 100f;
    [SerializeField] private float mediumDistance = 80f;
    [SerializeField] private float closeDistance = 20f;


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
        silhouette = FindObjectOfType<ChasingMonsterSilhouette>();
    }



    private void Update()
    {
        if (currentMonster == null || player == null) return;

        float distance = Vector3.Distance(player.position, currentMonster.transform.position);
        ChasingMonsterDistanceState state = GetDistanceState(distance);

        if (state != lastState)
        {
            silhouette?.SetSilhouette(state == ChasingMonsterDistanceState.Close);
            lastState = state;
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
        if (monsterPrefab == null || player == null || currentMonster != null)
            return;

        GameObject monsterObj = Instantiate(monsterPrefab);
        monsterObj.transform.position = player.position - new Vector3(0, followDistance, 0);
        currentMonster = monsterObj.GetComponent<ChasingMonster>();
        currentMonster?.Initialize(player);
    }


    private ChasingMonsterDistanceState GetDistanceState(float distance)
    {
        if (distance > mediumDistance)
            return ChasingMonsterDistanceState.Far;
        else if (distance > closeDistance)
            return ChasingMonsterDistanceState.Medium;
        else
            return ChasingMonsterDistanceState.Close;
    }
}
