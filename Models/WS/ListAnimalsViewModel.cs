using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API_FUNDAMENTALS.Models.WS
{
    // 9. Creo AnimalsViewModel para retornar el listado de animales
    // Se recomienda crear el view Model, para evitar referencias circulares, para regresar sólo los campos que quiero consultar
    public class ListAnimalsViewModel
    {
        public string name { get; set; }
        public int? patas { get; set; } // ? porque en db el campo esta como allownull
    }
}