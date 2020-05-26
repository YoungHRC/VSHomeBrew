using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

//https://stackoverflow.com/questions/55749717/entity-framework-cannot-bind-value-object-in-entity-constructor
namespace Tester.Models
{
    [Table("Alarms")]
    public class Alarms
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlarmsId { get; set; }
        public Boolean Head { get; set; }
        public Stage stageName { get; set; }
        public int NextAlarmId { get; set; }
        [ForeignKey("AlarmId")]
        public virtual Alarms NextAlarm { get; set; } //private
        [Required] public double Minutes { get; set; }
        public string IngredList { get; set; }
        public int RecipeId { get; set; }
        [ForeignKey("Id")]
        public virtual Recipe Recipe { get; set; } //private

        private Alarms(Stage stageName, double Minutes, string IngredList)
        {
            this.stageName = stageName;
            this.Minutes = Minutes;
            this.Head = false;
            if (IngredList != null)
                this.IngredList = IngredList;
            else
                this.IngredList = "ADD INGREDIENTS";
        }

        public Alarms(Stage stageName, double Minutes, string IngredList, Recipe recipe, Alarms next) : this(stageName, Minutes, IngredList)
        {
            setNextAlarmInfo(next);
            this.Recipe = recipe;
        }

        public enum Stage
        {
            mashing,
            lautering,
            boiling,
            fermenting
        }

        public void setNextAlarmInfo(Alarms next)
        {
            if (next != null)
            {
                this.NextAlarm = next;
                this.NextAlarmId = next.AlarmsId;
            }
            else Console.WriteLine(" Next is null. ");
        }

        public ArrayList getIngredArray()
        {
            string[] test = IngredList.Split(',').ToArray();
            ArrayList ingtList = new ArrayList(test);
            return ingtList;
        }

        public void setIngredList(ArrayList lists)
        {
            StringBuilder csvBuilder = new StringBuilder();
            foreach (string list in lists)
            {
                csvBuilder.Append(list);
                csvBuilder.Append(",");
            }
            string csv = csvBuilder.ToString();
            csv = csv.Substring(0, csv.Length - 1);
            this.IngredList = csv;
        }

        public string toString()
        {
            return "Alarm{ id:" + AlarmsId + ", Head " + Head + ", NEXT# " + NextAlarm.AlarmsId + " " + NextAlarm.toString() + ",  recId " + Recipe + ", ingred:" + IngredList;
        }
    }
}
