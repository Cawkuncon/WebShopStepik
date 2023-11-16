namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public bool Comparsion = false;

        public bool Favorite = false;

        public Product(int id, int cost, string name, string description)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            Count = 1;
        }

        public Product(int id, int cost, string name, string description, int count)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}\n{Description}\n\n";
        }

    }
}
