namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class TrackItemRenderer : MonoBehaviour 
	{
		private RawImage _iconRawImage;

		public string iconName
		{
			set
			{
				_iconRawImage.texture = Resources.Load<Texture>("Gui/Tracks/" + value);
			}
		}

		private Text _trackText;

		public string trackName
		{
			set
			{
				_trackText.text = value;
			}
		}

		public int stars
		{
			set
			{
				Transform starsTransform = transform.Find("Stars");

				if (value > 0)
				{
					Star[] stars = starsTransform.GetComponentsInChildren<Star>();

					for (int i = 0; i < stars.Length; i++)
					{
						Star star = stars[i];
						star.isActive = (i < value);
					}
				}
				else
				{
					starsTransform.gameObject.SetActive(false);
				}
			}
		}

		private void Awake()
		{
			_iconRawImage = transform.Find("Icon").GetComponent<RawImage>();
			_trackText = transform.Find("Bar/Text").GetComponent<Text>();
		}
	}
}