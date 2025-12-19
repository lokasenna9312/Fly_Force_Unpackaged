using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    // 생성위치
    public Transform[] enemySpawns;
    // 적 프리팹
    public GameObject[] enemyGameObject;
    // 시간을 재는 변수
    float time;
    // 적 생성 시간
    float respawnTime;
    // 적 생성 숫자
    int enemyCount;
    // 랜덤 숫자 변수를 저장하는 배열
    int[] randomCount;
    // 웨이브
    int wave;
    int bossTrigger;
    // 보스 관련
    public GameObject bossGameObject;
    public bool isBossSpawned;

    public static EnemySpawnController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0.0f;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;
        bossTrigger = 0;
        isBossSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        // 보스 생성
        if (wave > 0 && bossTrigger == 5 && isBossSpawned == false)
        {
            CreateBoss();
        }
    }


    void Timer()
    {
        time += Time.deltaTime;
        if (time > respawnTime)
        {
            randomPos();
            CreateEnemy();
            wave++;
            if (isBossSpawned == false)
                bossTrigger++;
            time -= time;
        }
    }

    void randomPos()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            randomCount[i] = Random.Range(0, enemySpawns.Length);
        }
    }
    void CreateEnemy()
    {
        if (GameManager.instance.lifeCount < 0)
            return;
        for (int i = 0; i < enemyCount; i++)
        {
            // 랜덤 적 선택
            int tmpCnt = Random.Range(0, enemyGameObject.Length);
            // 생성
            GameObject tmp = Instantiate(enemyGameObject[tmpCnt]);
            // 위치
            tmp.transform.position = enemySpawns[randomCount[i]].position;
            // 동일 위치를 방지하기 위한 조금의 값 수정
            float tmpX = tmp.transform.position.x;
            float result = Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, tmp.transform.position.z);
        }
    }
    void CreateBoss()
    {
        bossTrigger = 0;
        isBossSpawned = true;
        GameObject boss = Instantiate(bossGameObject);
        int randomCount = Random.Range(0, enemySpawns.Length);
        boss.transform.position = enemySpawns[randomCount].position;
        BossController bossController = boss.GetComponent<BossController>();
        UIManager.instance.BossIsPresent(bossController);
        /*
        UIManager.instance.MaxHp1 = bossController.hp1;
        UIManager.instance.MaxHp2 = bossController.hp2;
        UIManager.instance.bossController = bossController;
        */
    }
    public void BossIsKilled()
    {
        isBossSpawned = false;
        Debug.Log("Boss has been killed!");
    }
}
