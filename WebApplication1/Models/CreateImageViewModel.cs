namespace WebApplication1.Models
{
    public class CreateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PathImage { get; set; }
        public IFormFile formFile { get; set; }
    }
}
