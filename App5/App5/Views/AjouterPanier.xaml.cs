using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace App5.Views
{

    public class Panier
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string pizzaTaille { get; set; }
        public string pizzaType { get; set; }
        public string adresse { get; set; }
        public string tel { get; set; }

        public bool isSelected { get; set; }

    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouterPanier : ContentPage
    {
        private string databasePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
        private SQLiteConnection database;
        public AjouterPanier()
        {
            InitializeComponent();
            var options = new string[]
           {
                "Margerita",
                "4 fromages",
                "viande hachée",
                "au poulet"
           };

            // Assigner la liste des options à la liste déroulante
            picker.ItemsSource = options;
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Panier>();
        }

        private void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            var pizzaData = tel.Text + " " + adresse.Text + picker.SelectedItem;
            //DisplayAlert("Save", pizzaData, "OK");

            

            var panier = new Panier
            {
                pizzaType = picker.SelectedItem as String,
                adresse = adresse.Text,
                tel = tel.Text

            };


            if (radioSmall.IsChecked)
            {
                panier.pizzaTaille = "Petite";

            }
            if (radioMedium.IsChecked)
            {
                panier.pizzaTaille = "Moyenne";

            }
            if (radioLarge.IsChecked)
            {
                panier.pizzaTaille = "Grande";

            }
            // Insérer la note dans la base de données
            var rowsAffected = database.Insert(panier);

            if (rowsAffected > 0)
            {
                // L'insertion a réussi
                DisplayAlert("Save", "Pizza est ajoutée avec succès ", "OK");

                //vider 
                picker.SelectedItem = null;

                radioSmall.IsChecked = false;
                radioMedium.IsChecked = false;
                radioLarge.IsChecked = false;

                adresse.Text = string.Empty;
                tel.Text = string.Empty;

                /*List<Panier> paniers = database.Table<Panier>().ToList();

                // Afficher 
                
                foreach (var n in paniers)
                {
                    DisplayAlert("", $"Note ID: {n.Id}, Text: {n.pizzaTaille}", "ok");
                }*/

            }
            else
            {
                // L'insertion a échoué
                DisplayAlert("Save", "L'insertion a échoué", "OK");
            }


        }

        private void OnPizzaSizeChanged(object sender, CheckedChangedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            
            if (radioSmall.IsChecked)
            {
                //DisplayAlert("", "Petite ", "ok");

            }
            if (radioMedium.IsChecked)
            {
                //DisplayAlert("", "Moyenne ", "ok");

            }
            if (radioLarge.IsChecked)
            {
                //DisplayAlert("", "Large ", "ok");

            }
            

        }
    }
}