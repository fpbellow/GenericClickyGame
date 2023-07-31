using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<GameObject> targetProps;
    public GameObject rareProp;
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private AudioSource gameAudio;

    public bool isGameActive;
    public float spawnRate = 1.0f;

    private int scoreVal;
    private int missedTargets;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int rareChance = Random.Range(0, 65);
            int index = Random.Range(0, targetProps.Count);
            if (rareChance < 1) 
            {
                Instantiate(rareProp);
            }
            else
            {
                Instantiate(targetProps[index]);
            }
        }
    }

    public void playTargetSound(AudioClip tarSound)
    {
        gameAudio.PlayOneShot(tarSound, 0.45f);
    }
    public void UpdateScore(int scoreIncrement)
    {
        scoreVal += scoreIncrement;
        scoreText.text = "Score: " + scoreVal;
        if(scoreVal < 0)
        {
            GameOver();
        }
    }

    public void MissedTarget()
    {
        missedTargets++;
        if (missedTargets < 3)
        {
            missedText.text += "/";
        }else if(missedTargets == 3)
        {
            missedText.text += "/";
            GameOver();
        }
    }

    public void StartGame(float difficulty=1f)
    {
        spawnRate = difficulty;
        titleScreen.gameObject.SetActive(false);
        missedTargets = 0;
        missedText.text = "";
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        scoreVal = 0;
        UpdateScore(0);
    }

    void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        missedTargets = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
    }

}
