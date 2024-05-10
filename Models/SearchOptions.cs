// If the code was bigger, this would go into HotelBackend.Models.Configuration namespace
namespace HotelBackend.Models {
    public class SearchOptions
    {
        public double MaxDistance { get; set; } 
        public int Limit { get; set; }
        public int PageSize { get; set; }
    }
}