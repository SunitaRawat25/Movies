using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Authentication;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AdminAPIKeyAuthentication))]
    public class AdminController : ControllerBase
    {
        private readonly MoviesContext _context;

        public AdminController(MoviesContext context)
        {
            _context = context;
        }
        //get all data
        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesInReq>>> GetMoviesInReq()
        {
          if (_context.MoviesInReq == null)
          {
              return NotFound();
          }
            return await _context.MoviesInReq.ToListAsync();
        }

        //get data based on id
        // GET: api/Admin/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MoviesInReq>> GetMoviesInReq(int id)
        {
          if (_context.MoviesInReq == null)
          {
              return NotFound();
          }
            var moviesInReq = await _context.MoviesInReq.FindAsync(id);

            if (moviesInReq == null)
            {
                return NotFound();
            }

            return moviesInReq;
        }

		//get data based on date range
		// GET: api/Admin/MoviesByRangedtBeginDate=01-01-2012T00:00:00Z&dtBeginDate=01-31-2012T00:00:00Z)
		[HttpGet]
		[Route("MoviesByRange/{dtBeginDate:datetime}&{dtEndDate:datetime}")]
		public async Task<ActionResult<IEnumerable<MoviesInReq>>> GetMoviesByDateRange(DateTime dtBeginDate, DateTime dtEndDate)
		{
			if (_context.MoviesInReq == null)
			{
				return NotFound();
			}
			var moviesInReq = await _context.MoviesInReq.Where(d => d.timeStamp >= dtBeginDate && d.timeStamp <= dtEndDate).ToListAsync();

			if (moviesInReq == null)
			{
				return NotFound();
			}

			return moviesInReq;
		}

		//get count of records returned for a date search
		// GET: api/Admin/MoviesByDate/dtSearchDate=01-01-2012T00:00:00Z
		[HttpGet]
        [Route("MoviesByDate/{dtSearchDate:datetime}")]
		public async Task<int> GetMoviesByDate(DateTime dtSearchDate)
		{
			
			var moviesDateRangeCount = await _context.MoviesInReq.Where(d => d.timeStamp.Date == dtSearchDate).ToListAsync();

			if (moviesDateRangeCount == null)
			{
				return 0;
			}

			return moviesDateRangeCount.Count();
		}

        //get count of records returned for movie name search
		// GET: api/Admin/strName
		[HttpGet]
		[Route("MoviesByTokenName/{strName}")]
		public async Task<int> GetMoviesInReqNameCount(string strName)
		{

			var moviesTitleCount = await _context.MoviesInReq.Where(d => d.searchToken == strName).ToListAsync();

			if (moviesTitleCount == null)
			{
				return 0;
			}

			return moviesTitleCount.Count();
		}

		// DELETE: api/Admin/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoviesInReq(int id)
        {
            if (_context.MoviesInReq == null)
            {
                return NotFound();
            }
            var moviesInReq = await _context.MoviesInReq.FindAsync(id);
            if (moviesInReq == null)
            {
                return NotFound();
            }

            _context.MoviesInReq.Remove(moviesInReq);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesInReqExists(int id)
        {
            return (_context.MoviesInReq?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
