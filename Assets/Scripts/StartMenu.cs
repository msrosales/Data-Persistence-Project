using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenu : MonoBehaviour
{
	public static StartMenu instance;
	public string playerName;
	public TMP_InputField inputPlayer;

	public string topPlayerName;
	public int topScore;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	public void Awake(){
		/*if (instance!=null){
		  Destroy(gameObject);
		  return;
		  }
		  instance = this;
		  DontDestroyOnLoad(gameObject);*/

		if (instance==null){
			instance = this;
			instance.topScore = 0;

			LoadTopScore();

			DontDestroyOnLoad(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartGame(){
		if (inputPlayer.text != ""){
			instance.playerName = inputPlayer.text;
			Debug.Log(instance.playerName);

			if (instance.topPlayerName == ""){
				instance.topPlayerName = instance.playerName;
				instance.topScore = 0;
			}

			SceneManager.LoadScene(1);
		}
	}

	[System.Serializable]
	class DataTopScore{
		public int topScore;
		public string topPlayerName;
	}

	public void SaveTopScore(){
		DataTopScore data = new DataTopScore();
		data.topScore = instance.topScore;
		data.topPlayerName = instance.topPlayerName;
		string json = JsonUtility.ToJson(data);
		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}
	
	public void LoadTopScore(){
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path)){
			string json = File.ReadAllText(path);
			DataTopScore data = JsonUtility.FromJson<DataTopScore>(json);
			instance.topPlayerName = data.topPlayerName;
			instance.topScore = data.topScore;
		}
	}

	public void QuitGame(){
		#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
		#else
			Application.Quit();
		#endif

	}
}
