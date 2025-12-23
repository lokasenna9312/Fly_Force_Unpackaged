public class ItemPowerupController : ItemController
{

    protected override void ItemGain()
    {
        base.ItemGain();
        if (playerController.currentShieldInstance == null)
        {
            SoundManager.instance.itemGainSound.Play();
            if (playerController.Damage < 3)
            {
                playerController.IncreaseBulletLevel(1);
            }
            else if (playerController.Damage >= 3)
            {
                UIManager.instance.AddScore(base.score);
            }
        }
    }
}