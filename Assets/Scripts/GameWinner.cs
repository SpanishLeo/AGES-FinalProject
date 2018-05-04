using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinner : MonoBehaviour
{
    [SerializeField]
    private float respawnTimer = 5f;

    private int startingLives = 4;
    private static int player1LivesLeft;
    private static int player2LivesLeft;
    private static GameWinner instance;

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
        player1LivesLeft = startingLives;
        player2LivesLeft = startingLives;
    }

    private void AdjustPlayerLives(PlayerController playerThatDied)
    {
        if (playerThatDied.PlayerNumber == 1)
        {
            player1LivesLeft--;
        }
        else
        {
            player2LivesLeft--;
        }
    }

    private void CheckForGameOver()
    {
        int playerNumberThatWon;

        if (player1LivesLeft < 1)
        {
            playerNumberThatWon = 2;
            HandleGameOver(playerNumberThatWon);
        }
        else if (player2LivesLeft < 1)
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

        // DO whatever you want but you know which player number won.
        // SceneManager.LoadScene(EndScene);
        // YOur end scene can look GameWinner.PlayerNumberThatWon and do whatever w/ it
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
