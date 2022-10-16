using JournalModels;
using JournalsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController : ControllerBase
    {
        private readonly JournalContext _context;

        public JournalsController(JournalContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        // GET: api/Journals/RecentsForResearcher/{subscriberId}/{lastRequest?}
        // Gets journals posted for a specific subscriber.
        [HttpGet("RecentsForResearcher/{subscriberId}")]
        [Route("api/journals/RecentsForResearcher/{subscriberId}")]
        public async Task<ActionResult<IEnumerable<Journal>>> GetRecentForResearcher(long subscriberId)
        {
            string mediaServer = $"{Request.Scheme}://{Request.Host.Value}";
            var subscriptions = (from s in _context.Subscriptions.Where(s => s.SubscriberId == subscriberId) select s.PublisherId).ToList();
            subscriptions.Add(subscriberId);

            var lastJournals = (from j in _context.Journals.Where(j => subscriptions.Contains(j.ResearcherId))
                                join r in _context.Researchers on j.ResearcherId equals r.Id
                                orderby j.PostDate descending
                                select j.SetResearcherInfo(r, mediaServer)).Take(10).Distinct();


            return await lastJournals.ToListAsync();
        }


        [HttpGet("ByResearcher/{researcherId}")]
        [Route("api/journals/ByResearcher/{researcherId}")]
        public async Task<ActionResult<IEnumerable<Journal>>> GetByResearcher(long researcherId)
        {

            var lastJournals = from j in _context.Journals.Where(j => j.ResearcherId == researcherId)
                               orderby j.PostDate descending
                               select j;


            return await lastJournals.ToListAsync();
        }


        // POST: api/Journals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostJournal(IFormFile file, [FromForm] string title, [FromForm] string publisherId)
        {

            if (file.Length > 0)
            {
                string fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));
                string filePath = Path.Combine(AppContext.BaseDirectory, $"Assets\\journals\\{fileName}");

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                _context.Journals.Add(new Journal
                {
                    FileUrl = $"journals/{fileName}",
                    PostDate = DateTime.Now,
                    Title = title,
                    ResearcherId = long.Parse(publisherId)
                });

                await _context.SaveChangesAsync();
            }


            return Ok();
        }

    }
}
