using Newtonsoft.Json.Converters;
using ResourceShortageUI.Enums;
using System.Text.Json.Serialization;

namespace ResourceShortageUI.Models;

public class Shortage
{
    public string? Title { get; set; }
    public string? Name { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public Room RoomType { get; }
    [JsonConverter(typeof(StringEnumConverter))]
    public Category ItemCategory { get; }
    public int Priority { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? UserName { get; set; }
}
