namespace sneakyRacing
{
	using UnityEngine;
	
	public class SettingManager : MonoBehaviour
	{
		private static SettingManager _instance = null;

		public static SettingManager instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingManager();

				return _instance;
			}
		}

		private SettingManager() {}

		public bool isInitialized = false;

		private SettingData _data = new SettingData();

		public static SettingData data
		{
			get
			{
				return _instance._data;
			}
		}

		public void resetData()
		{
			_data = new SettingData();
		}
		
		/// <summary>
		/// Checks if it's time to charge battery.
		/// </summary>
		public void flush()
		{
			if (isInitialized == false)
				return;

			string data = JsonUtility.ToJson(_data);

			PlayerPrefs.SetString("data", data);
		}
		
		public void init()
		{
			if (isInitialized)
				return;

			// first time launch: defaults
			if (PlayerPrefs.HasKey("data") == false)
			{
				flush();
			}
			// restore saved data
			else
			{
				restore();
			}
			
			isInitialized = true; 
		}

		private void restore()
		{
			if (PlayerPrefs.HasKey("data"))
			{
				string data = PlayerPrefs.GetString("data");

				_data = JsonUtility.FromJson<SettingData>(data);
			}
		}
	}

	[System.Serializable]
	public class SettingData
	{
		public enum TutorialSlide
		{
			VehiclePurchase,
			RaceStart,
			InputPractise,
			NitroPractise,
			CrashPractise,
			RepairPractise,
			BoosterPractise,
			RaceFinish,
			VehicleUpgrade,
		};
		
		// settings' data
		public string locale = "_en-en";
		public bool gameRated = false;
		public bool fxEnabled = false;
		public bool soundEnabled = true;
		public bool musicEnabled = true;

		// player's data
		public int coins = 0;

		// player's statistical data
		public int coinsOverall = 0;			// how many coins was earned / bought (from install)
		public int beadsOverall = 0;			// how many beads was earned / bought (from install)
		public int gameSessionOverall = 0;      // how many game sessions was played (from install)
		public int gameSessionCount = 0;        // how many game sessions was played (from launch)
		
		// tutorial's data
		//public bool[] tutorialSlides = new bool[TutorialSlideCount];
		public bool tutorialVehiclePurchase = false;
		public bool tutorialVehicleUpgrade = false;
		public bool tutorialRaceFinish = false;
	}
}