using Microsoft.AspNetCore.Mvc;

namespace JournalsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediasController : ControllerBase
    {
        [HttpGet]
        [HttpGet("{*mediaPath}")]
        [Route("api/medias/{*mediaPath}")]
        public ActionResult GetFile(string mediaPath)
        {
            string localFilePath;

            localFilePath = Path.Combine(AppContext.BaseDirectory, $"Assets/{mediaPath}");
            return PhysicalFile(localFilePath, "application/octet-stream", Path.GetFileName(localFilePath));
        }
    }
}
