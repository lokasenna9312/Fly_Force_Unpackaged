using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] bombs;
    public GameObject shieldAmmoGauge;
    public ShieldAmmoGaugeController shieldGaugeController;
    public BossController currentBoss { get; private set; }

    // 스테이지
    [SerializeField] private Text stageNoText;
    public int stageNo { get; private set; }

    // 점수
    [SerializeField] private Text scoreText;
    public int score { get; private set; }
    [SerializeField] private Text highScoreText; 
    public int highScore { get; private set; }

    // 목숨
    public GameObject[] life;
    public bool isGameOver { get; private set; }

    // 암막
    public Image BlackOutCurtain;
    float BlackOutCurtain_value;
    float BlackOutCurtain_speed;

    // 게임오버
    public Image GameOverImage;

    // 보스
    public Image hpBarFrame;
    public Image hpBar1;
    public Image hpBar2;
    public int MaxHp1 { get; private set; }
    public int MaxHp2 { get; private set; }

    public void Awake()
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

    public void Start()
    {
        stageNo = 1;
        stageNoText.text = stageNo.ToString();
        score = 0;
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        BlackOutCurtain_value = 1.0f;
        BlackOutCurtain_speed = 0.5f;

        if (shieldAmmoGauge != null)
        {
            shieldGaugeController = shieldAmmoGauge.GetComponent<ShieldAmmoGaugeController>();
        }
    }

    public void Update()
    {
        if (BlackOutCurtain_value > 0.0f)
        {
            HideBlackOutCurtain();  
        }
        if (currentBoss != null)
        { 
            BossHpBarCheck();
        }
        if (isGameOver)
        {
            if (Input.GetButton("Bomb") && Input.GetButton("Shield"))
            {
                ResetHighScore();
            }
        }
    }

    public void BombCheck(int bombCount)
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            if(i < bombCount)
            {
                bombs[i].SetActive(true);
            }
            else
            {
                bombs[i].SetActive(false);
            }
        }
    }

    public void ShieldAmmoGaugeController(PlayerController pc)
    {
        if (shieldGaugeController != null)
        {
            shieldAmmoGauge.SetActive(true);
            shieldGaugeController.SetPlayerController(pc);
        }
    }

    public void LifeCheck(int lifeCount)
    {
        for (int i = 0; i < life.Length; i++)
        {
            if (i < lifeCount)
            {
                life[i].SetActive(true);
            }
            else
            {
                life[i].SetActive(false);
            }
        }
    }

    public void BossIsPresent(BossController boss)
    {
        currentBoss = boss;
        Debug.Log("Boss is present!");
        MaxHp1 = boss.hp1;
        MaxHp2 = boss.hp2;
    }

    public void BossHpBarCheck()
    {
        if (currentBoss == null) return;

        hpBarFrame.gameObject.SetActive(true);
        hpBar1.gameObject.SetActive(true);
        hpBar2.gameObject.SetActive(true);

        hpBar1.fillAmount = (float) currentBoss.hp1 / MaxHp1;
        hpBar2.fillAmount = (float) currentBoss.hp2 / MaxHp2;
    }

    public void TurnOffBossInterface()
    {
        currentBoss = null;
        hpBarFrame.gameObject.SetActive(false);
        hpBar1.gameObject.SetActive(false);
        hpBar2.gameObject.SetActive(false);
        Debug.Log("Boss Interface has turned off.");
    }

    public void AddStageNo()
    {
        stageNo++;
        stageNoText.text = stageNo.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
        if (score >= highScore)
            scoreText.color = Color.yellow;
    }

    public void HideBlackOutCurtain()
    {
        BlackOutCurtain_value -= Time.deltaTime * BlackOutCurtain_speed;
        Debug.Log($"Curtain Value: {BlackOutCurtain_value}");
        BlackOutCurtain.color = new Color(0.0f, 0.0f, 0.0f, BlackOutCurtain_value);
        if (BlackOutCurtain_value <= 0.0f)
            BlackOutCurtain.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver == true) return;
        else isGameOver = true;

        GameOverImage.gameObject.SetActive(true);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        RefreshScores();
    }

    private void ResetHighScore()
    {
        score = 0;
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", 0);
        RefreshScores();
        Debug.Log("High Score Reset!");
    }

    private void RefreshScores()
    { 
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void ReturnTitle()
    {
        isGameOver = false;
        SceneManager.LoadScene("Title");
        Destroy(UIManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(SoundManager.instance.gameObject);
        Destroy(EnemySpawnController.instance.gameObject);
    }
}
