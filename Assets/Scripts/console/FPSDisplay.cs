using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;

	private int lowFPS;
	private int highFPS;
		
	private int _fps;
	private int _fpsPrev;
	private float _timeSinceLevelLoad;
		
	private Text _frameCounterText;
		
	public static bool DEBUG
	{
		get
		{
			return (_instance != null && instance.gameObject.activeSelf);
		}
		set
		{
			if (_instance)
				instance.gameObject.SetActive(value);
		}
	}
		
	private static FPSDisplay _instance = null;
		
	public static FPSDisplay instance 
	{
		get
		{
			if (_instance != null)
				return _instance;
				
			throw new UnityException("FPSDisplay instance is not initialized.");
		}
	}

	private int _qty = -5;
	private float _currentAvgFPS = 0.0f;

	private void updateCumulativeMovingAverageFPS(float newFPS)
	{
		_qty++;

		if (_qty > 0)
			_currentAvgFPS += (newFPS - _currentAvgFPS) / _qty;
	}

	public void Awake()
	{
		if (_instance != null)
			throw new UnityException("FPSDisplay instance is already initialized.");

		_instance = this;
	}

	public void Start()
	{
		_timeSinceLevelLoad = Time.timeSinceLevelLoad;

		highFPS = Application.targetFrameRate;
		lowFPS = (int)(Application.targetFrameRate * 0.5f);

		_fps = highFPS;
		_fpsPrev = highFPS;

		_frameCounterText = transform.GetComponent<Text>();
	}

	public void Update()
	{
		if (Time.timeSinceLevelLoad - _timeSinceLevelLoad <= 1.0f)
		{
			_fps++;
		}
		else
		{
			_fpsPrev = _fps + 1;
			_timeSinceLevelLoad = Time.timeSinceLevelLoad;
			_fps = 0;

			highFPS = Application.targetFrameRate;
			lowFPS = (int)(Application.targetFrameRate * 0.5f);

			updateCumulativeMovingAverageFPS(_fpsPrev);

			_frameCounterText.color = Color.Lerp(lowFPSColor, highFPSColor, (_fpsPrev - lowFPS) / (highFPS - lowFPS));
			_frameCounterText.text = "" + _fpsPrev + "/" + Mathf.RoundToInt(_currentAvgFPS);
		}
	}
}