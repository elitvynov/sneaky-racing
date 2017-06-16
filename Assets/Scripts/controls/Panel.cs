namespace sneakyRacing
{
	using UnityEngine;

	public class Panel : MonoBehaviour 
	{
		protected InputInvalidator _inputInvalidator;

		public bool visible
		{
			get
			{
				return gameObject.activeSelf;
			}
			set
			{
				gameObject.SetActive(value);
			}
		}

		protected virtual void Awake()
		{
			_inputInvalidator = GetComponent<InputInvalidator>();
		}
	}
}