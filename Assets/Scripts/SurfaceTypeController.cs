namespace sneakyRacing
{
	using UnityEngine;

	public class SurfaceTypeController : MonoBehaviour
	{
		public enum SurfaceType
		{
			Mud,
			Grass,
			Sand,
		};

		private SurfaceType _type = SurfaceType.Sand;

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
						_type = (SurfaceType)i;

						break;
					}
				}

				//Debug.Log("color = " + color + ", _type = " + _type);
			}
		}

		private void OnDrawGizmos()
		{
			
		}
	}
}