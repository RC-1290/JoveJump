// Copyright Code Animo™ (Laurens Mathot) © 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{

	public class PlayerControl : MonoBehaviour
	{
		public float MoveSpeed = 1;
		public Camera PlayerCamera;

		public float MoveForce = 100;
		public Transform ThrusterLeft;
		public Transform ThrusterRight;

		private Transform _targetTransform;
		private Vector3 _spawnPosition;
		private Quaternion _spawnRotation;

		private Rigidbody2D _rigidBody;

		private void Awake()
		{
			_targetTransform = GetComponent<Transform>();
			_rigidBody = GetComponent<Rigidbody2D>();
			_spawnPosition = _targetTransform.position;
			_spawnRotation = _targetTransform.rotation;
		}

		private void OnEnable()
		{
			_targetTransform.SetPositionAndRotation(_spawnPosition, _spawnRotation);
		}

		void FixedUpdate()
		{
			if (Input.touchCount > 0)
			{
				float summedX = 0;


				for (int touchIndex = 0; touchIndex < Input.touchCount; ++touchIndex)
				{
					Touch touch = Input.GetTouch(touchIndex);

					summedX += touch.position.x;
				}

				float averageX = summedX / Input.touchCount;
				Vector3 originalPos = _targetTransform.position;
				Vector3 originalScreenPos = PlayerCamera.WorldToScreenPoint(originalPos);

				if (averageX > originalScreenPos.x)
				{
					_rigidBody.AddForceAtPosition(new Vector2(MoveForce, 0), ThrusterRight.position);
				}
				if (averageX < originalScreenPos.x)
				{
					_rigidBody.AddForceAtPosition(new Vector2(-MoveForce,0), ThrusterLeft.position);
				}
			}
		}
	}
}