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
		public ParticleSystem ThrusterEffectLeft;
		public ParticleSystem ThrusterEffectRight;

		public KeyCode GoLeftKey = KeyCode.LeftArrow;
		public KeyCode GoLeftKeySecondary = KeyCode.A;
		public KeyCode GoRightKey = KeyCode.RightArrow;
		public KeyCode GoRightKeySecondary = KeyCode.D;

		private Transform _targetTransform;
		private Vector3 _spawnPosition;
		private Quaternion _spawnRotation;

		private Rigidbody2D _rigidBody;

		private ParticleSystem.EmissionModule _thrustLeftEmission;
		private ParticleSystem.EmissionModule _thrustRightEmission;

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

			_thrustLeftEmission = ThrusterEffectLeft.emission;
			_thrustRightEmission = ThrusterEffectRight.emission;

			_thrustLeftEmission.enabled = false;
			_thrustRightEmission.enabled = false;
			ThrusterEffectLeft.Play();
			ThrusterEffectRight.Play();
		}

		void FixedUpdate()
		{
			bool goLeft		= Input.GetKey(GoLeftKey);
			bool goRight	= Input.GetKey(GoRightKey);
			goLeft			|= Input.GetKey(GoLeftKeySecondary);
			goRight			|= Input.GetKey(GoRightKeySecondary);


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

				goRight	|= averageX > originalScreenPos.x;
				goLeft	|= averageX < originalScreenPos.x;
			}


			bool includeChildren = true;

			if (goRight)
			{
				_rigidBody.AddForceAtPosition(new Vector2(MoveForce, 0), ThrusterRight.position);
			}
			if (goLeft)
			{
				_rigidBody.AddForceAtPosition(new Vector2(-MoveForce,0), ThrusterLeft.position);
			}

			_thrustLeftEmission.enabled = goRight;
			_thrustRightEmission.enabled = goLeft;



		}
	}
}