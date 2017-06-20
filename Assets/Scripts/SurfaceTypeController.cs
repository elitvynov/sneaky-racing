namespace sneakyRacing
{
	using UnityEngine;

	using UnityStandardAssets.Vehicles.Car;

	public class SurfaceTypeController : MonoBehaviour
	{
		public enum SurfaceType
		{
			Mud,
			Grass,
			Sand,
		};
		
		[SerializeField]
		private CarController _car;

		[SerializeField]
		private WheelEffects _leftWheel;

		[SerializeField]
		private WheelEffects _rightWheel;

		private readonly float[] _tractionSurfaces = new float[3] { 0.25f, 0.65f, 0.85f };

		private ParticleSystem[] _leftSurfaces = new ParticleSystem[3];
		private ParticleSystem[] _rightSurfaces = new ParticleSystem[3];
		/*
		private SurfaceType _type = SurfaceType.Sand;

		public SurfaceType type
		{
			get
			{
				return _type;
			}
		}
		*/
		private void Awake()
		{
			_leftSurfaces[0] = _leftWheel.transform.Find("ParticlesMud").GetComponent<ParticleSystem>();
			_leftSurfaces[1] = _leftWheel.transform.Find("ParticlesGrass").GetComponent<ParticleSystem>();
			_leftSurfaces[2] = _leftWheel.transform.Find("ParticlesSand").GetComponent<ParticleSystem>();

			_rightSurfaces[0] = _rightWheel.transform.Find("ParticlesMud").GetComponent<ParticleSystem>();
			_rightSurfaces[1] = _rightWheel.transform.Find("ParticlesGrass").GetComponent<ParticleSystem>();
			_rightSurfaces[2] = _rightWheel.transform.Find("ParticlesSand").GetComponent<ParticleSystem>();
		}

		private void FixedUpdate()
		{
			RaycastHit hit;

			if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f, LayerMask.GetMask("TerrainMask")))
			{
				Renderer renderer = hit.transform.GetComponent<Renderer>();
				MeshCollider meshCollider = hit.collider as MeshCollider;

				if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
					return;

				Texture2D texture = renderer.material.mainTexture as Texture2D;
				Vector2 pixelUV = hit.textureCoord;
				pixelUV.x *= texture.width;
				pixelUV.y *= texture.height;

				//Debug.Log("hit.textureCoord = " + hit.textureCoord + ", pixelUV = " + pixelUV);

				Color color = texture.GetPixel((int)pixelUV.x, (int)pixelUV.y);

				float maxChannel = Mathf.Max(color[0], color[1], color[2]);

				for (int i = 0; i < 3; i++)
				{
					if (color[i] == maxChannel)
					{
						//_type = (SurfaceType)i;

						Debug.Log("_type = " + (SurfaceType)i);

						if (_leftWheel.skidParticles != _leftSurfaces[i])
						{
							//_leftWheel.skidParticles.Emit(0);

							_leftWheel.skidParticles = _leftSurfaces[i];
							//_leftWheel.skidParticles.gameObject.SetActive(true);
						}

						if (_rightWheel.skidParticles != _rightSurfaces[i])
						{
							//_rightWheel.skidParticles.Emit(0);

							_rightWheel.skidParticles = _rightSurfaces[i];
							//_rightWheel.skidParticles.gameObject.SetActive(true);
						}

						_car.tractionControl = _tractionSurfaces[i];

						//Debug.Log("_leftWheel.skidParticles = " + _leftWheel.skidParticles.name);
						//Debug.Log("_rightWheel.skidParticles = " + _rightWheel.skidParticles.name);

						break;
					}
				}
			}
		}
	}
}