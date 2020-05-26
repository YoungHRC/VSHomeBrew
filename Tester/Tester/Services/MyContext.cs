using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tester.Models;
using Xamarin.Forms;
using static Tester.Models.Alarms;

namespace Tester.Services{
    //TODO: lock(new object())
    public class MyContext : DbContext
    {
        static string databasePath;
        string databaseName = "trial.db";

        public MyContext() : base(){
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); 
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            Database.EnsureCreated();
        }

        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<Alarms> Alarms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //https://docs.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations
            //https://stackoverflow.com/questions/24033924/creating-a-double-linked-list-in-entity-framework
            modelBuilder.Entity<Alarms>()
                .HasIndex(b => b.Head)
                .IsUnique()
                .HasFilter("[Head] = 1");

            modelBuilder.Entity<Alarms>()
                .Property(p => p.stageName)
                .HasConversion(
                  new EnumToStringConverter<Stage>()
                );
        }
        //ObservableCollection<Recipe> recList;
        //public void SetRecList()
        //{
        //    foreach (var recipe in Recipe)
        //    {
        //        recList.Add(recipe);
        //    }
        //}
        //public ObservableCollection<Recipe> GetAllRecipes()
        //{
        //    if (recList == null)
        //    {
        //        SetRecList();
        //    }
        //    return recList;
        //}
        public Alarms GetHead(Recipe id)
        {
            return Alarms.SingleOrDefault(x => x.Recipe.Id == id.Id && x.Head == true);
        }
        public IEnumerable<Alarms> GetAlarms(Recipe id)
        {
            return Set<Alarms>().Where(x => x.Recipe.Id == id.Id).ToList();
        }

        //OR can I recipe.getAlarms with collections OR .include(a=>a.Alarms)   (?) OR

        public void RemoveRecipe(Recipe id)
        {
            if (GetAlarms(id) != null)
            {
                Alarms.RemoveRange(GetAlarms(id));
            }
            Recipe.Remove(id);
        }
        public async void SaveAlarmAsync(Alarms item)
        {
            if (item.AlarmsId != 0) { 
                Alarms.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
            }
            else await Alarms.AddAsync(item).ConfigureAwait(false);
        }
        public async void SaveRecipeAsync(Recipe item)
        {
            if (item.Id != 0){
                Recipe.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
            }
            else await Recipe.AddAsync(item).ConfigureAwait(false);
        }
        public void DeleteAlarm(Alarms item)
        {
            Alarms.Remove(item);
        }
    }
}