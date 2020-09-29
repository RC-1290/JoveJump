// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class AutomaticJump : MonoBehaviour
	{
		[Range(0,1)]
		public float MaxGroundDistance = 0.1f;
		public LayerMask GroundHitLayers;
		public Transform GroundDetectionPoint;

		public float TimeBeforeJump = 0.5f;
		public Vector2 JumpForce;

		public float _lastTimeAirborne = 0;
		private Rigidbody2D _ownRigidBody;

		private void OnEnable()
		{
			_ownRigidBody = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			RaycastHit2D hit = Physics2D.Raycast(GroundDetectionPoint.position, Vector2.down, MaxGroundDistance, GroundHitLayers.value);
			
			if (hit.collider != null)
			{
				if (Time.fixedTime - _lastTimeAirborne > TimeBeforeJump)
				{
					_ownRigidBody.AddForce(JumpForce);
				}
			}
			else
			{
				_lastTimeAirborne = Time.fixedTime;
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawLine(GroundDetectionPoint.position, new Vector3(GroundDetectionPoint.position.x, GroundDetectionPoint.position.y - MaxGroundDistance, GroundDetectionPoint.position.z));
		}
	}
}