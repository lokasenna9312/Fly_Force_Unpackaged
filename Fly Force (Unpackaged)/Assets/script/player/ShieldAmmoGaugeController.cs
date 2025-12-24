using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ShieldAmmoGaugeController : MonoBehaviour
    {
        private PlayerController playerController;
        private ShieldController shield;

        [SerializeField] private Image ShieldAmmoGauge;

        private float _shieldAmmo = 0.0f;
        public float ShieldAmmo
        {
            get { return _shieldAmmo; }
            private set
            {
                _shieldAmmo = value;
                UpdateBarUI();
            }
        }
        private float _maxValue = 1.0f;
        public float maxValue
        {
            get { return _maxValue; }
            private set
            {
                _maxValue = value;
                UpdateBarUI();
            }
        }

        private float FillRatio => Mathf.Clamp01(ShieldAmmo / maxValue);

        void Start()
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            if (playerController.isShieldActive == true) shield = GameObject.FindWithTag("Shield").GetComponent<ShieldController>();
        }

        void Update()
        {
            ShieldAmmoSetter();
        }

        void ShieldAmmoSetter()
        {
            if (ShieldAmmo >= 0.0f && ShieldAmmo < 1.0f && playerController.isShieldActive == false)
            {
                ShieldAmmo += Time.deltaTime * 0.1f;
                Debug.Log("Shield Charged " + ShieldAmmo * 100 + "%");
            }
            if (ShieldAmmo >= 1.0f)
            { 
                ShieldAmmo = 1.0f;
                Debug.Log("Shield is Fully Charged!");
            }
        }

        public void ShieldAmmoForceSetter(float amount)
        {
            ShieldAmmo = amount;
        }

        public void UpdateBarUI()
        {
            if (ShieldAmmoGauge != null)
            {
                ShieldAmmoGauge.fillAmount = FillRatio;
            }
        }
    }
}