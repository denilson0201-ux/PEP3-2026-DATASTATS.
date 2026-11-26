using System.Text.Json;
using DataStats.Models;

namespace DataStats.Services;

public class DataStore
{
    private readonly string _folder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataStats");
    private string FilePath => Path.Combine(_folder, "encuestas.json");

    public List<SurveyResponse> Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return new();
            return JsonSerializer.Deserialize<List<SurveyResponse>>(File.ReadAllText(FilePath)) ?? new();
        }
        catch
        {
            // Si el archivo está dañado, se conserva una copia para no perder los datos al guardar de nuevo.
            try { File.Copy(FilePath, FilePath + ".bak", true); } catch { }
            return new();
        }
    }

    public void Save(List<SurveyResponse> data)
    {
        Directory.CreateDirectory(_folder);
        var options = new JsonSerializerOptions { WriteIndented = true };
        var temp = FilePath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(data, options));
        File.Move(temp, FilePath, true);
    }

    public string DataLocation => FilePath;
}
