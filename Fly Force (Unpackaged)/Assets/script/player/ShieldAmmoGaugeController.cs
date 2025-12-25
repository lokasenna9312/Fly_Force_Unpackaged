using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ShieldAmmoGaugeController : MonoBehaviour
    {
        private PlayerController playerController;

        [SerializeField] private Image ShieldAmmoGauge;

        private float FillRatio => Mathf.Clamp01(playerController.ShieldAmmo / playerController.maxValue);

        void Start()
        {

        }

        void Update()
        {
            UpdateBarUI();
        }

        public void Initializer(PlayerController owner)
        {
            playerController = owner;
        }

        public void UpdateBarUI()
        {
            if (ShieldAmmoGauge != null && playerController != null)
            {
                ShieldAmmoGauge.fillAmount = FillRatio;
            }
            else if (ShieldAmmoGauge != null)
            {
                ShieldAmmoGauge.fillAmount = 0; // 플레이어가 없으면 게이지 비움
            }
        }
    }
}