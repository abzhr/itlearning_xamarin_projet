using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
   
    public partial class Modifier : ContentPage
    {
        public ICommand LabelClickedCommand { get; private set; }

        private string databasePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
        private SQLiteConnection database;
        private SQLiteConnection db_connection;
        private bool isIdDescending = false;

        public Modifier()
        {
            InitializeComponent();
            BindingContext = this;
            //Debug.WriteLine(BindingContext);
            //db_connection=new SQLiteConnection(databasePath);
            // Récupérer les objets enregistrés à partir de SQLite (exemple de données)
            List<Panier> panierList = GetPaniersFromSQLite();

            // Définir la source de données de la CollectionView
            collectionView.ItemsSource = panierList;

            // Définir la commande pour gérer le clic sur le label
            LabelClickedCommand = new Command<Panier>(ExecuteLabelClickedCommand);

        


        }

        private List<Panier> GetPaniersFromSQLite()
        {
            // Logique pour récupérer les objets Panier depuis SQLite
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                // Créez la table "Panier" si elle n'existe pas déjà
                connection.CreateTable<Panier>();

                // Récupérez tous les enregistrements de la table "Panier" depuis SQLite
                List<Panier> panierList = connection.Table<Panier>().ToList();

                return panierList;
            }
        }

        private  void ExecuteLabelClickedCommand(Panier panier)
        {
            // Action à exécuter lorsque le label est cliqué
            // Par exemple, afficher une alerte avec l'adresse du panier cliqué
            //DisplayAlert("Détails du panier", $"Adresse : {panier.adresse}", "OK");
             Navigation.PushModalAsync(new NavigationPage(new PanierDetails(panier)));
            //await Navigation.PushAsync(new NavigationPage(new PanierDetails(panier)));
        }






        private void SortLabel_Tapped(object sender, EventArgs e)

        {
            //DisplayAlert("", "rzeourapzer", "llo");
            using (SQLiteConnection conn = new SQLiteConnection(databasePath))
            {
               // conn.CreateTable<Panier>();

                if (isIdDescending)
                {
                    List<Panier> sortedList = conn.Table<Panier>().OrderByDescending(p => p.pizzaType).ToList();
                    collectionView.ItemsSource = sortedList;
                    isIdDescending = false;
                }
                else
                {
                    List<Panier> sortedList = conn.Table<Panier>().OrderBy(p => p.pizzaType).ToList();
                    collectionView.ItemsSource = sortedList;
                    isIdDescending = true;
                }
            }
        }


    }
}