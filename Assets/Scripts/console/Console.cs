using UnityEngine;
using UnityEngine.UI;

/**
 * Decorator for Debug functionality.
 * @author Eugene Litvynov
 */
public class Console : MonoBehaviour
{
	private const int MaxLength = 4096;

	private static bool _debug = true;

	public static bool DEBUG
	{
		get
		{
			return _debug;
		}
		set
		{
			_debug = value;

			if (_instance)
				_instance.gameObject.SetActive(value);
		}
	}

	private static bool _handleLogMessages = false;

	public static bool handleLogMessages
	{
		get
		{
			return _handleLogMessages;
		}
		set
		{
			_handleLogMessages = value;

			if (value)
				Application.logMessageReceived += _instance.HandleLog;
			else
				Application.logMessageReceived -= _instance.HandleLog;
		}
	}

	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (_consoleText)
		{
			if (type == LogType.Error || type == LogType.Exception)
			{
				_consoleText.text += "<color=red>" + logString + "(" + stackTrace + ")" + "</color>" + "\n";
			}
			else
			{
				if (type == LogType.Warning)
					_consoleText.text += "<color=yellow>" + logString + "</color>" + "\n";
				else
					_consoleText.text += logString + "\n";
			}

			_consoleScrollRect.verticalNormalizedPosition = 0.0f;
		}
	}

	public static bool traceTimeStamp = true;

	private static Console _instance = null;
	
	public static Console instance
	{
		get
		{
			if (_instance != null)
				return _instance;
			
			throw new UnityException("Console instance is not initialized.");
		}
	}
	
	//private static Transform _transform;
	private static Transform _consoleTransform;

	private static ScrollRect _consoleScrollRect;
	private static Text _consoleText;
	
	private static Transform console
	{
		set
		{
			//_transform = value;

			_consoleTransform = value.Find("ConsolePanel");
			Transform consoleScrollTransform = _consoleTransform.Find("ScrollRect");
			Transform consoleTextTransform = consoleScrollTransform.Find("Text");

			_consoleScrollRect = consoleScrollTransform.GetComponent<ScrollRect>();
			_consoleText = consoleTextTransform.GetComponent<Text>();
		}
	}

	//private static bool _visible = false;

	public static bool visible
	{
		set
		{
			//_visible = value;

			if (DEBUG)
			{
				if (_consoleTransform)
				{
					_consoleTransform.gameObject.SetActive(value);
				}
			}
		}
	}

	private static System.Action<string[]> _commandCallback;

	public static void setExecuteCommandCallback(System.Action<string[]> commandCallback)
	{
		_commandCallback = commandCallback;
	}

	private static string getTimeStamp()
	{
		System.DateTime currentDateTime = System.DateTime.Now;
		
		return (currentDateTime.Minute < 10 ? "0" + currentDateTime.Minute : "" + currentDateTime.Minute) + ":" +
			(currentDateTime.Second < 10 ? "0" + currentDateTime.Second : "" + currentDateTime.Second) + ":" +
			currentDateTime.Millisecond + ": ";
	}

	public static void error(params object[] parameters)
	{
		if (DEBUG)
		{
			string result = "";
			
			if (traceTimeStamp)
				result += getTimeStamp();
			
			if (parameters.Length > 1)
			{
				for (int i = 0; i < parameters.Length; i++)
					result += parameters[i] + ", ";
			}
			else
				result += parameters[0];

			if (_consoleText && _handleLogMessages == false)
			{
				if (_consoleText.text.Length > MaxLength)
					_consoleText.text = "<color=red>" + result + "</color>" + "\n";
				else
					_consoleText.text += "<color=red>" + result + "</color>" + "\n";

				_consoleScrollRect.verticalNormalizedPosition = 0.0f;
			}
			
			Debug.LogError(result);
		}
	}

	public static void trace(params object[] parameters)
	{
		if (DEBUG)
		{
			string result = "";
			
			if (traceTimeStamp)
				result += getTimeStamp();

			if (parameters.Length > 1)
			{
				for (int i = 0; i < parameters.Length; i++)
					result += parameters[i] + ", ";
			}
			else
				result += parameters[0];

			if (_consoleText && _handleLogMessages == false)
			{
				if (_consoleText.text.Length > MaxLength)
					_consoleText.text = result + "\n";
				else
					_consoleText.text += result + "\n";

				_consoleScrollRect.verticalNormalizedPosition = 0.0f;
			}
			
			Debug.Log(result);
		}
	}
	
	public static void warning(params object[] parameters)
	{
		if (DEBUG)
		{
			string result = "";
			
			if (traceTimeStamp)
				result += getTimeStamp();
			
			if (parameters.Length > 1)
			{
				for (int i = 0; i < parameters.Length; i++)
					result += parameters[i] + ", ";
			}
			else
				result += parameters[0];

			if (_consoleText && _handleLogMessages == false)
			{
				if (_consoleText.text.Length > MaxLength)
					_consoleText.text = "<color=yellow>" + result + "</color>" + "\n";
				else
					_consoleText.text += "<color=yellow>" + result + "</color>" + "\n";

				_consoleScrollRect.verticalNormalizedPosition = 0.0f;
			}
			
			Debug.LogWarning(result);
		}
	}

	public bool swipeOpen = true;

	// if Console has a window and attached to GameObject - check swipe to open and close gesture
	private Vector2 fingerOneStart;
	private Vector2 fingerOneEnd;

	private Vector2 fingerTwoStart;
	private Vector2 fingerTwoEnd;

	public enum Movement
	{
		None,
		Left,
		Right, 
		Up,
		Down
	};

	private Movement _movementOne;
	private Movement _movementTwo;

	private InputField _inputField;

	public void clear()
	{
		_consoleText.text = "";
		_consoleScrollRect.verticalNormalizedPosition = 1.0f;
	}

	public void executeCommand()
	{
		if (_inputField.text != "")
		{
			trace(_inputField.text);

			string text = _inputField.text;

			if (DEBUG)
			{
				string[] commandArgs = text.Split(' ');
				commandArgs[0] = commandArgs[0].ToLower();

				_commandCallback(commandArgs);
			}

			_inputField.text = "";
		}
	}
	
	public void Awake()
	{
		if (_instance != null)
			throw new UnityException("Console instance is already initialized.");

		_instance = this;

		// disables only script, in other way if we disable console we can't use fps or stats,
		// in future let's made stats and fps parts of console and it another states, so Console.DEBUG will activate all elements
		// and then you can show or hide it components
		enabled = _debug;

		console = transform;

		Transform inputFieldTransform = _consoleTransform.Find("InputField");
		
		_inputField = inputFieldTransform.GetComponent<InputField>();
	}

	public void Update()
	{
        if (Input.GetKeyDown(KeyCode.C))
        {
            visible = true;
        }

        if (swipeOpen && Input.touches.Length == 2)
		{
			Touch touchOne = Input.touches[0];
			Touch touchTwo = Input.touches[1];

			if (touchOne.phase == TouchPhase.Began)
			{
				fingerOneStart = touchOne.position;
				fingerOneEnd = touchOne.position;
			}

			if (touchTwo.phase == TouchPhase.Began)
			{
				fingerTwoStart = touchOne.position;
				fingerTwoEnd = touchOne.position;
			}

			if (touchOne.phase == TouchPhase.Moved)
			{
				fingerOneEnd = touchOne.position;

				if (Mathf.Abs(fingerOneStart.x - fingerOneEnd.x) > Mathf.Abs(fingerOneStart.y - fingerOneEnd.y))
				{
					if ((fingerOneEnd.x - fingerOneStart.x) > 0)
						_movementOne = Movement.Right;
					else
						_movementOne = Movement.Left;
				}
				else
				{
					if ((fingerOneEnd.y - fingerOneStart.y) > 0)
						_movementOne = Movement.Up;
					else
						_movementOne = Movement.Down;
				}

				fingerOneStart = touchOne.position;

				//trace("_movementOne = " + _movementOne.ToString());
			}

			if (touchTwo.phase == TouchPhase.Moved)
			{
				fingerTwoEnd = touchTwo.position;
				
				if (Mathf.Abs(fingerTwoStart.x - fingerTwoEnd.x) > Mathf.Abs(fingerTwoStart.y - fingerTwoEnd.y))
				{
					if ((fingerTwoEnd.x - fingerTwoStart.x) > 0)
						_movementTwo = Movement.Right;
					else
						_movementTwo = Movement.Left;
				}
				else
				{
					if ((fingerTwoEnd.y - fingerTwoStart.y) > 0)
						_movementTwo = Movement.Up;
					else
						_movementTwo = Movement.Down;
				}
				
				fingerTwoStart = touchTwo.position;
				
				//trace("_movementTwo = " + _movementTwo.ToString());
			}

			if (touchOne.phase == TouchPhase.Ended)
			{
				fingerOneStart = Vector2.zero;
				fingerOneEnd = Vector2.zero;

				_movementOne = Movement.None;
			}

			if (touchTwo.phase == TouchPhase.Ended)
			{
				fingerTwoStart = Vector2.zero;
				fingerTwoEnd = Vector2.zero;
				
				_movementTwo = Movement.None;
			}

			if (_movementOne == Movement.Down && _movementTwo == Movement.Down)
			{
				visible = true;
			}
			else
			{
				if (_movementOne == Movement.Up && _movementTwo == Movement.Up)
					visible = false;
			}

           
            
		}
	}
}