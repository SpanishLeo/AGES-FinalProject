using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesUI : MonoBehaviour
{
    [SerializeField]
    private int playerDeathsToWin = 3;

    private Text text; 

	void Start ()
    {
        text = GetComponent<Text>();
	}

	void Update ()
    {
		
	}
}
