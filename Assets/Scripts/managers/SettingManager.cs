namespace sneakyRacing
{
	using UnityEngine;

	using System.Collections.Generic;

	public class SettingManager
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

		private SettingManager()
		{
			_tracks = DataManager.GetCompaings();
		}

		public bool isInitialized = false;

		private List<Track> _tracks = new List<Track>();

		public static List<Track> tracks
		{
			get
			{
				return _instance._tracks;
			}
		}

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

		public void completeTrack(int stars)
		{
			// level replay
			if (_data.currentTrack < _data.stars.Count)
			{
				// update only if current result is better than old one
				if (_data.stars[_data.currentTrack] < stars)
					_data.stars[_data.currentTrack] = stars;
			}
			// saved data doesn't contain current level
			else
			{
				_data.stars.Add(stars);
			}
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
		// settings' data
		public string locale = "_en-en";
		public bool gameRated = false;
		public bool fxEnabled = false;
		public bool soundEnabled = true;
		public bool musicEnabled = true;

		// player's data
		public int coins = 0;
		public int currentTrack = 0;

		public List<int> stars = new List<int>();
	}
	/*
	[System.Serializable]
	public class TrackData
	{
		public int starCount = 0;
	}
	*/
}