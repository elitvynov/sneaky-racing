namespace sneakyRacingEditor
{
	using UnityEditor;
	using UnityEngine;

	using sneakyRacing;

	public class ResetSaves : EditorWindow
	{

		[MenuItem("Sneaky Racing/Reset Saved Data %#r")]
		public static void reset()
		{
			SettingManager.instance.resetData();
			SettingManager.instance.flush();
		}
	}
}