using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
namespace HMSL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        string connectionString;
        string updateConnection;
        string resetConnection;
        ObservableCollection<Item> app_names;
        string[]? files;
        [Serializable]
        public class Sorted
        {
            public bool IsSorted { get; set; }
        }
        Sorted sort_app = new Sorted() { IsSorted = false};
        public MainWindow()
        {
            Uri iconUri = new Uri("icon.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            InitializeComponent();
            App_list.Foreground = Brushes.Red;
            App_list.FontSize = 22;
            App_list.FontFamily = new FontFamily("Courier New");
            app_names = new ObservableCollection<Item>()
            {
            };
            App_list.ItemsSource = app_names;

            App_list.DisplayMemberPath = "Name";
            App_list.SelectedValuePath = "Path_pic";
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            updateConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            resetConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
        }
        [Serializable] // Индикация для сохранения данных в app_names после выхода
        class Item 
        {
            public string? Name { get; set; }
            public string? Path_pic { get; set; }
            public string? Path_app { get; set; }   
            public string? ID { get; set; }
            public string? Full_path { get; set; }   
            public string? IsEnabled { get; set; }
            public string? Background_pic { get; set; } 
        }
        private void App_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((App_list.SelectedItem != null)) {
                app_name_text.Text = ((App_list.SelectedItem as Item).Name);
            }
            else
            {
                app_name_text.Text = "";
            }
            if (App_list.Items.Count >= 1)
            {
                if ((App_list.SelectedItem as Item) != null)
                {
                    if (File.Exists((App_list.SelectedItem as Item).Path_pic) == true)
                    {
                        pic.Source = (ImageSource)new ImageSourceConverter().ConvertFrom((App_list.SelectedItem as Item).Path_pic);
                    }
                    else if (File.Exists(@"pics\default.jpg") == true)
                    {
                        pic.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"pics\default.jpg");
                    }

                    else
                    {
                        pic.Source = null;
                    }
                }
            }
            if (App_list.Items.Count >= 1)
            {
                if ((App_list.SelectedItem as Item) != null)
                {
                    if (File.Exists((App_list.SelectedItem as Item).Background_pic) == true)
                    {
                        background.Source = (ImageSource)new ImageSourceConverter().ConvertFrom((App_list.SelectedItem as Item).Background_pic);
                    }
                    else
                    {
                        background.Source = null;
                    }
                }
            }
            ///
           
            // pic.Source = new BitmapImage(new Uri("pics/warzone.jpg", UriKind.Relative));
            // pic.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(App_list.SelectedValue.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
        
        private void Find_app_Click(object sender, RoutedEventArgs e)
        {
            
            if(sort_app.IsSorted == true)
            {
                App_list.ItemsSource = app_names.OrderBy(c => c.Name);
            }
            else 
            {
                App_list.ItemsSource = app_names;
            }
            if (App_list.Items.Count <= 0)
            {
                App_list.SelectedIndex = 0;
            }
            CommonOpenFileDialog app = new CommonOpenFileDialog();
            app.IsFolderPicker = true;
            app.Multiselect = true;
            if (app.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ///
                SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string? app_name = "";
            string? app_id = "";
            string query = "SELECT * from App_Table";
            string? app_FileName = "";
            string? app_image = "";
            string? app_FullPath = "";
            string? app_IsEnabled = "";
            string? app_background = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@App_name", app_name);
            cmd.Parameters.AddWithValue("@ID", app_id);
            cmd.Parameters.AddWithValue("@App_FileName", app_FileName);
            cmd.Parameters.AddWithValue("@App_image", app_image);
            cmd.Parameters.AddWithValue("@App_FullPath", app_FullPath);
            cmd.Parameters.AddWithValue("@App_IsEnabled", app_IsEnabled);
            cmd.Parameters.AddWithValue("@App_Background", app_background);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {   
                    app_name = reader["App_name"].ToString();
                    app_id = reader["ID"].ToString();
                    app_FileName = reader["App_FileName"].ToString();
                    app_image = reader["App_image"].ToString();
                    app_FullPath = reader["App_FullPath"].ToString();
                    app_IsEnabled = reader["App_IsEnabled"].ToString();
                    app_background = reader["App_Background"].ToString();
                    test.Text += app_IsEnabled;
                    foreach (string folders in app.FileNames)
                    {
                         try
                            {
                                files = Directory.GetFiles(folders, "*.exe", SearchOption.AllDirectories);                              
                            }
                            catch(Exception ex) { }

                        if (files != null)
                        {
                            foreach (string file in files)
                            {
                                app_FullPath = file;
                                if (System.IO.Path.GetFileName(file) == reader["App_FileName"].ToString())
                                {
                                    if (app_IsEnabled == "false")
                                    {
                                        app_IsEnabled = "true";
                                        SqlConnection update_connection = new SqlConnection(updateConnection);
                                        update_connection.Open();
                                        //string sql = @"UPDATE App_Table SET App_IsEnabled = '" + app_IsEnabled + "'" + "' WHERE ID = '" + app_id + "'";
                                        string sql = @"UPDATE App_Table SET App_IsEnabled = @app_IsEnabled WHERE App_name = @app_name";
                                        SqlCommand cmd_update = new SqlCommand();
                                        cmd_update.Parameters.AddWithValue("@App_IsEnabled", app_IsEnabled);
                                        cmd_update.Parameters.AddWithValue("@App_name", app_name);
                                        cmd_update.CommandText = sql;
                                        cmd_update.Connection = update_connection;
                                        cmd_update.CommandType = CommandType.Text;
                                        SqlDataReader update_reader = cmd_update.ExecuteReader();
                                        app_names.Add(new Item()
                                        {
                                            Name = app_name,
                                            ID = app_id,
                                            Path_app = app_FileName,
                                            Path_pic = app_image,
                                            Full_path = app_FullPath,
                                            IsEnabled = app_IsEnabled,
                                            Background_pic = app_background
                                        });
                                        if (sort_app.IsSorted == true)
                                        {
                                            App_list.ItemsSource = app_names.OrderBy(c => c.Name);
                                        }
                                        else
                                        {
                                            App_list.ItemsSource = app_names;
                                        }
                                    }
                                }
                            }
                        }
                        
                    }                  
                }
            }                                  
        }
        private void App_launch_Click(object sender, RoutedEventArgs e)
        {
            if(App_list.SelectedItem as Item != null)
            {
                string? file_name = ((App_list.SelectedItem as Item).Full_path);
                if (file_name != null)
                {
                    string? pathFolder = "";
                    if ((App_list.SelectedItem as Item) == null)
                    {
                        pathFolder = "";
                        MessageBox.Show("Not Found! Choose an application.", "Error");
                    }
                    else
                    {
                        pathFolder = System.IO.Path.GetDirectoryName(((App_list.SelectedItem as Item).Full_path));
                        if ((Directory.Exists(pathFolder) == true))
                        {
                        }
                        else if (pathFolder == null)
                        {
                            MessageBox.Show("Folder not found.", "Error");
                        }
                    }
                    ///
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();     
                    processStartInfo.FileName = file_name;
                    if (((App_list.SelectedItem as Item).Name) == "Warzone")
                    {      
                        
                        processStartInfo.UseShellExecute = false;
                        processStartInfo.RedirectStandardError = false;
                        processStartInfo.RedirectStandardOutput = false;
                        processStartInfo.ArgumentList.Add("explorer.exe");
                        processStartInfo.ArgumentList.Add("ModernWarfare.exe");
                        processStartInfo.ArgumentList.Add("-uid");
                        processStartInfo.ArgumentList.Add("odin");
                        Process.Start(processStartInfo);    
                    }
                    else if(((App_list.SelectedItem as Item).Name) == "Resident Evil 2 Remake")
                    {
                        processStartInfo.UseShellExecute = false;
                        processStartInfo.RedirectStandardError = false;
                        processStartInfo.RedirectStandardOutput = false;
                        processStartInfo.ArgumentList.Add("steam://rungameid/883710");
                        if (File.Exists(@"C:\Program Files (x86)\Steam\steam.exe"))
                        {
                            processStartInfo.FileName = @"C:\Program Files (x86)\Steam\steam.exe";
                        }
                        
                        Process.Start(processStartInfo);
                    }
                    else
                    {
                        processStartInfo.Verb = "runas";
                        processStartInfo.WorkingDirectory = pathFolder;
                        Process.Start(processStartInfo);
                    }
                }
                else
                {
                    MessageBox.Show("Not found path of application.", "Error");
                }
            }
            else
            { 
                MessageBox.Show("Not found path of application.", "Error");
            }
            
                        
        }

        private void sort_Click(object sender, RoutedEventArgs e)
        {
            if (app_names.Count > 0)
            {
                App_list.ItemsSource = app_names.OrderBy(c => c.Name);
                sort_app.IsSorted = true;
            }
        }

        private void reset_apps_Click(object sender, RoutedEventArgs e)
        {
            pic.Source = null;
            background.Source = null;
            app_name_text.Text = null;
            if (App_list.Items.Count > 0)
            {
                App_list.ClearValue(ItemsControl.ItemsSourceProperty);
                app_names.Clear();
            }
            sort_app.IsSorted = false;
            string? app_IsEnabled = "";
            string? app_name = "";
            app_IsEnabled = "false";
            SqlConnection reset_connection = new SqlConnection(resetConnection);
            reset_connection.Open();
            
            string reset_string = @"UPDATE App_Table SET App_IsEnabled = @app_IsEnabled";
            SqlCommand reset_cmd = new SqlCommand();
            reset_cmd.CommandType = CommandType.Text;
            reset_cmd.Connection = reset_connection;
            reset_cmd.CommandText = reset_string;
            reset_cmd.Parameters.AddWithValue("@App_IsEnabled", app_IsEnabled);
            reset_cmd.Parameters.AddWithValue("@App_name", app_name);
            SqlDataReader reader = reset_cmd.ExecuteReader();
           

        }
        

        public static class BinarySerialization // Класс для сохранения и последующего чтения данных
        {
            /// <summary>
            /// Writes the given object instance to a binary file.
            /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
            /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
            /// </summary>
            /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
            /// <param name="filePath">The file path to write the object instance to.</param>
            /// <param name="objectToWrite">The object instance to write to the XML file.</param>
            /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
            public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false) // Запись в *.bin
            {
                using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, objectToWrite);
                }
            }

            /// <summary>
            /// Reads an object instance from a binary file.
            /// </summary>
            /// <typeparam name="T">The type of object to read from the XML.</typeparam>
            /// <param name="filePath">The file path to read the object instance from.</param>
            /// <returns>Returns a new instance of the object read from the binary file.</returns>
            public static T ReadFromBinaryFile<T>(string filePath) // Чтение из *.bin
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
        }
        private void Window_Closing(object sender, EventArgs e)
        {
           
                BinarySerialization.WriteToBinaryFile<ObservableCollection<Item>>(@"appnames.bin", app_names);
            BinarySerialization.WriteToBinaryFile<Sorted>(@"sorted.bin", sort_app);
            
            }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@"appnames.bin"))
            {
                app_names = BinarySerialization.ReadFromBinaryFile<ObservableCollection<Item>>(@"appnames.bin");
                App_list.ItemsSource = app_names;   
                
            }
            if (File.Exists(@"sorted.bin"))
            {
                sort_app = BinarySerialization.ReadFromBinaryFile<Sorted>(@"sorted.bin");
                if (sort_app.IsSorted == true)
                {
                    App_list.ItemsSource = app_names.OrderBy(c => c.Name);

                }
            }
            App_list.SelectedIndex = 0;
        }

        private void fullpath_app_Click(object sender, RoutedEventArgs e)
        {
            string? pathFolder = "";
            if((App_list.SelectedItem as Item) == null)
            {
                pathFolder = "";
                MessageBox.Show("Not Found! Choose an application.", "Error");
            }
            else
            {
                pathFolder = System.IO.Path.GetDirectoryName(((App_list.SelectedItem as Item).Full_path));
                if ((Directory.Exists(pathFolder) == true))
                {

                    Process.Start("explorer.exe", pathFolder);
                }
                else if (pathFolder == null)
                {
                    MessageBox.Show("Folder not found.", "Error");
                }
            }         
        }
        private void pic_visibility_Checked(object sender, RoutedEventArgs e)
        {
            if (pic_visibility.IsChecked == true)
            {
                pic.Visibility = Visibility.Hidden;
                pic_border.Visibility = Visibility.Hidden;
            }         
        }
        private void pic_visibility_Unchecked(object sender, RoutedEventArgs e)
        {
            if (pic_visibility.IsChecked == false)
            {
                pic.Visibility = Visibility.Visible;
                pic_border.Visibility = Visibility.Visible;
            }
        }

        private void reset_current_app_Click(object sender, RoutedEventArgs e)
        {

            pic.Source = null;
            background.Source = null;
            app_name_text.Text = null;
               
            string? app_name = ((App_list.SelectedItem) as Item).Name;
            string? app_IsEnabled = "";
            app_IsEnabled = "false";
            SqlConnection reset_connection = new SqlConnection(resetConnection);
            reset_connection.Open();

            string reset_string = @"UPDATE App_Table SET App_IsEnabled = @app_IsEnabled WHERE App_name = @app_name";
            SqlCommand reset_cmd = new SqlCommand();
            reset_cmd.CommandType = CommandType.Text;
            reset_cmd.Connection = reset_connection;
            reset_cmd.CommandText = reset_string;
            reset_cmd.Parameters.AddWithValue("@App_IsEnabled", app_IsEnabled);
            reset_cmd.Parameters.AddWithValue("@App_name", app_name);
            SqlDataReader reader = reset_cmd.ExecuteReader();
            
            app_names.Remove(App_list.SelectedItem as Item);
            App_list.ClearValue(ItemsControl.ItemsSourceProperty);
            if (sort_app.IsSorted == true)
            {
                App_list.ItemsSource = app_names.OrderBy(c => c.Name);
            }
            else
            {
                App_list.ItemsSource = app_names;
            }
            App_list.SelectedIndex = 0;
        }
    }
}
