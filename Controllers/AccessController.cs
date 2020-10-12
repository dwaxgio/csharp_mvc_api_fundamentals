using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVC_API_FUNDAMENTALS.Models.WS;
using MVC_API_FUNDAMENTALS.Models;

namespace MVC_API_FUNDAMENTALS.Controllers
{
    // 4. Se agrega controller de api
    public class AccessController : ApiController
    {
        [HttpGet] // Implemento data anotation httpget para invocar api
        public Reply HelloWorld() // Es el tipo de dato que se va a serializar, regresandola ya en formato json
        {            
            // Se instancia un ubjeto tipo Reply            
            Reply oReply = new Reply();
            oReply.result = 1; // El 1, indicara éxito
            oReply.message = "Hola Mundo Api";

            return oReply;
        }

        // 5. Se agrega nuevo metodo, luego crear la db, tabla y el ef, para autentificación por medio de post
        [HttpPost]
        public Reply Login([FromBody] AccessViewModel model) // Se va a recibir como parametro el AccessViewModel
        {
            Reply oReply = new Reply();
            oReply.result = 0;
            try
            {
                using (Cursomvc_apiEntities db = new Cursomvc_apiEntities())
                {
                    var lst = from d in db.user
                              where d.email == model.email && d.password == model.password && d.idEstatus == 1
                              select d;

                     if(lst.Count() > 0)
                     {
                        oReply.result = 1;
                        oReply.message = "Token asignado: ";
                        // Creo token
                        oReply.data = Guid.NewGuid().ToString(); // Genera identificador único, para ser implementado como token

                        // Para guardar token en la db
                        user oUser = lst.FirstOrDefault();
                        oUser.token = (string)oReply.data;
                        // Para editar valor en db
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                     }
                     else
                     {
                        oReply.message = "Datos incorrectos";
                     }
                }
            }
            catch (Exception ex)
            {
                
                oReply.message = "Hubo un error" + ex;
            }
            return oReply;
        }

    }
}
