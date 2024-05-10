// If the code was bigger, this would go into HotelBackend.Models.Results namespace
namespace HotelBackend.Models {
  public class HotelSearchResult
  {
      public required Hotel Hotel { get; set; }
      public double Score { get; set; }
  }
}