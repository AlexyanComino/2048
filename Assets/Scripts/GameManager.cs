using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public TileBoard board;
    public CanvasGroup mainMenu;
    public CanvasGroup winMenu;
    public CanvasGroup gameOver;
    public CanvasGroup optionsMenu;
    public CanvasGroup pauseMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private int score;
    public bool isWin;
    public int themeIndex;
    public Color[][] Colors = new Color[3][]
    {
        new Color[] { new Color(0.9803922f, 0.972549f, 0.9372549f), new Color(0.6f, 0.6f, 0.6f), new Color(0.7f, 0.7f, 0.7f) },
        new Color[] { new Color(0.3427602f, 0.3427602f, 0.3427602f), new Color(0.6f, 0.6f, 0.6f), new Color(0.7f, 0.7f, 0.7f) },
        new Color[] { new Color(0.4588235f, 0.5176471f, 0.4039216f), new Color(0.6f, 0.6f, 0.6f), new Color(0.7f, 0.7f, 0.7f) }
    };

    private void Start()
    {
        NewGame();
        board.enabled = false;
    }

    public void StartGame()
    {
        mainMenu.alpha = 0f;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;

        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highscoreText.text = LoadHighscore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;
        winMenu.alpha = 0f;
        winMenu.interactable = false;
        winMenu.blocksRaycasts = false;
        optionsMenu.alpha = 0f;
        optionsMenu.interactable = false;
        optionsMenu.blocksRaycasts = false;
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void Continue()
    {
        winMenu.alpha = 0f;
        winMenu.interactable = false;
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        board.enabled = true;
    }

    public void Pause()
    {
        board.enabled = false;
        pauseMenu.alpha = 0.85f;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;
    }

    public void Win()
    {
        board.enabled = false;
        winMenu.interactable = true;
        winMenu.blocksRaycasts = true;

        StartCoroutine(Fade(winMenu, 1f, 0.5f));
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        gameOver.blocksRaycasts = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    public void OpenOptions()
    {
        mainMenu.alpha = 0f;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;

        optionsMenu.alpha = 1f;
        optionsMenu.interactable = true;
        optionsMenu.blocksRaycasts = true;
    }

    public void BackToMainMenu()
    {
        optionsMenu.alpha = 0f;
        optionsMenu.interactable = false;
        optionsMenu.blocksRaycasts = false;
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;

        mainMenu.alpha = 1f;
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;
    }

    public void SwitchTheme()
    {
        themeIndex = (themeIndex != 2) ? themeIndex + 1 : 0;
        mainCamera.backgroundColor = Colors[themeIndex][0]; 
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int amount)
    {
        SetScore(score + amount);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHighscore();
    }

    private void SaveHighscore()
    {
        int highscore = LoadHighscore();

        if (score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
        }
    }

    private int LoadHighscore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }
}
