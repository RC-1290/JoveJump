﻿// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;
using UnityEngine.UI;

namespace CodeAnimo
{
	public class Score : MonoBehaviour
	{
		public AutomaticJump Jumper;
		public Text ScoreField;
		public Text HighscoreField;

		[Tooltip("An arbitrary multiplier to avoid decimal places, while still showing enough detail.")]
		public float Multiplier = 10;
		public float AnimationSpeed = 1;

		public string HighscoreKey = "Highscore";

		private float _currentScore = 0;
		private float _animatedScore = 0;
		private float _animatedHighscore = 0;
		private float _highscore = 0;

		private void OnEnable()
		{
			_animatedScore = 0;
			_currentScore = 0;
			ScoreField.text = "0";

			if (PlayerPrefs.HasKey(HighscoreKey))
			{
				_highscore = PlayerPrefs.GetFloat(HighscoreKey);
				_animatedHighscore = _highscore;// No need to animate the initial value.
				HighscoreField.text =_highscore.ToString();
			}
			else
			{
				HighscoreField.text = "0";
			}
		}

		private void OnDisable()
		{
			PlayerPrefs.SetFloat(HighscoreKey, _highscore);
		}


		private void Reset()
		{
			ScoreField = GetComponent<Text>();
		}

		void Update()
		{
			_currentScore = Mathf.Floor(Jumper.HeightReached * Multiplier);

			if (_currentScore > _highscore)
			{
				_highscore = _currentScore;
			}

			// Update UI:
			if (_animatedScore < _currentScore)
			{
				_animatedScore += AnimationSpeed;
				_animatedScore = _animatedScore > _currentScore ? _currentScore : _animatedScore;
				ScoreField.text = _animatedScore.ToString();
			}

			if (_animatedHighscore < _highscore)
			{
				_animatedHighscore += AnimationSpeed;
				_animatedHighscore = _animatedHighscore > _highscore ? _highscore : _animatedHighscore;
				HighscoreField.text = _animatedHighscore.ToString();
			}
		}
	}
}