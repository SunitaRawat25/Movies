using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.OpenApi.Writers;
using Movies.Data;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MovieUserController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MoviesContext _context;
		public MovieUserController(IHttpClientFactory httpClientFactory , MoviesContext context)
		{
			_httpClientFactory = httpClientFactory;
			_context = context;
		}


		// GET: MovieUserController/Details/5
		[HttpGet]
		public async Task<string> GetMovieDetails(string strmovie)
		{
			string strIPAdress = string.Empty;
			DateTime dtTimeStamp = DateTime.Now;
			var watch = new Stopwatch();
			watch.Start();


			var httpclient = _httpClientFactory.CreateClient("omdb");
			var httpResponseMessage = await httpclient.GetAsync(httpclient.BaseAddress.ToString()+ $"?apikey=c4460b3c&t={strmovie}");
			watch.Stop();
			//ip address of incoming req
			string strHeader = (HttpContext.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault());
			if (IPAddress.TryParse(strHeader, out IPAddress ip))
			{
				strIPAdress = ip.ToString();
			}
			//save data in database :
			string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
			var objDbData = JsonConvert.DeserializeObject<MoviesInReq>(apiResponse);
			MoviesInReq moviesInReq = new MoviesInReq()
			{
				imdbID = objDbData.imdbID,
				processingTimeMS = watch.ElapsedMilliseconds,
				searchToken = strmovie,
				ipAdress = strIPAdress,
				timeStamp = dtTimeStamp
			};
			_context.MoviesInReq.Add(moviesInReq);
			await _context.SaveChangesAsync();

			//return response
			var objResponseData = JsonConvert.DeserializeObject<MovieRes>(apiResponse);
			var jsonResponseData = JsonConvert.SerializeObject(objResponseData);
			return jsonResponseData;
		}


		
	}
}
