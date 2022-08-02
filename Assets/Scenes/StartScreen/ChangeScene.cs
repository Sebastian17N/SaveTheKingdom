using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scenes.StartScreen
{
	public class ChangeScene : MonoBehaviour
	{
		public float TimeToChangeScene;
		public string SceneGoIn;
		void Start()
		{
			StartCoroutine(NewScene());
		}

		IEnumerator NewScene()
		{
			yield return new WaitForSeconds(TimeToChangeScene);
			SceneManager.LoadScene(SceneGoIn);
		}
	}
}
