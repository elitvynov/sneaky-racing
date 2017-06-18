namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class InstrumentPanel : MonoBehaviour 
	{
		private Transform _pauseTransform;

		public GameObject pauseButton
		{
			get
			{
				return _pauseTransform.gameObject;
			}
		}

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
			_pauseTransform = transform.Find("PauseButton");

			_checkpointText = transform.Find("Checkpoints/Text").GetComponent<Text>();
			_timeText = transform.Find("Time/Text").GetComponent<Text>();
		}

		private void Start()
		{
			//ammo = 0;
			//distance = 0;
			//energy = 0.0f;
			//coins = SettingManager.data.coins;
		}

		private void Update()
		{
			
		}
	}
}