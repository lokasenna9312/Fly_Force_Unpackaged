using UnityEngine;

public class BombController : ItemController
{

    protected override void ItemGain()
    {
        base.ItemGain();
        if (playerController.Bomb < 3 && playerController.currentShieldInstance == null)
        {
            SoundManager.instance.itemGainSound.Play();
            playerController.Bomb++;
            UIManager.instance.BombCheck(playerController.Bomb);
        }
        else if (playerController.Bomb >= 3 && playerController.currentShieldInstance == null)
        {
            SoundManager.instance.itemGainSound.Play();
            UIManager.instance.AddScore(base.score);
        }
    }

}
