using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtTypeController : BaseMongoController<ArtType>
    {
         public ArtTypeController(ArtTypeRepository artTypeRepository): base(artTypeRepository)
        {
            
        }
    }
}
