public class ItemPowerupController : ItemController
{

    protected override void ItemGain()
    {
        base.ItemGain();
        if (playerController.isShieldActive == false)
        {
            SoundManager.instance.itemGainSound.Play();
            if (playerController.bulletLevel < 3)
            {
                playerController.IncreaseBulletLevel(1);
            }
            else if (playerController.bulletLevel >= 3)
            {
                UIManager.instance.AddScore(base.score);
            }
        }
    }
}