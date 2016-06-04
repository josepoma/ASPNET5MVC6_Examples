using ASPNET5MVC6_Examples.Models;
using ASPNET5MVC6_Examples.ViewModel;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPNET5MVC6_Examples.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private ILogger<StopController> _logger;
        private IWorldRepository _repository;

        public StopController(IWorldRepository repository,ILogger<StopController> logger)
        {
            _repository = repository;
            _logger = logger;

        }

        public int HttpStatus { get; private set; }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var result = _repository.GetTripByName(tripName);
                if (result == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(result.Stops));
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fallo {tripName}");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json($"Error : {ex}");
            }
            
        }

        [HttpPost("")]
        public JsonResult Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Mapear a Entidad
                    var newstop = Mapper.Map<Stop>(vm);

                    //Visualizar coordenadas                    

                    //Guardando en BD
                    _repository.AddStop(tripName, newstop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newstop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Fallo : ",ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Fallo");
            }
            return Json(null);
        }
    }
}
