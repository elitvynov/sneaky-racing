namespace sneakyRacing
{
	using UnityEngine;
	using UnityEngine.UI;

	public class NotificationToast : MonoBehaviour 
	{
		private Text _labelText;

		private float _showTime = -1.0f;

		public bool isVisible
		{
			get
			{
				return gameObject.activeSelf;
			}
		}

		public void show(string label)
		{
			gameObject.SetActive(true);

			_labelText.text = "<color=#FFFFFF>" + label + "</color>";
			_showTime = 2.5f;
		}

		public void hide()
		{
			gameObject.SetActive(false);
		}

		public void Awake()
		{
			_labelText = transform.Find("Label").GetComponent<Text>();
		}

		public void Start()
		{
			
		}
		
		public void Update()
		{
			if (_showTime > 0.0f)
			{
				_showTime = Mathf.Clamp(_showTime - Time.deltaTime, 0.0f, float.MaxValue);

				if (_showTime == 0.0f)
					hide();
			}
			/*
			if (_type == Type.Alert)
			{
				_labelText.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
			}
			*/
		}
	}
}