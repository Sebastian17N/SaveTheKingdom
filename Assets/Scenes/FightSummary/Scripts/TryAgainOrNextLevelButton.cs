using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class TryAgainOrNextLevelButton : MonoBehaviour
	{
		public TMP_Text ButtonText;
		public FightSummaryGameManager FightSummaryGameManager;

		public string SceneName;
		public Button ChangingButton;

		void Start()
		{
			FightSummaryGameManager = FindObjectOfType<FightSummaryGameManager>();
			ChangingButton.onClick.AddListener(ChangeScene);

			ButtonText.text = 
				FightSummaryGameManager.DidGamerWin 
					? "NEXT LEVEL" 
					: "TRY AGAIN";
		}
    
		public void ChangeScene()
		{
			if (FightSummaryGameManager.DidGamerWin)
			{
				SceneManager.LoadScene(SceneName);
			}
			else
			{
				// TODO: Jak chcemy przekazywać inną scenę przy porażce?
				SceneManager.LoadScene(SceneName);
			}
		}
              
	}
}
