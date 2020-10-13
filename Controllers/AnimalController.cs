using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVC_API_FUNDAMENTALS.Models;
using MVC_API_FUNDAMENTALS.Models.WS;

namespace MVC_API_FUNDAMENTALS.Controllers
{
    // 7. Agrego AnimalController
    public class AnimalController : BaseController // Se hereda de BaseController, y a su vez de apiController
    {
        // Crear método que regrese el listado de los animales registrados en db
        [HttpPost]
        public Reply Get([FromBody] SecurityViewModel model)
        {
            Reply oReply = new Reply();
            oReply.result = 0;

            // Método de verificación del token
            if(!Verify(model.token))
            {
                oReply.message = "No autorizado";
                return oReply;
            }

            try
            {
                using(Cursomvc_apiEntities DB = new Cursomvc_apiEntities())
                {
                    List<ListAnimalsViewModel> lst = (from d in DB.animal
                                                     where d.idState == 1
                                                     select new ListAnimalsViewModel
                                                     {
                                                         name = d.name,
                                                         patas = d.patas
                                                     }).ToList();
                    oReply.data = lst;
                    oReply.result = 1;
                    oReply.message = "Datos enviados: ";
                }
            }
            catch (Exception ex)
            {
                oReply.message = " Ocurrió un error, inténtelo más tarde";
            }

            return oReply;
        }
    }
}
