using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagDbContext _dbContext;

        public TagController(TagDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET /tags
        [Route("tags")]
        [HttpGet]
        public List<Tag> Get()
        {
            return _dbContext.Tags.ToList();
        }

        // POST /tag
        [Route("tag")]
        [HttpPost]
        public CreatedResult Post(DTOTag dtoTag)
        {
            var client = HttpContext.Connection.RemoteIpAddress.ToString();

            var tag = _dbContext.Tags.Where(tag => tag.Source == client).FirstOrDefault();
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
                    Source = client,
                    Message = dtoTag.Message,
                    Font = dtoTag.Font,
                    Rotation = rand.NextDouble() * 30.0
                };
                _dbContext.Tags.Add(tag);
            }

            _dbContext.SaveChanges();
            
            return new CreatedResult("tags", tag);
        }
    }

    public class DTOTag
    {
        public string Message { get; set; }

        public string Font { get; set; }
    }
}
