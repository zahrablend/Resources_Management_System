using ResourceShortageUI.Models;

namespace ResourceShortageUI;

public static class ShortageDisplayer
{
    public static void DisplayShortage(List<Shortage> shortages)
    {
        foreach (var shortage in shortages)
        {
            Console.WriteLine($"{shortage.Priority}: " +
                $"{shortage.Title} ({shortage.RoomType}, {shortage.ItemCategory})");
        }
    }
}
 