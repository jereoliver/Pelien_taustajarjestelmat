public static class PlayerExtensions
{
    public static Item GetHighestLevelItem(this Player player)
    {
        Item highestItem = new Item();
        highestItem.Level = -100;
        foreach (Item x in player.Items)
        {
            if (x.Level > highestItem.Level)
                highestItem = x;
        }


        return highestItem;
    }
}