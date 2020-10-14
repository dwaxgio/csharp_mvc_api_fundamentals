using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API_FUNDAMENTALS.Models.WS
{
    // 12. Creo model AnimalPictureViewModel
    public class AnimalPictureViewModel : SecurityViewModel // Hereda de SecurityViewModel para implementar validación con token
    {
        public int id { get; set; } // Indica el id del registro en la tabla de la db donde se va a agregar la foto
    }
}