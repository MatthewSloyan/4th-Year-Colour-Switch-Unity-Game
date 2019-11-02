﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    #region == Private Variables ==

    [SerializeField]
    private Text scoreText;

    private int playerScore = 0;
    public int PlayerScore { get { return playerScore; } }
    #endregion


    // Singleton design pattern to get instance of class in PlayerCollider.cs
    public static ScoreController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    // subscribe to an star collected event and add the score
    private void OnEnable()
    {
        // subscribe
        PlayerCollider.StarCollectedEvent += HandleStarCollectedEvent;
    }

    private void OnDisable()
    {
        // unsubscribe
        PlayerCollider.StarCollectedEvent -= HandleStarCollectedEvent;
    }

    private void HandleStarCollectedEvent(PlayerCollider pc)
    {
        // add to the score
        playerScore += pc.ScoreValue;
        Debug.Log("Score: " + playerScore);

        // Set the onscreen score.
        scoreText.text = playerScore.ToString();
        
        // Check if score has been saved before.
        if (PlayerPrefs.HasKey("Score"))
        {
            //Debug.Log("HighScore: " + PlayerPrefs.GetInt("Score"));

            if (playerScore >= PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", playerScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        
        //PlayerPrefs.DeleteAll();
    }
}
