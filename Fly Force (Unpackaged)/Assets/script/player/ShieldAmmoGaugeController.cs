using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ShieldAmmoGaugeController : MonoBehaviour
    {
        private PlayerController playerController;

        [SerializeField] private Image ShieldAmmoGauge;

        private float _shieldAmmo = 0.0f;
        public float ShieldAmmo
        {
            get { return _shieldAmmo; }
            set
            {
                _shieldAmmo = value;
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

        private float FillRatio => Mathf.Clamp01(ShieldAmmo / maxValue);

        void Start()
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        void Update()
        {
            ShieldAmmoSetter();
        }

        void ShieldAmmoSetter()
        {
            if (ShieldAmmo >= 0.0f && ShieldAmmo < 1.0f && playerController.currentShieldInstance == null)
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
        public void UpdateBarUI()
        {
            if (ShieldAmmoGauge != null)
            {
                ShieldAmmoGauge.fillAmount = FillRatio;
            }
        }
    }
}