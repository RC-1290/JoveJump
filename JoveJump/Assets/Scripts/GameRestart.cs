// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class GameRestart : MonoBehaviour
	{
		public Camera PlayerCamera;
		public GameObject Player;

		public GameObject World;

		private Transform _playerTransform;
		private Transform _cameraTransform;

		private void Awake()
		{
			_playerTransform = Player.transform;
			_cameraTransform = PlayerCamera.transform;
		}

		private void FixedUpdate()
		{
			if (PlayerCamera.WorldToViewportPoint(_playerTransform.position).y <= 0)
			{
				World.SetActive(false);
				World.SetActive(true);
			}
		}

	}
}