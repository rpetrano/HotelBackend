using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace HotelBackend.Models {
  public class Hotel
  {
      public int Id { get; set; }
      public required string Name { get; set; }
      public required decimal Price { get; set; }

      [Column(TypeName="geography")]
      [JsonConverter(typeof(GeoLocationConverter))]
      public required Point GeoLocation { get; set; }
  }
}