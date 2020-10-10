using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController :  BaseMongoController<Message>
    {
        //test deneme
        public MessageController(MessageRepository messageRepository) : base(messageRepository)
        {
        }


    }
}
