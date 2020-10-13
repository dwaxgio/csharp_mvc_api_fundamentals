using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVC_API_FUNDAMENTALS.Models;

namespace MVC_API_FUNDAMENTALS.Controllers
{
    // 6. Creo BaseController
    public class BaseController : ApiController
    {
        // Método para verificar si Token es válido
        public bool Verify(string pToken)
        {
            using(Cursomvc_apiEntities DB = new Cursomvc_apiEntities())
            {
                if(DB.user.Where(d => d.token == pToken && d.idEstatus == 1).Count()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
