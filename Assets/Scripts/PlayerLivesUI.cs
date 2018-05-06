using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesUI : MonoBehaviour
{
    [SerializeField]
    private Text player1LivesText;
    [SerializeField]
    private Text player2LivesText;
    [SerializeField]
    private Text roundOverText;

    private PlayerController playerController;

    void Start ()
    {

	}

	void Update ()
    {
        player1LivesText.text = "Player 1 Lives: " + GameWinner.Player1LivesLeft;
        player2LivesText.text = "Player 2 Lives: " + GameWinner.Player2LivesLeft;

        //if (playerController.IsAlive == false)
        //{
        //    roundOverText.enabled = true;
        //}
    }
}
