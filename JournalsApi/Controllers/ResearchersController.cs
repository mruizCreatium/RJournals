using JournalModels;
using JournalsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchersController : ControllerBase
    {
        private readonly JournalContext _context;

        public ResearchersController(JournalContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        // GET: api/Researchers/ForSubscriber/{subscriberId}
        // Gets all reasearchers indicating if the suscriber is following the researcher.
        // If a lastRequest parameter is passed, it will get the
        // Researchers posted before that date
        [HttpGet("ForSubscriber/{subscriberId}")]
        [Route("api/Researchers/ForSubscriber/{subscriberId}")]
        public async Task<ActionResult<IEnumerable<Researcher>>> ForSubscriber(long subscriberId)
        {
            string mediaServer = $"{Request.Scheme}://{Request.Host.Value}";

            var subscriptions = from s in _context.Subscriptions.Where(s => s.SubscriberId == subscriberId) select s.PublisherId;

            var lastResearchers = (from r in _context.Researchers
                                   join s in _context.Subscriptions on r.Id equals s.PublisherId
                                   into rsc
                                   from rs in rsc.DefaultIfEmpty()
                                   orderby r.Id
                                   select r.SetSubcriberFlag(rs != null, mediaServer)).Distinct();


            return await lastResearchers.ToListAsync();
        }



        // POST: api/Researchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Researcher>>> Subscribe([FromBody] Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return await ForSubscriber(subscription.SubscriberId);
        }

        // Delete: api/Researchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete]
        //[Route("api/Researchers/Unsubscribe/{subscriberId}/{researcherId}")]
        public async Task<ActionResult<IEnumerable<Researcher>>> Unsubscribe(long subscriberId, long researcherId)
        {
            _context.Subscriptions.Remove(new Subscription { PublisherId = researcherId, SubscriberId = subscriberId });
            await _context.SaveChangesAsync();
            return await ForSubscriber(subscriberId);
        }


    }
}
