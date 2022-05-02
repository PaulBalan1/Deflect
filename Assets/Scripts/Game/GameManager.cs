using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Overlay")]
    [SerializeField] private Overlay overlay;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    public static int highscore;

    public void IncreaseScore()
    {
        score++;
        scoreText.SetText(score.ToString());
    }

    private void Awake()
    {
        score = 0;
        BlockSpawner.OnGameLost += FadeToBlack;
        Overlay.OnFadeInFinished += GoToMenu;
    }

    private void OnDestroy()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
        }

        BlockSpawner.OnGameLost -= FadeToBlack;
        Overlay.OnFadeInFinished -= GoToMenu;
    }

    private void Start()
    {
        overlay.SetColor(Color.black);
        overlay.FadeOut();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void FadeToBlack()
    {
        overlay.FadeIn(Color.black);
    }
}
