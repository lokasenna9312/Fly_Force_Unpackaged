using UnityEngine;
using Player;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerController playerController;
    public ShieldAmmoGaugeController shieldAmmoGaugeController;
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
        if (lifeCount >= 0) CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.isGameOver == true) return;
        if (lifeCount >= 0)
        {
            if (playerController == null && GameObject.FindWithTag("Player") == null)
            {
                Debug.Log("Miraculous Victory! Player respawned and game goes on.");
                CreatePlayer();
            }
        }
        else
        {
            if (playerController == null || playerController.deadAnimFinished == true)
            {
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
                GameObject[] bombs = GameObject.FindGameObjectsWithTag("BulletBomb");
                Debug.Log("Bullets on the scene: " + bullets.Length + ", Bombs on the scene: " + bombs.Length);
                if (bullets.Length == 0 && bombs.Length == 0)
                {
                    UIManager.instance.GameOver();
                }
            }
        }
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
        if (playerController.deadAnimFinished == true && lifeCount >= 0)
            CreatePlayer();
    }

    public void CreatePlayer()
    {
        {
            float x = Random.Range(-9.0f, 9.0f);
            float y = -18.0f;
            playerPos = new Vector3(x, y, 0);
            GameObject player = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            playerController = player.GetComponent<PlayerController>();
            playerController.ShieldAmmoForceSetter(0.0f);
            UIManager.instance.BombCheck(playerController.Bomb);
            UIManager.instance.ShieldAmmoGaugeController(playerController);
        }
    }
}