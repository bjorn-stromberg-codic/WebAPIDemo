using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    public class TagController : ControllerBase
    {
        private static readonly List<Tag> _tags = new List<Tag>();

        private readonly ILogger<TagController> _logger;

        public TagController(ILogger<TagController> logger)
        {
            _logger = logger;
        }

        [Route("tags")]
        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return _tags;
        }

        [Route("tag")]
        [HttpPost]
        public CreatedResult Post(DTOTag dtoTag)
        {
            var host = HttpContext.Request.Host.Host;

            var tag = _tags.Where(tag => tag.Source == host).FirstOrDefault();
            if (tag != null)
            {
                tag.Message = dtoTag.Message;
                tag.Font = dtoTag.Font;
            }
            else
            {
                var rand = new Random();
                tag = new Tag()
                {
                    Source = host,
                    Message = dtoTag.Message,
                    Font = dtoTag.Font,
                    Rotation = rand.NextDouble() * 30.0
                };
                _tags.Add(tag);
            }
            
            return new CreatedResult("tags", tag);
        }
    }

    public class DTOTag
    {
        public string Message { get; set; }

        public string Font { get; set; }
    }
}
