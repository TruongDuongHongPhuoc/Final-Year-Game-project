using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerRpg : MonoBehaviour
{
    public static List<SpawnerRpg> spawnerRpgs = new List<SpawnerRpg>();
    public Enemy objectPrefab;
    public float spawnInterval = 1f;
    public float spawnRadius = 5f;
    public int maxspawn = 3;
    private float timer;
    public List<GameObject> SpawnedObject = new List<GameObject>();
    public EnemyFactory Weakfactory;
    public EnemyFactory NormalFactory;
    public EnemyFactory StrongFactory;
    [SerializeField] GameObject MovingArea;
    private PlayerCondition playerCondition;
    [SerializeField] private float weakSpawnRate;
    [SerializeField] private float NormalSpawnRate;
    [SerializeField] private float StrongSpawnRate;
    [SerializeField] private float variabilityOfWeakSpawnRate;
    [SerializeField] private float variabilityOfNormalSpawnRate;
    [SerializeField] private float variabilityOfStrongSpawnRate;
    public static bool PlayerLevelUp = false;
    private void Start()
    {
        this.playerCondition = PlayerController.intance.playerCondition;
        SpawnerRpg.spawnerRpgs.Add(this);
        UpdateAllSpawner();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && SpawnedObject.Count < maxspawn)
        {
            // Tạo một đối tượng ở vị trí ngẫu nhiên trên mặt đất
            GenerateEnemy();
            SpawnObjectOnGround();
            timer = spawnInterval;
        }
    }
    // 1 -> weak 2 -> normal 3 -> hard
    public void GenerateEnemy()
    {
        int scale = RandomOutCome();
        switch (scale)
        {
            case 1:
                objectPrefab = Weakfactory.GenerateEnemy(objectPrefab);
                // Debug.Log("Spawn in weak factory");
                break;
            case 2:
                objectPrefab = NormalFactory.GenerateEnemy(objectPrefab);
                // Debug.Log("Spawn in normal factory");
                break;
            case 3:
                objectPrefab = StrongFactory.GenerateEnemy(objectPrefab);
                // Debug.Log("Spawn in strong factory");
                break;
        }
    }
    void SpawnObjectOnGround()
    {
        Vector3 randomPosition = RandomPointOnGround(transform.position, spawnRadius);
        SpawnEntity(randomPosition);
    }
    public void SpawnEntity(Vector3 randomPosition)
    {
        var JustPawn = Instantiate(objectPrefab, randomPosition, Quaternion.identity, transform);
        JustPawn.gameObject.GetComponent<MovingAround>().MovingArea = this.MovingArea.GetComponent<BoxCollider>();
        SpawnedObject.Add(JustPawn.gameObject);
    }
    Vector3 RandomPointOnGround(Vector3 center, float radius)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * radius;
        randomPoint.y = 100f; // Bất kỳ chiều cao nào (ví dụ: 100) đủ lớn để đảm bảo tia Raycast từ trên xuống mặt đất

        RaycastHit hit;
        int attempts = 0;
        while (attempts < 10) // Giới hạn số lần thử để tránh vòng lặp vô hạn
        {
            if (Physics.Raycast(randomPoint, Vector3.down, out hit, Mathf.Infinity))
            {
                return hit.point;
            }
            else
            {
                // Nếu không tìm thấy mặt đất, thử lại với một vị trí ngẫu nhiên mới
                randomPoint = center + Random.insideUnitSphere * radius;
                randomPoint.y = 100f;
                attempts++;
            }
        }

        // Trả về vị trí gốc nếu không tìm thấy mặt đất sau số lần thử nhất định
        return center;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

    }
    public void RandomStrongToWeakCharacterLevelBase()
    {
        int level = playerCondition.CurrentCharacter.useUnit.Level;

    }
    public int RandomOutCome()
    {
        float randomValue = UnityEngine.Random.Range(0, 100f);
        if (randomValue < weakSpawnRate)
        {
            return 1;
        }
        else if (randomValue < weakSpawnRate + NormalSpawnRate)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
    public void isPlayerLevelEqualWeak(int playerlevel)
    {
        if (playerlevel == Weakfactory.minLevel)
        {
            Debug.Log("Weak factory");
            
            int graph = Weakfactory.maxLevel - Weakfactory.minLevel;
            variabilityOfWeakSpawnRate = CalculateVariablity(graph, weakSpawnRate, 25f);
            variabilityOfNormalSpawnRate = CalculateVariablity(graph, NormalSpawnRate, 70f);
            variabilityOfStrongSpawnRate = CalculateVariablity(graph, StrongSpawnRate, 5f);
            weakSpawnRate = 75;
            NormalSpawnRate = 25;
            StrongSpawnRate = 5;
        }
    }
    public void isPlayerLevelEqualNormal(int playerlevel)
    {
        if (playerlevel == NormalFactory.minLevel)
        {
            Debug.Log("NOrmal factory");
            int graph = NormalFactory.maxLevel - NormalFactory.minLevel;
            variabilityOfWeakSpawnRate = CalculateVariablity(graph, weakSpawnRate, 25f);
            variabilityOfNormalSpawnRate = CalculateVariablity(graph, NormalSpawnRate, 60);
            variabilityOfStrongSpawnRate = CalculateVariablity(graph, StrongSpawnRate, 5);
             weakSpawnRate = 25;
            NormalSpawnRate = 70;
            StrongSpawnRate = 5;
        }
    }
    public void isPlayerLevelEqualStrong(int playerlevel)
    {
        if (playerlevel == StrongFactory.minLevel)
        {
            Debug.Log("strong factory");
            int graph = StrongFactory.maxLevel - StrongFactory.minLevel;
            variabilityOfWeakSpawnRate = CalculateVariablity(graph, weakSpawnRate, 0);
            variabilityOfNormalSpawnRate = CalculateVariablity(graph, NormalSpawnRate, 0);
            variabilityOfStrongSpawnRate = CalculateVariablity(graph + 6, StrongSpawnRate, 100);
            weakSpawnRate = 10;
            NormalSpawnRate = 30;
            StrongSpawnRate = 65;
        }
    }
    public static void UpdateAllSpawner()
    {
        foreach (SpawnerRpg spawnerRpg in spawnerRpgs)
        {
            updateSpawnRate(spawnerRpg);
            Debug.Log("Weak spawner" + spawnerRpg.weakSpawnRate);
        }
    }
    public static void updateSpawnRate(SpawnerRpg spawner)
    {
        spawner.isPlayerLevelEqualWeak(spawner.playerCondition.CurrentCharacter.useUnit.Level);
        spawner.isPlayerLevelEqualStrong(spawner.playerCondition.CurrentCharacter.useUnit.Level);
        spawner.isPlayerLevelEqualNormal(spawner.playerCondition.CurrentCharacter.useUnit.Level);
        Debug.Log("Spawner Rate" + spawner.weakSpawnRate);
        Debug.Log("Spawner Rate" + spawner.NormalSpawnRate);
        Debug.Log("Spawner Rate" + spawner.StrongSpawnRate);
        spawner.weakSpawnRate += spawner.variabilityOfWeakSpawnRate;
        spawner.NormalSpawnRate += spawner.variabilityOfNormalSpawnRate;
        spawner.StrongSpawnRate += spawner.variabilityOfStrongSpawnRate;
        // if (true)
        // {

        // }
    }
    public float CalculateVariablity(float graph, float currentOdd, float AimedOdd)
    {
        float spawnrate = (AimedOdd - currentOdd) / graph;
        return spawnrate;
    }
}
