namespace sneakyRacing
{
	using UnityEngine;
	using System.Collections.Generic;

	public class DataManager : MonoBehaviour
	{
		[SerializeField]
		private List<Track> _tracks;

		public static List<Track> GetCompaings()
		{
			DataManager dm = Resources.Load<DataManager>("Tracks");
			return dm._tracks;
		}
	}

	[System.Serializable]
	public class Track
	{
		public string name;
		public string sceneName;
		public string iconName;

		//public int cashPrize;
	}
}