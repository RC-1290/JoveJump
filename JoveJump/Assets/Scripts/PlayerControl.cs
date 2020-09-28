// Copyright Code Animo™ (Laurens Mathot) © 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{

	public class PlayerControl : MonoBehaviour
	{
		public float MoveSpeed = 1;
		public Camera PlayerCamera;

		private Transform _targetTransform;

		private void OnEnable()
		{
			_targetTransform = GetComponent<Transform>();
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
				_targetTransform.position = PlayerCamera.ScreenToWorldPoint(new Vector3(averageX, originalScreenPos.y, originalScreenPos.z));
			}
		}
	}
}