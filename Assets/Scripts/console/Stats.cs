using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

/**
* Shows statistic menu.
* @author Eugene Litvynov
*/
public class Stats : MonoBehaviour
{
	public bool showCommonInfo = false;

	private Dictionary<string, Text> _dataList = new Dictionary<string, Text>();
	private Transform _itemRendererPrefab;

	private long tris = 0;
	private long verts = 0;

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
		
	private static Stats _instance = null;
		
	public static Stats instance 
	{
		get
		{
			if (_instance == null)
				throw new UnityException("Stats instance is not initialized.");
				
			return _instance;
		}
	}
		
	public void Awake()
	{
		if (_instance != null)
			throw new UnityException("Stats instance is already initialized.");
			
		_instance = this;

		_itemRendererPrefab = transform.Find("ItemRenderer");
		_itemRendererPrefab.gameObject.SetActive(false);
	}

	public static void setValue(string key, object text)
	{
		setValue(key, text, Color.white);
	}

	public static void setValue(string key, object text, Color color)
	{
		if (DEBUG)
		{
			instance.setItemData(key, key + ": " + text, color);
		}
	}

	public void setItemData(string key, object text, Color color)
	{
		if (_dataList.ContainsKey(key) == false)
		{
			GameObject itemRenderer = Instantiate(_itemRendererPrefab.gameObject) as GameObject;
			itemRenderer.transform.SetParent(instance.transform, false);
			itemRenderer.SetActive(true);
				
			_dataList[key] = itemRenderer.transform.Find("Text").GetComponent<Text>();
		}
			
		_dataList[key].text = text.ToString();
		_dataList[key].color = color;
	}
		
	public void clear()
	{
		_dataList.Clear();
	}
	/*
	private void showDevice()
	{
		setValue("Unity player version", Application.unityVersion);
		setValue("Graphics", SystemInfo.graphicsDeviceName + " " +
			SystemInfo.graphicsMemorySize + "MB\n" +
			SystemInfo.graphicsDeviceVersion + "\n" +
			SystemInfo.graphicsDeviceVendor);
		setValue("Shadows", SystemInfo.supportsShadows);
		setValue("Image Effects", SystemInfo.supportsImageEffects);
		setValue("Render Textures", SystemInfo.supportsRenderTextures);
	}
	/*
	void Qualities()
	{
		switch (QualitySettings.currentLevel) 
		{
			case QualityLevel.Fastest:
				GUILayout.Label("Fastest");
			break;
			case QualityLevel.Fast:
				GUILayout.Label("Fast");
			break;
			case QualityLevel.Simple:
				GUILayout.Label("Simple");
			break;
			case QualityLevel.Good:
				GUILayout.Label("Good");
			break;
			case QualityLevel.Beautiful:
				GUILayout.Label("Beautiful");
			break;
			case QualityLevel.Fantastic:
				GUILayout.Label("Fantastic");
			break;
		}
	}
		
	void QualityControl()
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease"))
		{
		QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase"))
		{
		QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}
		
	void StatControl()
	{
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		showtris = GUILayout.Toggle(showtris,"Triangles");
		showvtx = GUILayout.Toggle(showvtx,"Vertices");
		showfpsgraph = GUILayout.Toggle(showfpsgraph,"FPS Graph");
		GUILayout.EndHorizontal();
	}
	*/

	public void Update()
	{
		if (showCommonInfo)
		{
			getObjectStats();
				
			setValue("triangles", tris);
			setValue("vertices", verts);
		}
	}
		
	private void getObjectStats()
	{
		verts = 0;
		tris = 0;
			
		GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
			
		foreach (GameObject obj in ob)
		{
			getObjectStats(obj);
		}
	}
		
	private void getObjectStats(GameObject obj)
	{
		Component[] filters = obj.GetComponentsInChildren<MeshFilter>();
			
		foreach (MeshFilter f in filters)
		{
			tris += f.sharedMesh.triangles.Length / 3;
			verts += f.sharedMesh.vertexCount;
		}
	}
}
/*
internal class ItemRenderer 
{
	public string id;
	public int steps;
		
	public AchievementData(string id, int steps)
	{
		this.id = id;
		this.steps = steps;
	}
}
*/