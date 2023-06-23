using App5.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App5
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private void OnQuitClicked(object sender, EventArgs e)
        {
            // Fermer l'application
            if (Device.RuntimePlatform == Device.Android)
            {
                // Si vous êtes sur Android, utilisez le code suivant
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else if(Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.UWP)
            {
                // Si vous êtes sur iOS ou UWP, utilisez le code suivant
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
