using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class Food
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public int IsAvailable { get; set; }
    }
  
}
