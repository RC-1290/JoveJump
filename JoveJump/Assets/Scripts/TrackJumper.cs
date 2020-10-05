// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class TrackJumper : MonoBehaviour
	{
		public Score ScoreKeeper;
		public Transform DesiredPlatformLevel;
		public float MoveDuration = 1;
		public AnimationCurve MoveCurve;

		private float _trackedHeight = 0;
		private bool _movingToNewHeight = false;
		private float _moveStartTime = 0;
		private Vector3 _startPosition;
		private Vector3 _targetPosition;

		private Transform _transform;

		private Vector3 _spawnPosition;
		private Quaternion _spawnRotation;


		private void Awake()
		{
			_transform = GetComponent<Transform>();
			_spawnPosition = _transform.position;
			_spawnRotation = _transform.rotation;
		}

		private void OnEnable()
		{
			_transform.SetPositionAndRotation(_spawnPosition, _spawnRotation);

			_trackedHeight = 0;
			_movingToNewHeight = false;
			_moveStartTime = 0;
		}

		void Update()
		{
			if (ScoreKeeper.HeightReached > _trackedHeight)
			{
				_movingToNewHeight = true;
				_moveStartTime = Time.time;
				_startPosition = transform.position;
				_trackedHeight = ScoreKeeper.HeightReached;

				float offsetRequired = _trackedHeight - DesiredPlatformLevel.position.y;
				_targetPosition = new Vector3(_startPosition.x, _startPosition.y + offsetRequired, _startPosition.z);
			}

			if (_movingToNewHeight)
			{
				float moveProgress = (Time.time - _moveStartTime) / MoveDuration;
				float clampedProgress = Mathf.Clamp01(moveProgress);


				float curvedProgress = MoveCurve.Evaluate(clampedProgress);
				_transform.position = Vector3.LerpUnclamped(_startPosition, _targetPosition, curvedProgress);

				if (moveProgress >= 1.0)
				{
					_movingToNewHeight = false;

				}
			}
			
			
		}
	}
}