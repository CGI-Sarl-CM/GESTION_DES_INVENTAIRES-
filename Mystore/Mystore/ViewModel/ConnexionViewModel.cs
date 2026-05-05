global using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyStoreData;
using MyStoreData.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Mystore.ViewModel
{
    
  public partial class ConnexionViewModel :ObservableObject
    {
       [ObservableProperty] 
        public partial string User { get; set; }
        [ObservableProperty]
        public partial string Password { get; set; }
        partial void OnUserChanged(string oldValue, string newValue)
        {
            Debug.WriteLine(oldValue);
            Debug.WriteLine(newValue);
            //Debugger.Break();
        }

        private readonly IRealmFactory realm;

        public ConnexionViewModel(IRealmFactory Realm)
        {
            realm = Realm;
            realm.GetRealmInstance();


            realm.GetRealmInstance().Write(() =>
            {
                realm.GetRealmInstance().Add(new UserModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "johndoe",
                    Password = "password123",
                    DateCreated = DateTimeOffset.Now
                });
            });

            Debug.WriteLine(realm.GetRealmInstance().All<UserModel>().Count());
        }

        [RelayCommand]
        public async Task Connect()
        {
            var bd = realm.GetRealmInstance();
            var utilisateur = bd.All<UserModel>()
                .FirstOrDefault
                (u => u.UserName == User);

            if(utilisateur == null || utilisateur.Password != Password)
            {
                await Shell.Current.DisplayAlertAsync("Erreur", "Utilisateur ou mot de passe incorrect", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Success", "vos informations sont correctes", "OK"); 
                
            }

        }
        
    }
}
