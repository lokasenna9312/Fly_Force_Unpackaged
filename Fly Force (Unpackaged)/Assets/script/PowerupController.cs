using UnityEngine;

public class PowerupController : ItemController
{

    protected override void ItemGain()
    {
        base.ItemGain();
        if (playerController.Damage < 3 && playerController.currentShieldInstance == null)
        {
            SoundManager.instance.itemGainSound.Play();
            playerController.IncreaseBulletLevel(1);
            playerController.BulletProperties(playerController.bulletLevel);
        }
        else if (playerController.Damage >= 3 && playerController.currentShieldInstance == null)
        {
            SoundManager.instance.itemGainSound.Play();
            UIManager.instance.AddScore(base.score);
        }
    }

}
