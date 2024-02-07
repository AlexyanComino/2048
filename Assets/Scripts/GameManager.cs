using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup mainMenu;
    public CanvasGroup winMenu;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private int score;
    public bool isWin;

    private void Start()
    {
        isWin = false;
        NewGame();
    }

    public void StartGame()
    {
        mainMenu.alpha = 0f;
        mainMenu.interactable = false;
        Destroy(mainMenu.gameObject);

        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highscoreText.text = LoadHighscore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        winMenu.alpha = 0f;
        winMenu.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void Continue()
    {
        winMenu.alpha = 0f;
        winMenu.interactable = false;
        board.enabled = true;
    }

    public void Win()
    {
        board.enabled = false;
        winMenu.interactable = true;

        StartCoroutine(Fade(winMenu, 1f, 0.5f));
    }

    public void GameOver()
    {
        board.enabled = false;  
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
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
