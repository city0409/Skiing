﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{

    private int score;
    public int Score{get { return score; }set{score = value;}}
    private int scoreAward;
    public int ScoreAward { get { return scoreAward; } set { scoreAward = value; } }

    private void Start () 
	{
		
	}
	
	private void Update () 
	{
		
	}
}
