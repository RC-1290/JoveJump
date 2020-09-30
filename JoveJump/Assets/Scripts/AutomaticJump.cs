// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class AutomaticJump : MonoBehaviour
	{
		[Range(0,1)]
		public float MaxGroundDistance = 0.1f;
		public float MaxmimumUpwardsSpeedBeforeLaunch = 0.1f;
		public LayerMask GroundHitLayers;
		public Transform GroundDetectionPoint;

		public float TimeBeforeJump = 0.5f;
		public Vector2 JumpForce;
		public float ThrustDuration = .034f;

		public float HeightReached = 0;

		private float _lastTimeAirborne = 0;
		private Rigidbody2D _ownRigidBody;
		private bool _performingThrust = false;
		private float _thrustStartTime = 0;

		private void OnEnable()
		{
			_ownRigidBody = GetComponent<Rigidbody2D>();
			HeightReached = 0;
			_lastTimeAirborne = 0;
			_performingThrust = false;
		}

		private void FixedUpdate()
		{
			if (!_performingThrust)
			{
				// Check if we've landed:
				RaycastHit2D hit = Physics2D.Raycast(GroundDetectionPoint.position, Vector2.down, MaxGroundDistance, GroundHitLayers.value);
				bool weHitSomething = hit.collider != null;
				bool notGoingUp = _ownRigidBody.velocity.y <= MaxmimumUpwardsSpeedBeforeLaunch;

				if (weHitSomething && notGoingUp)
				{
					float platformHeight = hit.point.y;
					if (platformHeight > HeightReached)
					{
						HeightReached = platformHeight;
					}

					if (Time.fixedTime - _lastTimeAirborne > TimeBeforeJump)
					{
						_performingThrust = true;
						_thrustStartTime = Time.fixedTime;
					}
				}
				else
				{
					_lastTimeAirborne = Time.fixedTime;
				}
			}

			if (_performingThrust)
			{
				if (Time.fixedTime < _thrustStartTime + ThrustDuration)
				{
					_ownRigidBody.AddForce(JumpForce, ForceMode2D.Force);
				}
				else
				{
					_performingThrust = false;
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawLine(GroundDetectionPoint.position, new Vector3(GroundDetectionPoint.position.x, GroundDetectionPoint.position.y - MaxGroundDistance, GroundDetectionPoint.position.z));
		}
	}
}