using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanierDetails : ContentPage
    {
        private string databasePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
        private SQLiteConnection database;
        private SQLiteConnection db_connection;
        private Panier newPanier;

        public PanierDetails(Panier panier)
        {
            InitializeComponent();
            //DisplayAlert("",panier.adresse,"ok");
            var options = new string[]
         {
                "Margerita",
                "4 fromages",
                "viande hachée",
                "au poulet"
         };

            // Assigner la liste des options à la liste déroulante
            picker.ItemsSource = options;
            picker.SelectedItem = panier.pizzaType;
            adresse.Text = panier.adresse;
            tel.Text = panier.tel;

            if (panier.pizzaTaille=="Petite")
            {
                radioSmall.IsChecked = true;
            }
            if (panier.pizzaTaille == "Moyenne")
            {
              radioMedium.IsChecked = true;
            }
            if (panier.pizzaTaille == "Grande")
            {
                radioLarge.IsChecked = true;
            }


            db_connection =new SQLiteConnection(databasePath);
            newPanier = panier;
            


        }

        private void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            newPanier.adresse = adresse.Text ;
            newPanier.tel=tel.Text ;
            newPanier.pizzaType = picker.SelectedItem.ToString() ;



            if (radioSmall.IsChecked)
            {
                newPanier.pizzaTaille = "Petite";

            }
            if (radioMedium.IsChecked)
            {
                newPanier.pizzaTaille = "Moyenne";

            }
            if (radioLarge.IsChecked)
            {
                newPanier.pizzaTaille = "Grande";

            }

            db_connection.Update(newPanier);


            DisplayAlert("Success", "Panier est a jour!", "OK");



        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var backButton = new ToolbarItem
            {
                Text = "Retour",
                IconImageSource = "back_arrow_icon.png", // Icône de la flèche de retour (remplacez "back_arrow_icon.png" par votre propre icône)
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            backButton.Clicked += OnBackButtonClicked;
            ToolbarItems.Add(backButton);
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

}
