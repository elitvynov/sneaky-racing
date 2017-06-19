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
			Debug.Log("Game data is reset to defaults.");

			_data = new SettingData();

			isInitialized = true;
		}

		public void completeTrack(int stars, float time)
		{
			Debug.Log("completeTrack(): stars=" + stars + ", time=" + time);
			Debug.Log("_data.currentTrack=" + _data.currentTrack);

			// level replay
			if (_data.currentTrack < _data.trackList.Count)
			{
				TrackData trackData = _data.trackList[_data.currentTrack];

				Debug.Log("trackData.starCount: " + trackData.starCount);

				// update only if current result is better than old one
				if (trackData.starCount < stars)
					trackData.starCount = stars;

				if (trackData.bestTime == 0.0f || trackData.bestTime > time)
					trackData.bestTime = time;

				Debug.Log("trackData.starCount: " + trackData.starCount);
			}
			// saved data doesn't contain current level
			else
			{
				TrackData trackData = new TrackData(stars, time);

				Debug.Log("save new data: " + stars);

				_data.trackList.Add(trackData);
			}
		}

		public void setReplay(List<Dictionary<string, ObjectState>> objectStateList, float time)
		{
			TrackData trackData = _data.trackList[_data.currentTrack];

			Debug.LogWarning("trackData.bestTime = " + trackData.bestTime);
			Debug.LogWarning("time = " + time);

			if (trackData.bestTime == 0.0f || trackData.bestTime > time)
				trackData.objectStateList = objectStateList;

			Debug.LogWarning("_data.currentTrack = " + _data.currentTrack + ", trackData.objectStateList = " + trackData.objectStateList);
		}

		/// <summary>
		/// Checks if it's time to charge battery.
		/// </summary>
		public void flush()
		{
			if (isInitialized == false)
				return;

			string data = JsonUtility.ToJson(_data);

			Debug.Log("flush(): " + data);

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

				Debug.Log("restore(): " + data);

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
		//public int availableTrack = 0;

		// by default, we have 1 opened track
		public List<TrackData> trackList = new List<TrackData>(new TrackData[] { new TrackData() });
	}
	
	[System.Serializable]
	public class TrackData
	{
		public int starCount = 0;
		public float bestTime = 0.0f;

		public bool isPlayed
		{
			get
			{
				return (bestTime > 0.0f);
			}
		}

		[System.NonSerialized]
		public List<Dictionary<string, ObjectState>> objectStateList;

		public TrackData() { }

		public TrackData(int starCount, float bestTime)
		{
			this.starCount = starCount;
			this.bestTime = bestTime;
		}
	}
}