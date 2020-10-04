// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	public class NoRotation : MonoBehaviour
	{
		private Transform _transform;

		private void OnEnable()
		{
			_transform = GetComponent<Transform>();
		}

		void Update()
		{
			_transform.rotation = Quaternion.identity;
		}
	}
}