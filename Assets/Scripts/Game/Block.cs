using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer blockRenderer;
    [SerializeField] private TextMeshPro hitsRemainingText;
    [SerializeField] private AudioClip destroyedSoundClip;

    private int hitsRemaining;

    // Block outline color
    private Gradient blockColor;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;

    private GameManager gameManager;

    private void Awake()
    {
        blockColor = new Gradient();
        colorKey = new GradientColorKey[6];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.yellow;
        colorKey[1].time = 0.2f;
        colorKey[2].color = Color.green;
        colorKey[2].time = 0.4f;
        colorKey[3].color = Color.cyan;
        colorKey[3].time = 0.6f;
        colorKey[4].color = Color.blue;
        colorKey[4].time = 0.8f;
        colorKey[5].color = Color.red;
        colorKey[5].time = 1.0f;


        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        blockColor.SetKeys(colorKey, alphaKey);
        gameManager = FindObjectOfType<GameManager>();
    }

    internal void UpdateBlock(int hitsRemaining)
    {
        this.hitsRemaining = hitsRemaining;
        blockRenderer.color = blockColor.Evaluate((float)(hitsRemaining / 25.0));
        hitsRemainingText.SetText(hitsRemaining.ToString());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitsRemaining--;

        gameManager.IncreaseScore();

        if (hitsRemaining > 0) { 
            UpdateBlock(hitsRemaining);
        }
        else
        {
            Invoke(nameof(DestroyBlock), destroyedSoundClip.length);
            AudioSource.PlayClipAtPoint(destroyedSoundClip, Vector3.zero);
            gameObject.SetActive(false);
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }
}
