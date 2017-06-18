namespace sneakyRacingEditor
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	using UnityEditor;
	using UnityEditor.SceneManagement;

	using sneakyRacing;

	[CustomEditor(typeof(CheckpointManager))]
	public class CheckpointManagerEditor : Editor
	{
		private CheckpointManager _checkpoints;

		private void OnEnable()
		{
			_checkpoints = target as CheckpointManager;
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Correct Checkpoins Names"))
			{
				_checkpoints.correctNames();

				Scene currentScene = EditorSceneManager.GetActiveScene();
				EditorSceneManager.MarkSceneDirty(currentScene);
			}
		}
	}
}