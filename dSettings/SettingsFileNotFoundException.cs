using System;

namespace dSettings
{
    public class SettingsFileNotFoundException : Exception
    {
        public SettingsFileNotFoundException()
        {
            
        }

        public SettingsFileNotFoundException(string message) : base(message)
        {
            
        }
    }
}