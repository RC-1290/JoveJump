// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class GameRestart : MonoBehaviour
	{
		public Camera PlayerCamera;
		public GameObject Player;
		public Transform PlayerSpawnPoint;

		private Transform _playerTransform;

		private void OnEnable()
		{
			_playerTransform = Player.transform;

			ResetPlayer();
		}

		private void FixedUpdate()
		{
			if (PlayerCamera.WorldToViewportPoint(_playerTransform.position).y <= 0)
			{
				ResetPlayer();
			}
		}

		private void ResetPlayer()
		{
			Player.SetActive(false);
			
			_playerTransform.SetPositionAndRotation(PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
			Rigidbody2D playerRigidBody =  Player.GetComponent<Rigidbody2D>();
			playerRigidBody.angularVelocity = 0;
			playerRigidBody.velocity = Vector2.zero;

			Player.SetActive(true);
		}


	}
}