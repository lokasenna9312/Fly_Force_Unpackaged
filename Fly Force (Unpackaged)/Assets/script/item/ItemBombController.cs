public class ItemBombController : ItemController
{

    protected override void ItemGain()
    {
        base.ItemGain();
        if (playerController.isShieldActive == false)
        { 
            SoundManager.instance.itemGainSound.Play();
            if (playerController.Bomb < 3)
            { 
                playerController.GetBomb(1);
            }
            else if (playerController.Bomb >= 3)
            {
                UIManager.instance.AddScore(base.score);
            }
        }
    }
}
