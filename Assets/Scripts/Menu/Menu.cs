using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Overlay")]
    [SerializeField] private Overlay overlay;

    [Header("Actions")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Toggle muteToggle;

    [Header("Mute")]
    [SerializeField] private Image muteToggleBackground;
    [SerializeField] private Image muteToggleIcon;
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;

    [Header("Music")]
    [SerializeField] private GameObject audioManager;
    [SerializeField] private AudioSource audioManagerSource;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void Awake()
    {
        // Setting fixed framerate.
        Application.targetFrameRate = 60;

        startGameButton.onClick.AddListener(StartFadeToGame);
        muteToggle.onValueChanged.AddListener(MuteGame);

        highscoreText.SetText("Highscore: " + PlayerPrefs.GetInt("highscore").ToString());

        Overlay.OnFadeInFinished += StartGame;

        DontDestroyOnLoad(audioManager);
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveAllListeners();
        muteToggle.onValueChanged.RemoveAllListeners();

        Overlay.OnFadeInFinished -= StartGame;
    }

    private void StartFadeToGame()
    {
        overlay.FadeIn(Color.black);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Mute button functionality. 
    private void MuteGame(bool isToggled)
    {
        switch (isToggled)
        {
            case true:
                muteToggleIcon.sprite = muteSprite;
                audioManagerSource.volume = 0.5f;
                break;

            case false:
                muteToggleIcon.sprite = unmuteSprite;
                audioManagerSource.volume = 0;
                break;
        }
    }
}