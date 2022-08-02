using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Common
{
	public class ChangeSceneManager : MonoBehaviour
	{
		[SerializeField] public string SceneName;
    
		public void ChangeScene()
		{
			SceneManager.LoadScene(SceneName);
		}
	}
}
