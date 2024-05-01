using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [RelayCommand]
        static void ContinueWithEmail()
        {

        }

        [RelayCommand]
        static void ContinueWithGoogle()
        {

        }

        [RelayCommand]
        static async Task ContinueWithApple()
        {
            var hasAccount = await Application.Current.MainPage.DisplayAlert("Hey!", "Você já possui cadastro ?", "Sim", "Não");

            if (hasAccount)
            {

            }
            else
            {

            }
        }
    }
}
