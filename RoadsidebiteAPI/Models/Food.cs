namespace RoadsidebiteAPI.Models
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
    }
    public class FoodCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
