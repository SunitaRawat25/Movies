using Newtonsoft.Json;

namespace Movies.Models
{
	public class MovieRes
	{
		[JsonProperty]
		public string Title { get; set; }
		[JsonProperty]
		public string Year { get; set; }
		[JsonProperty]
		public string Rated { get; set; }
		[JsonProperty]
		public string Released { get; set; }
		[JsonProperty]
		public string Runtime { get; set; }
		[JsonProperty]
		public string Genre { get; set; }
		[JsonProperty]
		public string Director { get; set; }
		[JsonProperty]
		public string Writer { get; set; }
		[JsonProperty]
		public string Actors { get; set; }
		[JsonProperty]
		public string Plot { get; set; }
		[JsonProperty]
		public string Language { get; set; }
		[JsonProperty]
		public string Country { get; set; }
		[JsonProperty]
		public string Awards { get; set; }
		[JsonProperty]
		public string Poster { get; set; }
		[JsonProperty]
		public string ImdbRating { get; set; }
		[JsonProperty]
		public string ImdbVotes { get; set; }
		[JsonProperty]
		public string ImdbID { get; set; }
		

	}
}
