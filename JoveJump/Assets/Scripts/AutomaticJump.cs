// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class AutomaticJump : MonoBehaviour
	{
		public float TimeBeforeJump = 0.5f;
		public Vector2 JumpForce;

		public bool _landedSinceJump = false;
		public float _collisionStartTime = 0;
		private Rigidbody2D _ownRigidBody;


		private void OnEnable()
		{
			_ownRigidBody = GetComponent<Rigidbody2D>();
			_landedSinceJump = false;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			_landedSinceJump = true;
			_collisionStartTime = Time.fixedTime;
		}

		private void FixedUpdate()
		{
			if (_landedSinceJump)
			{
				if (Time.fixedTime - _collisionStartTime > TimeBeforeJump)
				{
					_landedSinceJump = false;
					_ownRigidBody.AddForce(JumpForce);
				}
			}
		}
	}
}