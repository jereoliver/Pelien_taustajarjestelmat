using System;
using System.ComponentModel.DataAnnotations;

public enum ItemType { SWORD, POTION, SHIELD }

public class Item
{
    [Range(0, 99)]
    public int level { get; set; }
    [CheckTheDate]
    public DateTime creationDate;
    public ItemType type { get; set; }
    public Guid itemId { get; set; }

}