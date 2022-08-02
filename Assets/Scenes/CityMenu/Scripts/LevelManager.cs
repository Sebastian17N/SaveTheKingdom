using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scenes.CityMenu.Scripts
{
	public class LevelManager : MonoBehaviour
	{
		public string LevelName;

		void Start()
		{
			var levelFinished = PlayerPrefs.GetInt(LevelName + "_finished", 0) != 0;
			GetComponent<Image>().color = levelFinished ? Color.green : Color.grey;

			GetComponentInChildren<TMP_Text>().text = LevelName;
			GetComponent<Button>().onClick.AddListener(ChangeScene);
		}

		void ChangeScene()
		{        
			SceneManager.LoadScene("Game");
		}
	}
}