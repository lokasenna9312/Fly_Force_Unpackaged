using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerController playerController;
    public Vector3 playerPos;
    public int lifeCount { get; private set; }

    public static GameManager instance;

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
        lifeCount = 2;
        UIManager.instance.LifeCheck(lifeCount);
        CreatePlayerorGameOver();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddLife(int _life)
    {
        lifeCount += _life;
        Debug.Log("Life Count: " + lifeCount);
        UIManager.instance.LifeCheck(lifeCount);
    }

    public void PlayerDeath()
    {
        lifeCount--;
        Debug.Log("Life Count: " + lifeCount);
        if (playerController.deadAnimFinished == true)
            CreatePlayerorGameOver();
    }

    public void CreatePlayerorGameOver()
    {
        if (lifeCount >= 0)
        {
            float x = Random.Range(-9.0f, 9.0f);
            float y = -18.0f;
            playerPos = new Vector3(x, y, 0);
            GameObject player = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            playerController = player.GetComponent<PlayerController>();
            playerController.deadAnimFinished = false;
            playerController.respawned = true;
            UIManager.instance.BombCheck(playerController.Bomb);
            UIManager.instance.ShieldAmmoGaugeController(playerController);
        }
        else if (lifeCount < 0 && playerController.deadAnimFinished == true)
        {
            UIManager.instance.GameOver();
        }
    }

}
