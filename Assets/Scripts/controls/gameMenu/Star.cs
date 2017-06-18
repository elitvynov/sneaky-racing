namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class Star : MonoBehaviour
	{

		public bool isActive
		{
			set
			{
				GetComponent<Image>().enabled = value;
			}
		}
	}
}