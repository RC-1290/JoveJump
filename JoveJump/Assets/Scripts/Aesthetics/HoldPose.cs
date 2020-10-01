// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class HoldPose : MonoBehaviour
	{
		public HingeJoint2D PosedJoint;

		public float DesiredAngle = 45;
		public float MotorSpeed = 100;

		private void Reset()
		{
			PosedJoint = GetComponent<HingeJoint2D>();
		}


		void Update()
		{
			var motor = PosedJoint.motor;

			if (PosedJoint.jointAngle < DesiredAngle)
			{
				motor.motorSpeed = MotorSpeed;
			}
			else if (PosedJoint.jointAngle > DesiredAngle)
			{
				motor.motorSpeed = -MotorSpeed;
			}
			PosedJoint.motor = motor;
			
		}
	}
}