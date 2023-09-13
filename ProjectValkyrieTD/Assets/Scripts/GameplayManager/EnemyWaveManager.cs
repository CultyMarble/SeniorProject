using System.Text;
using UnityEngine;
using TMPro;

public class EnemyWaveManager : SingletonMonobehaviour<EnemyWaveManager>
{
    [SerializeField] private TextMeshProUGUI waveText = default;
    [SerializeField] private Transform pfTestEnemy = default;

    [Header("Wave Spawnpoint Settings:")]
    [SerializeField] private Transform enemySpawnPoints = default;
    [SerializeField] private Transform enemyPool = default;

    private TimeManager timeManager = default;

    // Wave Manager
    private int waveNumber = default;
    private int spawnIndex = default;
    private StringBuilder waveNumberSB = new();

    // Enemy Pool Manager
    private readonly int enemyPoolSize = 200;

    //===========================================================================
    protected override void Awake()
    {
        base.Awake();

        timeManager = GameObject.FindGameObjectWithTag(Tags.GameplayManager).GetComponent<TimeManager>();
    }

    private void OnEnable()
    {
        timeManager.OnTimeMinuteChanged += Instance_OnTimeMinuteChangedHandler;
        timeManager.OnTimeSecondChanged += Instance_OnTimeSecondChangedHandler;

        PopulateEnemyPool();

        UpdateWaveText();
    }

    private void OnDestroy()
    {
        timeManager.OnTimeMinuteChanged -= Instance_OnTimeMinuteChangedHandler;
        timeManager.OnTimeSecondChanged -= Instance_OnTimeSecondChangedHandler;
    }

    //===========================================================================
    private void Instance_OnTimeMinuteChangedHandler(object sender, TimeManager.OnTimeMinuteChangedEventArgs e)
    {
        waveNumber++;
        UpdateWaveText();
    }

    private void Instance_OnTimeSecondChangedHandler(object sender, TimeManager.OnTimeSecondChangedEventArgs e)
    {
        SpawnEnemy();
    }

    private void UpdateWaveText()
    {
        waveNumberSB.Clear();
        waveNumberSB.Append("Wave: ");
        waveNumberSB.Append(waveNumber);

        waveText.SetText(waveNumberSB);
    }

    private void SpawnEnemy()
    {
        foreach (Transform enemy in enemyPool)
        {
            if (enemy.gameObject.activeSelf == false)
            {
                spawnIndex = UnityEngine.Random.Range(0, enemySpawnPoints.childCount);

                enemy.transform.position = enemySpawnPoints.GetChild(spawnIndex).position;
                enemy.gameObject.SetActive(true);
                break;
            }
        }
    }

    //===========================================================================
    private void PopulateEnemyPool()
    {
        for (int i = 0; i < enemyPoolSize; ++i)
        {
            Transform enemyTransfrom = Instantiate(pfTestEnemy, transform.position, Quaternion.identity, enemyPool);
            enemyTransfrom.gameObject.SetActive(false);
        }
    }

    //===========================================================================
    public void RemoveAllEnemy()
    {
        foreach (Transform enemy in enemyPool)
        {
            if (enemy.gameObject.activeInHierarchy)
                enemy.gameObject.SetActive(false);
        }
    }

    public void ResetWaveNumber()
    {
        waveNumber = 1;
        UpdateWaveText();
    }
}