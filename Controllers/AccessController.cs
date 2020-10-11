using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVC_API_FUNDAMENTALS.Models.WS;

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
    }
}
