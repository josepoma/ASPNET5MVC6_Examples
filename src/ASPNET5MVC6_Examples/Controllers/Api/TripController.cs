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
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = _repository.GetAllTripsWithStops();
            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);

                    _logger.LogInformation("Corecto");
                    _repository.AddTrip(newTrip);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fallo con el mensaje : {ex}");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }


    }
}
