using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Tester.Models;
using Tester.Services;
using Tester.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tester
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeList : ContentPage
    {
        private MyContext database = new MyContext();
        ObservableCollection<Recipe> employees = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe> Employees { get { return employees; } }


        public RecipeList() {
            InitializeComponent();

        }


        protected override void OnAppearing()
        {
  //          base.OnAppearing();
 //           BindingContext = new RecipeListModel(database.Recipe.ToList());
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipeAddEdit
            {
   //             BindingContext = new Recipe("", "", 0.0, 0)
            }) ;
            
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TheRecipe
                {
   //                 BindingContext = e.SelectedItem as Recipe
                });
            }
        }
    }
}