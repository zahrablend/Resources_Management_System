using Newtonsoft.Json;
using ResourceShortageUI.Enums;
using ResourceShortageUI.Models;

namespace ResourceShortageUI.Repositories;

public class ShortageRepository
{
    private const string JsonFilePath = "shortage.json";
    private List<Shortage> shortages;

    public ShortageRepository()
    {
        LoadShortage();
    }

    public List<Shortage> GetShortages()
    {
        return shortages;
    }

    

    private void LoadShortage()
    {
        if (File.Exists(JsonFilePath))
        {
            string json = File.ReadAllText(JsonFilePath);
            shortages = JsonConvert.DeserializeObject<List<Shortage>>(json);
        }
        else
        {
            shortages = new List<Shortage>();
        }
    }

    public void SaveShortage()
    {
        string json = JsonConvert.SerializeObject(shortages, Formatting.Indented);
        File.WriteAllText(JsonFilePath, json);
    }

    

   
}
