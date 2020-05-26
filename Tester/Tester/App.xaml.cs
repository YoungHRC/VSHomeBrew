using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tester.Services;
using System.Diagnostics;
using Tester.Models;
using System.Linq;

namespace Tester
{
    public partial class App : Application
    {
        //https://medium.com/@utterbbq/c-unitofwork-and-repository-pattern-305cd8ecfa7a
        //static MyContext database;
        //public static MyContext Database{
        //    get{
        //        if (database == null){
        //            database = new MyContext();
        //        }
        //        return database;
        //    }
        //}
        public App()
        {
           InitializeComponent();
           MainPage = new NavigationPage(new RecipeList());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
