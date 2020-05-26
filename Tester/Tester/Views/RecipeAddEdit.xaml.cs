using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tester
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeAddEdit : ContentPage
    {
        public RecipeAddEdit()
        {
            InitializeComponent();

            //private static MyContext database;
            //RepoT<Recipe> rec = new RepoT<Recipe>(database);
           // using (database = new MyContext())
                //rec.AddTAsync(new Recipe("Test 4", "cream", 12.0, 10000));
                //database.SaveChangesAsync();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var recipe = (Recipe)BindingContext;
            Console.WriteLine("SAVE: " + recipe.Title);
         //   await App.(recipe);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var recipe = (Recipe)BindingContext;
            Console.WriteLine("Cancel: " + recipe.Title);
            await Navigation.PopAsync();
        }
    }
}