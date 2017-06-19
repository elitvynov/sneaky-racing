namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class InstrumentPanel : MonoBehaviour 
	{
		private Text _checkpointText;

		public int checkpoints
		{
			set
			{
				_checkpointText.text = value.ToString();
			}
		}

		private Text _timeText;

		public float time
		{
			set
			{
				_timeText.text = value.ToString("0.00");
			}
		}

		private void Awake()
		{
			_checkpointText = transform.Find("Checkpoints/Text").GetComponent<Text>();
			_timeText = transform.Find("Time/Text").GetComponent<Text>();
		}
	}
}