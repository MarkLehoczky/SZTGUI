using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace Workshop01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> notes;

        public MainWindow()
        {            
            try { notes = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("notes.json")) ?? new Dictionary<string, string>(); }
            catch (FileNotFoundException) { notes = new Dictionary<string, string>(); }
            InitializeComponent();
            foreach (var item in notes)
                listbox_notes.Items.Add(item.Key);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllText("notes.json", JsonConvert.SerializeObject(notes, Formatting.Indented));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (note_name.Text != "" && !notes.ContainsKey(note_name.Text))
                notes.Add(note_name.Text, "");

            listbox_notes.Items.Clear();
            foreach (var item in notes)
                listbox_notes.Items.Add(item.Key);
        }

        private void listbox_notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox_notes.SelectedItem != null)
                note_text.Text = notes[(string)listbox_notes.SelectedItem];
        }

        private void note_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listbox_notes.SelectedItem != null)
                notes[(string)listbox_notes.SelectedItem] = note_text.Text;
        }
    }
}
