using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Common
{
	public class ChangeSceneManager : MonoBehaviour
	{
		[SerializeField] public string SceneName;
    
		public virtual void ChangeScene()
		{
			SceneManager.LoadScene(SceneName);
		}
        private void OnMouseDown()
        {
			SceneManager.LoadScene(SceneName);
		}
    }
}
