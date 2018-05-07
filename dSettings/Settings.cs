using System.Collections.Generic;
using System.IO;

namespace dSettings
{
    public class Settings
    {
        private static Settings _instance;
        private static string SettingsFile = "settings.ini";
        private Dictionary<string, string> _values;
        private static Dictionary<string, string> _defaultValues;

        public Settings()
        {
            _instance = this;
            _values = new Dictionary<string, string>();
            LoadFile();
        }

        public static Settings GetInstance()
        {
            return _instance ?? (_instance = new Settings());
        }

        private void LoadFile()
        {
            if (File.Exists(SettingsFile))
            {
                var lines = File.ReadAllLines(SettingsFile);
                foreach (var line in lines)
                {
                    // Skip lines that start with '#' (comments).
                    if (line.StartsWith("#")) continue;
                
                    var keyValue = line.Split('=');

                    Set(keyValue[0], keyValue[1]);
                }
            }
            else
            {
                SaveDefaults();
                throw new SettingsFileNotFoundException("settings.ini was not found. Default settings loaded.");
            }
        }

        private void Set(string key, string value)
        {
            if (!Exists(key)) _values.Add(key, value);
            else _values[key] = value;
        }

        public bool Exists(string key)
        {
            return _values.ContainsKey(key);
        }

        public string Get(string key)
        {
            var success = _values.TryGetValue(key, out string value);
            return success ? value : null;
        }

        private void SaveDefaults()
        {
            var lines = new List<string>();
        
            foreach (var key in _defaultValues.Keys)
            {
                lines.Add($"{key}={Get(key)}");    
            }
        
            File.WriteAllLines(SettingsFile, lines);
        }

        public static void AddDefaultValue(string key, string val)
        {
            _defaultValues[key] = val;
        }

        public static void SetSettingsFileLocation(string loc)
        {
            SettingsFile = loc;
        }
    }
}