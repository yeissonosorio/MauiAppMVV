using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMVVM.controller
{
    public class ControllerFirebase
    {
        public async Task<List<Models.Productos>> GetListProductsFromFirebase()
        {
            var productList = new List<Models.Productos>();

            // Aquí debes realizar la lógica para obtener los productos desde Firebase
            // Por ejemplo:
            FirebaseClient firebaseClient = new FirebaseClient("https://tarea2-e4701-default-rtdb.firebaseio.com/");
            var firebaseProducts = await firebaseClient.Child("Productos").OnceAsync<Models.Productos>();

            foreach (var firebaseProduct in firebaseProducts)
            {
                productList.Add(new Models.Productos
                {
                    id = firebaseProduct.Key,
                    Nombre = firebaseProduct.Object.Nombre,
                    Precio = firebaseProduct.Object.Precio,
                    Foto = firebaseProduct.Object.Foto
                }); ;
            }

            return productList;
        }
    }
}
