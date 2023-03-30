using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;

namespace Movies.Models
{
	public class MoviesInReq
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }
		public string searchToken { get; set; }
		public string imdbID { get; set; }
		public long processingTimeMS { get; set; }
		public DateTime	 timeStamp { get; set; }
		public string ipAdress { get; set; }
	
	}
}
