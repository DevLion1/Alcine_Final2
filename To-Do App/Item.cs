using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_App
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HighImportance { get; set; }
        public bool TimeSensitive { get; set; }
        public bool IsCompleted { get; set; }

        // Constructor
        public Item(string name, string description, bool highImportance, bool timeSensitive, bool completed)
        {
            Name = name;
            Description = description;
            HighImportance = highImportance;
            TimeSensitive = timeSensitive;
            IsCompleted = completed; 
        }

        // Display details in string form 
        public string DisplayInformation()
        {
            // Return a formatted string for display
            return $"{Name}: {Description} (High Importance: {HighImportance}, Time Sensitive: {TimeSensitive}, Completed: {IsCompleted})";
        }

        // Override ToString method to display details
        public override string ToString()
        {
            return $"{Name} - {Description} Priority: {(HighImportance ? "High" : "Normal")}, Time Sensitive: {TimeSensitive}, Completed: {IsCompleted}";
        }
    }
}
