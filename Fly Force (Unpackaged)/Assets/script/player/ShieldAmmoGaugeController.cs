using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ShieldAmmoGaugeController : MonoBehaviour
    {
        PlayerController playerController;

        [SerializeField] private Image ShieldAmmoGauge;

        private float _currentValue = 0.0f;
        public float currentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                UpdateBarUI();
            }
        }

        private float _maxValue = 1.0f;
        public float maxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                UpdateBarUI();
            }
        }

        private float FillRatio => Mathf.Clamp01(currentValue / maxValue);

        void Start()
        {

        }

        void Update()
        {
            if (playerController != null)
            {
                currentValue = playerController.ShieldAmmo;
                ChangeValue(currentValue);
            }
        }
        public void SetPlayerController(PlayerController pc)
        {
            playerController = pc;
            currentValue = playerController.ShieldAmmo;
            UpdateBarUI();
        }
        public void UpdateBarUI()
        {
            if (ShieldAmmoGauge != null)
            {
                ShieldAmmoGauge.fillAmount = FillRatio;
            }
        }

        public void ChangeValue(float amount)
        {
            if (amount >= 0.0f && amount < 1.0f)
            {
                currentValue = Mathf.Clamp(amount, 0f, maxValue);
            }
            if (amount < 0.0f)
            {
                currentValue = 1.0f;
            }
            UpdateBarUI();
        }
    }
}