using System;
public enum ItemType { SWORD, POTION, SHIELD }

public class Item
{
    public int level { get; set; }
    public DateTime creationDate;
    public ItemType type { get; set; }
    public Guid itemId { get; set; }

}