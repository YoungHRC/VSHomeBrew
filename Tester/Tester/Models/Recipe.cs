using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tester.Models
{
    [Table("Recipe")]
    public class Recipe
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Style { get; set; }
        [MaxLength(4)] public double Abv { get; set; }
        [MaxLength(5)] public int Ibu { get; set; }

        //    [InverseProperty("Recipe")] public ICollection<Alarms> Alarms { get; set; }

        //public Recipe(string title, string style, double abv, int ibu)
        //{
        //    //     this.Id = Guid.NewGuid();
        //    this.Title = title;
        //    this.Style = style;
        //    this.Abv = abv;
        //    this.Ibu = ibu;
        //}

        ////TODO: Delete
        //private Recipe(string title, string style, double abv, int ibu, int ID)
        //{
        //    this.Id = ID;
        //    this.Title = title;
        //    this.Style = style;
        //    this.Abv = abv;
        //    this.Ibu = ibu;
        //}

        public override string ToString()
        {
            //   return "Recipe{" + "id=" + Id + ", title=" + Title +  "style" + Style + "a/i " + Abv + " " + Ibu + '}';
            return $"({Id}) {Title}, {Style}, {Abv}, {Ibu}";
        }
    }
}


//    public int getColor() {
//        switch (color) {
//            case 0:
//                return R.drawable.cream;
//            case 1:
//                return R.drawable.pale;
//            case 2:
//                return R.drawable.amber;
//            case 3:
//                return R.drawable.deepamber;
//            case 4:
//                return R.drawable.brwn;
//            case 5:
//                return R.drawable.dkbrwn;
//        }
//        return R.drawable.white;
//    }
