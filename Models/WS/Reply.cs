using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API_FUNDAMENTALS.Models.WS
{
    // 1. Agrego clase Reply, la cual se implementará para dar respuesta a las peticiones recibidas
    public class Reply
    {
        // 1.2. Creo metodos
        public int result{ get; set; } // Atributo para indicar si petición es exitosa o no
        public object data { get; set; } // Indica que la respuesta se va a entregar sin ningun tipo, dado que se serializa para el envio
        public string message { get; set; } // Atributo para remitir mensaje en la respuesta
    }
}