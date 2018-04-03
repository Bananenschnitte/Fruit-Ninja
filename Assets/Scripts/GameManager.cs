using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [Header("Score Elements")]
    public int score;
    public int highscore;
    public Text scoreText;
    public Text highscoreText;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;

    private AudioSource audioSource;

    private void Awake () {
        MakeSingleton();
        Advertisement.Initialize("999");
        audioSource = GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        GetHighscore();
    }

    /// <summary>
    /// Workaround for singleton-Behaviour
    /// </summary>
    private void MakeSingleton() {
        if (instance == null) {
            instance = this;
        }   else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);        
    }

    /// <summary>
    /// Reads the Highscore
    /// </summary>
    private void GetHighscore () {
		highscore = PlayerPrefs.GetInt("Highscore");
		highscoreText.text = "Best: " + highscore;
    }    

    /// <summary>
    /// Increases the current score
    /// </summary>
    /// <param name="points">The points to inscrease the current score with</param>
    public void IncreaseScore (int points) {
        score += points;
        scoreText.text = score.ToString();

        if(score > highscore){
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = score.ToString();
        }

    }

    /// <summary>
    /// Determines what happens when the bomb was hit --> Game ends (ad will be displayed)
    /// </summary>
    public void OnBombHit () {
        Advertisement.Show();
        Time.timeScale = 0;

        gameOverPanelScoreText.text = "Score: " + score.ToString();
        gameOverPanelHighScoreText.text = "Best: " + highscore.ToString();
        gameOverPanel.SetActive(true);

        Debug.Log("Bomb hit");
    }

    /// <summary>
    /// Restarts the Game
    /// </summary>
    public void RestartGame () {

        score = 0;
        scoreText.text = score.ToString();

        gameOverPanel.SetActive(false);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable")){
            Destroy(g);
        }

        Time.timeScale = 1;
    }

    /// <summary>
    /// Playes the slicing-sound
    /// </summary>
    public void PlayRandomSliceSound () {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }

}
