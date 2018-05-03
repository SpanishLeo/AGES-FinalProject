using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinner : MonoBehaviour
{
    public static event Action<int> PlayerLivesLeftChanged;

    private static int playerLivesLeft;

    public static int LivesLeft
    {
        get { return playerLivesLeft; }
        private set
        {
            playerLivesLeft = value;
            if (PlayerLivesLeftChanged != null)
            {
                PlayerLivesLeftChanged.Invoke(playerLivesLeft);
            }
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
