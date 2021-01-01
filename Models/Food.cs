using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string Recipe { get; set; }
        public string Banner { get; set; }
        public double? Score { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
        public int  ChefId { get; set; }
        public Chef Chef { get; set; }
        

    }
}
