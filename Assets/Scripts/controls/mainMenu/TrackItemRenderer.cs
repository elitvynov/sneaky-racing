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
				Texture im = Resources.Load<Texture>("Gui/Tracks/" + value);

				_iconRawImage.texture = im;
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
		
		private void Awake()
		{
			_iconRawImage = transform.Find("Icon").GetComponent<RawImage>();
			_trackText = transform.Find("Bar/Text").GetComponent<Text>();
		}
	}
}