using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KnowledgeApi.Controllers
{
    public abstract class BaseMongoController<TModel> : ControllerBase
        where TModel : MongoBaseModel
    {
        public BaseMongoRepository<TModel> BaseMongoRepository { get; set; }

        public BaseMongoController(BaseMongoRepository<TModel> baseMongoRepository)
        {
            this.BaseMongoRepository = baseMongoRepository;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetModel(string id)
        {
            return Ok(await this.BaseMongoRepository.GetById(id));
        }

        [HttpGet]
        public virtual async Task<ActionResult> GetModelList()
        {
            return Ok(await this.BaseMongoRepository.GetList());
        }

        [HttpPost]
        public virtual async Task<ActionResult> AddModel(TModel model)
        {
            try
            {
                return Ok(await this.BaseMongoRepository.Create(model));
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
            
        }
        //[HttpPost]
        //public virtual async Task<ActionResult> AddNested(TModel model, TModel NModel)
        //{
        //    return Ok(await this.BaseMongoRepository.CreateNested(model, NModel));
        //}

        [HttpPut]
        public virtual async Task<ActionResult> UpdateModel( TModel model)
        {
            await this.BaseMongoRepository.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task DeleteModel(string id)
        {
           await this.BaseMongoRepository.Delete(id);
        }
    }
}
