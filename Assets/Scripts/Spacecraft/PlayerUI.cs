using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

namespace DockMe
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        private Spacecraft target;

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private TMP_Text playerNameText;


        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        [SerializeField]
        private TMP_Text amountText;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        void Update()
        {
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
            // Reflect the Player Health
            if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Propellant.RelativeAmount;
                amountText.text = target.Propellant.Amount.ToString("F1", CultureInfo.InvariantCulture);
            }

            
        }

        private void LateUpdate()
        {
            if (targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
            }

            
        }

        #endregion

        #region Public Methods

        public void SetTarget(Spacecraft _target)
        {
            if (_target == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }
            // Cache references for efficiency
            target = _target;
            if (playerNameText != null && target.photonView.Owner != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }

            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            
        }

        #endregion

    }
}