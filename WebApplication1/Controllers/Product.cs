namespace WebApplication1.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }

        public Product(int id, int cost, string name, string description)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}\n\n";
        }

    }
}
