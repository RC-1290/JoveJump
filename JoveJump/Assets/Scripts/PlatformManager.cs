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

		public bool LimitHorizontalDifference = false;
		public float MaximumHorizontalDifference = 3;
		
		public GameObject PlatformPrefab;
		public Camera PlayerCamera;
		private List<GameObject> Platforms;

		private float _nextPlatformX;
		private float _nextPlatformY;

		private void OnEnable()
		{
			if (Platforms == null)
			{
				Platforms = new List<GameObject>(DesiredPlatformCount);
			}

			_nextPlatformY = MinimumHeightDifference;
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
			while (Platforms.Count < DesiredPlatformCount)
			{
				GameObject platform = Instantiate(PlatformPrefab, transform);
				Platforms.Add(platform);

				PositionPlatform(platform.transform);
				EnsureItHasAJumpCounter(platform);
			}


			for (int platformIndex = 0; platformIndex < Platforms.Count; ++platformIndex)
			{
				GameObject platform = Platforms[platformIndex];
				if (PlayerCamera.WorldToViewportPoint(platform.transform.position).y <= 0)
				{
					if (Platforms.Count > DesiredPlatformCount)
					{
						Platforms.Remove(platform);
						Destroy(platform);
					}
					else
					{
						PositionPlatform(platform.transform);
						EnsureItHasAJumpCounter(platform);
					}
				}
			}
		}

		private void EnsureItHasAJumpCounter(GameObject platform)
		{
			if (platform.GetComponent<PlatformJumpCounter>() == null)
			{
				platform.AddComponent<PlatformJumpCounter>();
			}
		}

		private void PositionPlatform(Transform platformTransform)
		{
			Vector3 position = Vector3.zero;
			position.x = _nextPlatformX;
			position.y = _nextPlatformY;
			position.z = 0;

			platformTransform.position = position;

			_nextPlatformY += Random.Range(MinimumHeightDifference, MaximumHeightDifference);

			// X coordinate takes viewport size into account:
			_nextPlatformX = Random.Range(0.0f, 1.0f);
			Vector3 viewportPosition = new Vector3(_nextPlatformX, 0, 0);
			_nextPlatformX = PlayerCamera.ViewportToWorldPoint(viewportPosition).x;

			// Limit distance to support uncommon aspect ratios:
			if (LimitHorizontalDifference)
			{
				float distanceFromCurrentPlatform = _nextPlatformX - position.x;
				if (Mathf.Abs(distanceFromCurrentPlatform) > MaximumHorizontalDifference)
				{
					_nextPlatformX = position.x + Mathf.Sign(distanceFromCurrentPlatform) * MaximumHorizontalDifference;
				}
			}
			
		}



	}
}