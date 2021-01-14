using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Get model from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetModel(string id)
        {
            return Ok(await this.BaseMongoRepository.GetById(id));
        }

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public virtual async Task<ActionResult> GetModelList()
        {
            return Ok(await this.BaseMongoRepository.GetList());
        }

        /// <summary>
        /// Post model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public virtual async Task<ActionResult> AddModel(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
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

        /// <summary>
        /// update model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public virtual async Task<ActionResult> UpdateModel(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.BaseMongoRepository.Update(model);
            return Ok();
        }

        /// <summary>
        /// Delete model from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public virtual async Task DeleteModel(string id)
        {
           await this.BaseMongoRepository.Delete(id);
        }
    }
}
