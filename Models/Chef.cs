using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Models
{
    public class Chef
    {
        public int Id { get; set; }
        public  string Name { get; set; }

        public DateTime Birth { get; set; }

        public string Image { get; set; }

        public string Information { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public int WorldCuisinesId { get; set; }
        public WorldCuisines WorldCuisines { get; set; }
    }
}
