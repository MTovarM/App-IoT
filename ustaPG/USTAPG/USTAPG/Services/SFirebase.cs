namespace USTAPG.Services
{
    using Firebase.Auth;
    using Firebase.Database;
    using Firebase.Database.Query;
    using System;
    using FireSharp;
    using FireSharp.Config;
    using FireSharp.Response;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using USTAPG.Models;
    using Xamarin.Forms;

    public class SFirebase : FirebaseAuth
    {
        #region Propiedades
        public string APIKey = "AIzaSyCczcx3VUDV58kVpOb6cUNdrdpCuJJKYK4";
        public static FirebaseAuthLink auth = null;
        public static string _userId = null;
        public string Email;
        public string Clave;
        #endregion

        #region Métodos
        public async Task<string> LoginWithEmail(bool createUser)
        {
            var authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(APIKey));
          
            try
            {
                if (createUser)
                {
                    auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Clave);
                }
                else
                {
                    auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Clave);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Firebase",
                    ex.Message,
                    "Aceptar");
                //System.Diagnostics.Debug.WriteLine(ex);
            }
            if (auth != null)
            {
                System.Diagnostics.Debug.WriteLine(auth.FirebaseToken);
                _userId = auth.User.LocalId;
                return auth.FirebaseToken;
            }
            else return null;
        }

        public Firebase.Database.FirebaseClient ClienteAutenticadoConEmail()
        {
            var firebaseClient = new Firebase.Database.FirebaseClient(
                  "https://ustaprogrado-default-rtdb.firebaseio.com/",
                  new FirebaseOptions
                  {
                      AuthTokenAsyncFactory = () => LoginWithEmail(false)
                  });
            return firebaseClient;
        }

        public async Task<List<measureTable>> GetMeasure(string _meter)
        {
            var firebase = ClienteAutenticadoConEmail();
           //await firebase.Child("").
            var Products = await firebase
                .Child("measureTable/" + _meter)
                .OnceAsync<measureTable>();

            List<measureTable> Measures = new List<measureTable>();

            foreach (var PR in Products)
            {
                Measures.Add(PR.Object);
            }
            return Measures;
        }

        public async Task<List<InfoTable>> GetInfo(string _meter)
        {
            var firebase = ClienteAutenticadoConEmail();
            
            var Products = await firebase
                .Child("InfoTable/" + _meter)
                .OnceAsync<InfoTable>();

            List<InfoTable> Measures = new List<InfoTable>();

            foreach (var PR in Products)
            {
                Measures.Add(PR.Object);
            }
            return Measures;
        }
        /*
        public async Task<string> Con()
        {
            FireSharp.Config.FirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "AIzaSyCczcx3VUDV58kVpOb6cUNdrdpCuJJKYK4",
                BasePath = "https://ustaprogrado-default-rtdb.firebaseio.com/"
            };
            FireSharp.FirebaseClient clienette = new FireSharp.FirebaseClient(config);
            try
            {
                FireSharp.Response.FirebaseResponse res = await clienette.GetAsync("measureTable");
                string r = res.ResultAs<string>();
                return r;
            }
            catch (Exception ex)
            {

                throw;
            }
            return "nothing";
        }
       */
        /*
         public async Task<List<measureTable>> GetMeasure()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("measureTable")
                .OnceAsync<measureTable>();

            List<measureTable> Measures = new List<measureTable>();

            foreach (var PR in Products)
            {
                Measures.Add(PR.Object);
            }
            return Measures;
        }
         */
        /*
        public async Task BorrarProducto(string code)
        {
            var firebase = ClienteAutenticadoConEmail();

            var lipro = await firebase
                .Child("Inventario")
                .OnceAsync<Producto>();

            FirebaseObject<Producto> ob = null;
            foreach (var p in lipro)
            {
                if (p.Object.Codigo == code)
                {
                    ob = p;
                    break;
                }
            }
            await firebase
                .Child("Inventario")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        public async Task BorrarCliente(string code)
        {
            var firebase = ClienteAutenticadoConEmail();

            var lipro = await firebase
                .Child("Clientes")
                .OnceAsync<Modelos.Cliente>();

            FirebaseObject<Modelos.Cliente> ob = null;
            foreach (var p in lipro)
            {
                if (p.Object.Nombre == code)
                {
                    ob = p;
                    break;
                }
            }
            await firebase
                .Child("Clientes")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        public async Task<List<Producto>> ObtenerProductos(string tabla)
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child(tabla)
                .OnceAsync<Producto>();

            List<Producto> ProductosFB = new List<Producto>();

            foreach (var PR in Products)
            {
                ProductosFB.Add(PR.Object);
            }
            return ProductosFB;
        }

        public async Task DeleteAllString(string _tabla)
        {
            var firebase = ClienteAutenticadoConEmail();

            await firebase
                .Child(_tabla)
                .DeleteAsync();
        }
         
        public async Task Guardar(string _tabla, Object _objeto)
        {
            var firebase = ClienteAutenticadoConEmail();
            try
            {
                var item = await firebase
                  .Child(_tabla)
                  .PostAsync(_objeto);
            }
            catch (Exception)
            {
            }
        }

        public async Task<List<Compra>> ObtenerCompras()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("Compras")
                .OnceAsync<Compra>();

            List<Compra> ComprasFB = new List<Compra>();

            foreach (var PR in Products)
            {
                ComprasFB.Add(PR.Object);
            }
            return ComprasFB;
        }

        public async Task<List<Venta>> ObtenerVentas()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("Ventas")
                .OnceAsync<Venta>();

            List<Venta> VentasFB = new List<Venta>();

            foreach (var PR in Products)
            {
                VentasFB.Add(PR.Object);
            }
            return VentasFB;
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("Clientes")
                .OnceAsync<Cliente>();

            List<Cliente> VentasFB = new List<Cliente>();

            foreach (var PR in Products)
            {
                VentasFB.Add(PR.Object);
            }
            return VentasFB;
        }

        public async Task<List<Proveedor>> ObtenerProveedores()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("Proveedores")
                .OnceAsync<Proveedor>();

            List<Proveedor> VentasFB = new List<Proveedor>();

            foreach (var PR in Products)
            {
                VentasFB.Add(PR.Object);
            }
            return VentasFB;
        }

        public async Task<List<string>> ObtenerMarcasTipos(string _tabla)
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child(_tabla)
                .OnceAsync<List<string>>();

            List<string> Strings = new List<string>();

            foreach (var PR in Products)
            {
                var u = PR.Object;
                foreach (var item in u)
                {
                    Strings.Add(item);
                }
            }
            return Strings;
        }

        public async Task SaveString(string _tabla, List<string> _list)
        {
            var firebase = ClienteAutenticadoConEmail();

            try
            {
                await firebase
                .Child(_tabla)
                .DeleteAsync();

                var item = await firebase
                  .Child(_tabla)
                  .PostAsync(_list);
            }
            catch (Exception)
            {
            }
        }
        */
        #endregion
    }
}
