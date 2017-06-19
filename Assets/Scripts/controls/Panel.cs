namespace sneakyRacing
{
	using UnityEngine;

	public class Panel : MonoBehaviour 
	{
		protected InputInvalidator _inputInvalidator;
		protected ElasticRotation _elasticRotation;
		
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
		
		public virtual void show()
		{
			//Debug.Log("Panel:show()");

			if (gameObject.activeSelf == false)
				gameObject.SetActive(true);

			_elasticRotation.desirableRotation = 0.0f;
		}

		public virtual void hide()
		{
			//Debug.Log("Panel:hide()");

			_elasticRotation.desirableRotation = 90.0f;
		}

		protected virtual void Awake()
		{
			_inputInvalidator = GetComponent<InputInvalidator>();
			_elasticRotation = GetComponent<ElasticRotation>();
		}
	}
}