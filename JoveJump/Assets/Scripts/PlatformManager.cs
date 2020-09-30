// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

using System.Collections.Generic;

namespace CodeAnimo
{
	public class PlatformManager : MonoBehaviour
	{
		public int DesiredPlatformCount = 10;
		public float MinimumHeightDifference = 5;
		public float MaximumHeightDifference = 10;
		public GameObject PlatformPrefab;

		public Camera PlayerCamera;

		private List<GameObject> Platforms;

		private float _nextPlatformHeight;

		private void OnEnable()
		{
			if (Platforms == null)
			{
				Platforms = new List<GameObject>(DesiredPlatformCount);
			}

			_nextPlatformHeight = MinimumHeightDifference;

			for(int platformIndex = 0; platformIndex < DesiredPlatformCount; ++platformIndex)
			{
				GameObject platform = Instantiate(PlatformPrefab, transform);
				Platforms.Add(platform);

				PositionPlatform(platform.transform);

				
			}
		}

		private void OnDisable()
		{
			for (int platformIndex = 0; platformIndex < Platforms.Count; ++platformIndex)
			{
				Destroy(Platforms[platformIndex]);
			}
			Platforms.Clear();
		}


		private void FixedUpdate()
		{
			for (int platformIndex = 0; platformIndex < Platforms.Count; ++platformIndex)
			{
				GameObject platform = Platforms[platformIndex];
				if (PlayerCamera.WorldToViewportPoint(platform.transform.position).y <= 0)
				{
					PositionPlatform(platform.transform);
				}
			}
		}

		private void PositionPlatform(Transform platformTransform)
		{
			Vector3 position = Vector3.zero;
			position.x = Random.Range(0.0f, 1.0f);
			position = PlayerCamera.ViewportToWorldPoint(position);
			position.y = _nextPlatformHeight;
			position.z = 0;

			platformTransform.position = position;

			_nextPlatformHeight += Random.Range(MinimumHeightDifference, MaximumHeightDifference);
		}



	}
}