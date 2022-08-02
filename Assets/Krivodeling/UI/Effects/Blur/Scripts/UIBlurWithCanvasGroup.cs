using UnityEngine;

namespace Assets.Krivodeling.UI.Effects.Blur.Scripts
{
    public class UiBlurWithCanvasGroup : MonoBehaviour
    {
        private UiBlur _uiBlur;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            SetComponents();

            _uiBlur.OnBeginBlur.AddListener(OnBeginBlur);
            _uiBlur.OnBlurChanged.AddListener(OnBlurChanged);
            _uiBlur.OnEndBlur.AddListener(OnEndBlur);
        }

        private void SetComponents()
        {
            _uiBlur = GetComponent<UiBlur>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnBeginBlur()
        {
            _canvasGroup.blocksRaycasts = true;
        }

        private void OnBlurChanged(float value)
        {
            _canvasGroup.alpha = value;
        }

        private void OnEndBlur()
        {
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
