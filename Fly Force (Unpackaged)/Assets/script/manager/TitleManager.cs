using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance;

    public Text highScoreText;
    public int highScore;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreText.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Bomb") && Input.GetButton("Shield"))
        {
            highScore = 0;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreText.text = highScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Destroy(TitleManager.instance.gameObject);
    }
}