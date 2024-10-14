using System.Collections;
using System.Linq;
using Unity.Entities;
using Unity.Scenes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private SubScene m_SubScene = null;
    private World m_TargetWorld = null;
    private SpawnerAuthoring m_Spawner = null;

    [SerializeField] private Slider m_EnemyMaxSlider = null;
    [SerializeField] private Slider m_SpawnTimeSlider = null;

    private int m_EnemyMaxValue = 10;
    private float m_SpawnTimeValue = 1.0f;    

    private void Start()
    {
        if (m_EnemyMaxSlider != null)
            m_EnemyMaxSlider.onValueChanged.AddListener(OnEnemyMaxValueChanged);

        if (m_SpawnTimeSlider != null)
            m_SpawnTimeSlider.onValueChanged.AddListener(OnSpawnTimerValueChanged);

        StartCoroutine(FindEntityAuthoringInSubScene());
    }

    public void OnEnemyMaxValueChanged(float inValue)
    {
        m_EnemyMaxValue = (int)inValue;
        SendToECS();
    }

    public void OnSpawnTimerValueChanged(float inValue)
    {
        m_SpawnTimeValue = inValue;
        SendToECS();
    }

    private void SendToECS()
    {
        var entityMgr = m_TargetWorld.EntityManager;

        if (m_Spawner != null)
        {
            Debug.Log("Entity is not null");
            if(entityMgr.HasComponent<SpawnerComponent>(m_Spawner.TargetEntity))
            {
                Debug.Log("Has");
                var spawnComponent = entityMgr.GetComponentData<SpawnerComponent>(m_Spawner.TargetEntity);
                if (spawnComponent.IsUnityNull() == false)
                {
                    spawnComponent.TargetCount = m_EnemyMaxValue;
                    spawnComponent.SpawnInterval = m_SpawnTimeValue;
                    entityMgr.SetComponentData<SpawnerComponent>(m_Spawner.TargetEntity, spawnComponent);
                }
            }
        }
        else
            Debug.Log("Not");
    }

    private IEnumerator FindEntityAuthoringInSubScene()
    {
        while (m_SubScene.IsLoaded == false)
            yield return null;

        m_Spawner = FindFirstObjectByType<SpawnerAuthoring>();

        SelectWorld();
    }

    private void SelectWorld()
    {
        if(m_TargetWorld == null)
        {
            foreach (var world in World.All)
            {
                if (world.Name.Contains("EntityScene"))
                {
                    m_TargetWorld = world;
                    break;
                }
            }
        }
    }
}
