using ResourceShortageUI.Enums;

namespace ResourceShortageUI.Models;

public class Shortage
{
    public string? Title { get; set; }
    public string? Name { get; set; }
    public Room RoomType { get; }
    public Category ItemCategory { get; }
    public int Priority { get; set; }
    public DateTime CreatedOn { get; set; }
}
