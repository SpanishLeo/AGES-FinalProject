using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinner : MonoBehaviour
{
    [SerializeField]
    private float respawnTimer = 5f;

    private int startingLives = 10;
    private static GameWinner instance;

    public static int Player1LivesLeft { get; private set; }
    public static int Player2LivesLeft { get; private set; }
    public static int PlayerNumberTheWon { get; private set; }

    //PUT IN TITLE SCENE on empty game object
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnPlayerDied(PlayerController playerThatDied)
    {
        AdjustPlayerLives(playerThatDied);

        CheckForGameOver();
    }

    private void OnStartingNewGame()
    {
        Player1LivesLeft = startingLives;
        Player2LivesLeft = startingLives;
    }

    private void AdjustPlayerLives(PlayerController playerThatDied)
    {
        if (playerThatDied.PlayerNumber == 1)
        {
            Player1LivesLeft--;
        }
        else
        {
            Player2LivesLeft--;
        }
    }

    private void CheckForGameOver()
    {
        int playerNumberThatWon;

        if (Player1LivesLeft < 1)
        {
            playerNumberThatWon = 2;
            HandleGameOver(playerNumberThatWon);
        }
        else if (Player2LivesLeft < 1)
        {
            playerNumberThatWon = 1;
            HandleGameOver(playerNumberThatWon);
        }
        else
        {
            Respawn();
        }
    }

    private void  HandleGameOver(int playerNumberThatWon)
    {
        PlayerNumberTheWon = playerNumberThatWon;

        GameOver();
    }

    private void Respawn()
    {
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(respawnTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {
        StartCoroutine(EndLevel());
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(respawnTimer);
        SceneManager.LoadScene("End Menu Scene");
    }

    private void OnEnable()
    {
        PlayerController.PlayerDied += OnPlayerDied;
        MainMenu.StartingANewGame += OnStartingNewGame;
    }

    private void OnDisable()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        MainMenu.StartingANewGame -= OnStartingNewGame;
    }
}
