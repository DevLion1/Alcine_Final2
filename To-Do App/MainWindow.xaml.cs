using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

//Benel Alcine
//12/05/23
//Programming 2 Final

namespace To_Do_App
{
    public partial class MainWindow : Window
    {
        private List<Category> _categories;
        private Category _selectedCategory;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCategories();
        }

        private void InitializeCategories()
        {
            // Preload categories
            _categories = new List<Category>
            {
                new Category("Today"),
                new Category("Shopping"),
                new Category("Travel")
            };

            // TODO: Add items to categories as specified in the preload instructions

            categoryComboBox.ItemsSource = _categories;
            categoryComboBox.SelectedIndex = 0;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Change selected category and update ListView
            _selectedCategory = categoryComboBox.SelectedItem as Category;
            if (_selectedCategory != null)
            {
                itemsListView.ItemsSource = _selectedCategory.TodoItemsInCategory;
            }
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            // Add new category
            var categoryName = newCategoryTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(categoryName))
            {
                _categories.Add(new Category(categoryName));
                categoryComboBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a category name.");
            }
        }

        private void AddToListButton_Click(object sender, RoutedEventArgs e)
        {
            // Add new item to selected category
            var itemName = taskNameTextBox.Text.Trim();
            var itemDescription = new TextRange(descriptionRichTextBox.Document.ContentStart, descriptionRichTextBox.Document.ContentEnd).Text.Trim();
            var highImportance = highImportanceCheckBox.IsChecked ?? false;
            var timeSensitive = timeSensitiveCheckBox.IsChecked ?? false;
            var completed = completedRadioButton.IsChecked ?? false;

            if (!string.IsNullOrEmpty(itemName) && !string.IsNullOrEmpty(itemDescription))
            {
                var newItem = new Item(itemName, itemDescription, highImportance, timeSensitive, completed);
                _selectedCategory.AddItemToCategory(newItem);
                itemsListView.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter name and description.");
            }
        }

        private void UpdateSelectedItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Makes sure an item is selected
            if (itemsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }
            // Update selected item
            if (itemsListView.SelectedIndex >= 0)
            {
                var selectedItem = itemsListView.SelectedItem as Item;
                if (selectedItem != null)
                {
                    selectedItem.Name = taskNameTextBox.Text;
                    selectedItem.Description = new TextRange(descriptionRichTextBox.Document.ContentStart, descriptionRichTextBox.Document.ContentEnd).Text;
                    selectedItem.HighImportance = highImportanceCheckBox.IsChecked ?? false;
                    selectedItem.TimeSensitive = timeSensitiveCheckBox.IsChecked ?? false;
                    selectedItem.IsCompleted = completedRadioButton.IsChecked ?? false;

                    itemsListView.Items.Refresh();
                    UpdateTaskInfoRichTextBox(selectedItem);
                }
            }
        }

        private void UpdateTaskInfoRichTextBox(Item selectedItem)
        {
            // Format the details of the selected item
            string formattedText = $"Name: {selectedItem.Name}\n\n" +
                                   $"Completed: {selectedItem.IsCompleted}\n" +
                                   $"High Importance: {selectedItem.HighImportance}\n" +
                                   $"Time Sensitive: {selectedItem.TimeSensitive}\n" +
                                   $"Description: {selectedItem.Description}" +
                                    "**Assignment Completed**\n";
            if (!selectedItem.IsCompleted)
            {
                // Remove Assignment Completed Text
                formattedText = formattedText.Replace("**Assignment Completed**\n", "");
            }
            // Update the RichTextBox with the formatted text
            taskInfoRichTextBox.Document.Blocks.Clear();
            taskInfoRichTextBox.Document.Blocks.Add(new Paragraph(new Run(formattedText)));
        }

        private void ClearBoxesButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear all inputs and selections
            taskNameTextBox.Clear();
            descriptionRichTextBox.Document.Blocks.Clear();
            highImportanceCheckBox.IsChecked = false;
            timeSensitiveCheckBox.IsChecked = false;
            completedRadioButton.IsChecked = false;
            notCompletedRadioButton.IsChecked = true;
            itemsListView.SelectedIndex = -1;
        }

        private void ItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update input fields based on selected item
            if (itemsListView.SelectedItem is Item selectedItem)
            {
                taskNameTextBox.Text = selectedItem.Name;
                descriptionRichTextBox.Document.Blocks.Clear();
                descriptionRichTextBox.Document.Blocks.Add(new Paragraph(new Run(selectedItem.Description)));
                highImportanceCheckBox.IsChecked = selectedItem.HighImportance;
                timeSensitiveCheckBox.IsChecked = selectedItem.TimeSensitive;
                if (selectedItem.IsCompleted)
                {
                    completedRadioButton.IsChecked = true;
                }
                else
                {
                    notCompletedRadioButton.IsChecked = true;
                }
                // Construct the formatted string
                string formattedText = $"Name: {selectedItem.Name}\n\n" +
                                       $"Completed: {selectedItem.IsCompleted}\n" +
                                       $"High Importance: {selectedItem.HighImportance}\n" +
                                       $"Time Sensitive: {selectedItem.TimeSensitive}\n" +
                                       $"Description: {selectedItem.Description}" +
                                        "**Assignment Completed**\n";
                if (!selectedItem.IsCompleted)
                {
                    // Remove Assignment Completed Text
                    formattedText = formattedText.Replace("**Assignment Completed**\n", "");
                }
                // Set the formatted text to the RichTextBox
                taskInfoRichTextBox.Document.Blocks.Clear(); // Clear previous content
                taskInfoRichTextBox.Document.Blocks.Add(new Paragraph(new Run(formattedText)));

            }
        }

        private void assignmentCompleted(bool isCompleted)
        {
            // Update selected item
            if (itemsListView.SelectedIndex >= 0)
            {
                var selectedItem = itemsListView.SelectedItem as Item;
                if (selectedItem != null)
                {
                    selectedItem.Name = taskNameTextBox.Text;
                    selectedItem.Description = new TextRange(descriptionRichTextBox.Document.ContentStart, descriptionRichTextBox.Document.ContentEnd).Text;
                    selectedItem.HighImportance = highImportanceCheckBox.IsChecked ?? false;
                    selectedItem.TimeSensitive = timeSensitiveCheckBox.IsChecked ?? false;
                    selectedItem.IsCompleted = completedRadioButton.IsChecked ?? false;

                    itemsListView.Items.Refresh();
                    // Format the details of the selected item
                    string formattedText = $"Name: {selectedItem.Name}\n\n" +
                                           $"Completed: {selectedItem.IsCompleted}\n" +
                                           $"High Importance: {selectedItem.HighImportance}\n" +
                                           $"Time Sensitive: {selectedItem.TimeSensitive}\n" +
                                           $"Description: {selectedItem.Description}" +
                                           "**Assignment Completed**\n";
                    if (!isCompleted)
                    {
                        // Remove Assignment Completed Text
                        formattedText = formattedText.Replace("**Assignment Completed**\n", "");
                    }

                    // Update the RichTextBox with the formatted text
                    taskInfoRichTextBox.Document.Blocks.Clear();
                    taskInfoRichTextBox.Document.Blocks.Add(new Paragraph(new Run(formattedText)));
                    
                }
            }
        }

     


        private void completedRadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (itemsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                completedRadioButton.IsChecked = false;
            }
            else
            {
                // Call assignmentCompleted method with true since it is completed
                assignmentCompleted(false);
            }
        }
    }

   

    

}//namespace

