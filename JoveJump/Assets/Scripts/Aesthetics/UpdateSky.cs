// Copyright Code Animo� (Laurens Mathot) � 2020, All rights reserved. Example project for 5th Planet Games
using UnityEngine;

namespace CodeAnimo
{
	[ExecuteInEditMode]
	public class UpdateSky : MonoBehaviour
	{
		[Header("Atmosphere Thickness")]
		public Transform Camera;
		public AnimationCurve CameraHeightToAtmosphereThickness;
		public float AtmosphereHeight = 100;

		[Header("Rotation")]
		public AnimationCurve CameraHeightToAtmsophereRotation;
		public float MinimumAtmosphereRotation = 33;
		public float MaximumAtmosphereRotation = 100;
		public float Maxrotationheight = 200;

		public Material SkyMaterial;

		private Transform _transform;

		private int _atmosphereThicknessID;
		private int _atmosphereRotationID;

		private void OnEnable()
		{
			_atmosphereThicknessID = Shader.PropertyToID("_AtmosphereThickness");
			_atmosphereRotationID = Shader.PropertyToID("_Rotation");

			_transform = GetComponent<Transform>();
		}


		void Update()
		{
			float atmosphereThickness = CameraHeightToAtmosphereThickness.Evaluate(Camera.position.y / AtmosphereHeight);

			SkyMaterial.SetFloat(_atmosphereThicknessID, atmosphereThickness);
			SkyMaterial.SetMatrix(_atmosphereRotationID, Matrix4x4.Rotate(_transform.rotation));

			float cameraRotation = CameraHeightToAtmsophereRotation.Evaluate(Camera.position.y / Maxrotationheight);
			_transform.localRotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(MinimumAtmosphereRotation, 0, 0)), Quaternion.Euler(MaximumAtmosphereRotation, 0, 0), cameraRotation);
		}
	}
}