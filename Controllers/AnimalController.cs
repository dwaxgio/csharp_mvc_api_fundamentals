using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
                    // Para respuesta a petición api
                    List<ListAnimalsViewModel> lst = List(DB); // Implementa metodo de helper, que trae resultado en lista, se le pasa como parametro el contexto

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

        //11. Agreo metodo para add
        [HttpPost]
        public Reply Add([FromBody] AnimalViewModel model)
        {
            Reply oReply = new Reply();
            oReply.result = 0;

            // Método de verificación del token
            if (!Verify(model.token))
            {
                oReply.message = "No autorizado";
                return oReply;
            }

            // Se puede agregar en esta sección validaciones, ej name es obligatorio... usa sección helper indicada más adelante con #
            if(!Validate(model)) // se envia model a Validate, si no es true, muestre error
            {
                oReply.message = error;
                return oReply;
            }

            // 11.2 se agrega try catch
            try
            {
                using(Cursomvc_apiEntities DB = new Cursomvc_apiEntities())
                {
                    // Se crea objeto de la entidad de la db para agregar a la db
                    animal oAnimal = new animal(); // Como se va a crear un nuevo objeto animal, se instancia como new animal el nuevo objeto
                    oAnimal.idState = 1;
                    oAnimal.name = model.name;
                    oAnimal.patas = model.patas;

                    // Se agrega objeto a db
                    DB.animal.Add(oAnimal);
                    DB.SaveChanges();

                    // Para respuesta a petición api
                    List<ListAnimalsViewModel> lst = List(DB); // Implementa metodo de helper, que trae resultado en lista, se le pasa como parametro el contexto

                    oReply.result = 1;
                    oReply.message = "Dato agregado: ";
                    oReply.data = lst; // Para que el cliente no tenga que solicitar nuevamente la data, se remite listado de entidad
                }

            }
            catch (Exception ex)
            {
                oReply.message = "Error, intente nuevamente..." + ex;                
            }
            return oReply;
        }

        // 12. Agrego método para editar
        [HttpPut]
        public Reply Edit([FromBody] AnimalViewModel model)
        {
            Reply oReply = new Reply();
            oReply.result = 0;

            // Método de verificación del token
            if (!Verify(model.token))
            {
                oReply.message = "No autorizado";
                return oReply;
            }

            // Se puede agregar en esta sección validaciones, ej name es obligatorio... usa sección helper indicada más adelante con #
            if (!Validate(model)) // se envia model a Validate, si no es true, muestre error
            {
                oReply.message = error;
                return oReply;
            }

            // 11.2 se agrega try catch
            try
            {
                using (Cursomvc_apiEntities DB = new Cursomvc_apiEntities())
                {
                    // Se crea objeto de la entidad de la db para agregar a la db
                    animal oAnimal = DB.animal.Find(model.id); // Acá cambia a .find en el edit
                    
                    oAnimal.name = model.name;
                    oAnimal.patas = model.patas;

                    // Se agrega objeto modificado a db
                    DB.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified; // Acà cambia para indicar a db cambio en objeto
                    DB.SaveChanges();

                    // Para respuesta a petición api
                    List<ListAnimalsViewModel> lst = List(DB); // Implementa metodo de helper, que trae resultado en lista, se le pasa como parametro el contexto

                    oReply.result = 1;
                    oReply.message = "Dato eliminado: ";
                    oReply.data = lst; // Para que el cliente no tenga que solicitar nuevamente la data, se remite listado de entidad
                }

            }
            catch (Exception ex)
            {
                oReply.message = "Error, intente nuevamente..." + ex;
            }
            return oReply;
        }

        // 13. Agrego método para eliminar
        [HttpDelete]
        public Reply Delete([FromBody] AnimalViewModel model)
        {
            Reply oReply = new Reply();
            oReply.result = 0;

            // Método de verificación del token
            if (!Verify(model.token))
            {
                oReply.message = "No autorizado";
                return oReply;
            }

            /* La validación se utiliza del lado del cliente, indicando si esta seguro de eliminar el registro
            //// Se puede agregar en esta sección validaciones, ej name es obligatorio... usa sección helper indicada más adelante con #
            //if (!Validate(model)) // se envia model a Validate, si no es true, muestre error
            //{
            //    oReply.message = error;
            //    return oReply;
            //}
            */

            // 11.2 se agrega try catch
            try
            {
                using (Cursomvc_apiEntities DB = new Cursomvc_apiEntities())
                {
                    // Se crea objeto de la entidad de la db para agregar a la db
                    animal oAnimal = DB.animal.Find(model.id); // Acá cambia a .find en el edit

                    oAnimal.idState = 0;

                    // Se agrega objeto modificado a db
                    DB.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified; // Acà cambia para indicar a db cambio en objeto
                    DB.SaveChanges();

                    // Para respuesta a petición api
                    List<ListAnimalsViewModel> lst = List(DB); // Implementa metodo de helper, que trae resultado en lista, se le pasa como parametro el contexto

                    oReply.result = 1;
                    oReply.message = "Dato editado: ";
                    oReply.data = lst; // Para que el cliente no tenga que solicitar nuevamente la data, se remite listado de entidad
                }

            }
            catch (Exception ex)
            {
                oReply.message = "Error, intente nuevamente..." + ex;
            }
            return oReply;
        }

        // 13. se emplea protocolo multipart para subir archivos
        [HttpPost]
        public async Task<Reply> Photo([FromUri] AnimalPictureViewModel model) // Asincrono que permite ejecutar hilos, para dar respuesta al cliente, mientras se procesa la solicitud. Recibe token
        {
            Reply oReply = new Reply();
            oReply.result = 0;

            // Se crea ruta para almacenar, correspondiente a carpeta App_Data, donde se almacenan archvios temporales cuando se cargan
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            // Se crea proveedor para poder leer el multipart
            var provider = new MultipartFormDataStreamProvider(root);  // Permite nutrir el provider, cuando el archivo sea subido por multipart

            if(!Verify(model.token))
            {
                oReply.message = " Error en autenticación...";
                return oReply;
            }

            // Validación de multipart
            if(!Request.Content.IsMimeMultipartContent()) // Si la solicitud no es multipart, entonces:
            {
                oReply.message = " No viene imagen...";
                return oReply;
            }

            // Lectura del archivo
            // Await indica que espere, hasta que se envíe el archivo del multipart
            await Request.Content.ReadAsMultipartAsync(provider);

            // Se crea dato tipo fileinfo que tiene información de la imagen
            FileInfo fileInfoPicture = null;

            // Foreach en caso de que se envíe más de una imagen
            foreach (MultipartFileData fileData in provider.FileData)
            {
                if(fileData.Headers.ContentDisposition.Name.Replace("\\","").Replace("\"","").Equals("picture"))
                {
                    fileInfoPicture = new FileInfo(fileData.LocalFileName); // Agrega archivo a ruta, siempre y cuando tenga como nombre picture
                }
            }

            // Si todo salió bien
            if(fileInfoPicture != null)
            {
                // Se pasa de tener imagen en temporal, a fileinfo, filestream y ahora a una matriz de bytes
                using (FileStream fs = fileInfoPicture.Open(FileMode.Open, FileAccess.Read))
                {
                    byte[] b = new byte[fileInfoPicture.Length]; // El tamaño de la matriz, lo da el lenght del fileinfopicture
                    UTF8Encoding temp = new UTF8Encoding(true); // Codificación

                    // El stream se va a escribir en la matriz
                    while (fs.Read(b, 0, b.Length) > 0) ; // Lee linea por linea la matriz de bytes generada de la imagen

                    // Se guarda con un TryCatch por si falla algo
                    try
                    {
                        using(Cursomvc_apiEntities DB = new Cursomvc_apiEntities())
                        {
                            // Como si fuera un update
                            var oAnimal = DB.animal.Find(model.id);
                            oAnimal.picture = b;

                            DB.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                            oReply.result = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        oReply.message = "Error procesando imagen: " + ex;
                        return oReply;
                    }
                }
            }

            return oReply;

        }

        // Sección helper para poner métodos que sólo sirven en esta clase y no en otra
        #region HELPERS

        // Mètodo para validar
        private bool Validate(AnimalViewModel model)
        {
            // Se valida acá el modelo
            if(model.name == "")
            {
                error = "El nombre es obligatorio";
            }

            return true;
        }

        // Metodo para retornar valores a lista
        private List<ListAnimalsViewModel> List(Cursomvc_apiEntities DB)
        {
            List<ListAnimalsViewModel> lst = (from d in DB.animal
             where d.idState == 1
             select new ListAnimalsViewModel // genera nuevo listado de animales de la tabla en la db
             {
                 name = d.name, // del atributo del AnimalViewModel, asignar lo que esta en la tabla de la db en campo name
                 patas = d.patas
             }).ToList();

            return lst;
        }

        #endregion

    }
}
