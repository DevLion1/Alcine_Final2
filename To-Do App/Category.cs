using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_App
{
    public class Category
    {
        public string Name { get; set; }
        // List of Items in Category
        public List<Item> TodoItemsInCategory { get; set; }

        // Constructor
        public Category(string name)
        {
            Name = name;
            TodoItemsInCategory = new List<Item>();
        }

        // Method to add an Item to a specific category
        public void AddItemToCategory(Item item)
        {
            TodoItemsInCategory.Add(item);
        }

        public override string ToString()
        {
            // Return the category name
            return Name;
        }
    }
}
