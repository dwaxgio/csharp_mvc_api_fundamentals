using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API_FUNDAMENTALS.Models.WS
{
    // 10. Agrego AnimalViewModel para manejar los datos referentes a las operaciones crud del animal
    public class AnimalViewModel : SecurityViewModel // Clase hereda de SecurityViewModel, que contiene token
    {
        public int id { get; set; } // Se utiliza para el Delete y el Update
        public string name { get; set; }
        public int patas { get; set; }
    }
}