using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

public class InventoryLogger<T> where T : IInventoryEntity
{
    private List<T> _log;
    private readonly string _filePath;

    public InventoryLogger(string filePath)
    {
        _log = new List<T>();
        _filePath = filePath;
    }

    public void Add(T item)
    {
        _log.Add(item);
    }

    public List<T> GetAll()
    {
        return new List<T>(_log);
    }

    public void SaveToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                string jsonData = JsonSerializer.Serialize(_log);
                writer.Write(jsonData);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to file: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string jsonData = reader.ReadToEnd();
                    _log = JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from file: {ex.Message}");
            _log = new List<T>();
        }
    }
}
