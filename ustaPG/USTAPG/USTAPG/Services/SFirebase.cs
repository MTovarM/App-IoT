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
                //await Application.Current.MainPage.DisplayAlert(
                //  "Firebase",
                //ex.Message,
                //"Aceptar");
                //System.Diagnostics.Debug.WriteLine(ex);
                return null;
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

        public async Task<MeterTable> GetMeter(string _meter)
        {
            var firebase = ClienteAutenticadoConEmail();
            var Products = await firebase
                .Child("meterTable")
                .OnceAsync<MeterTable>();
            MeterTable M = new MeterTable();
            foreach (var PR in Products) 
            {
                if (PR.Key == _meter) 
                {
                    M = PR.Object;
                    break;
                }
            }
            return M;
        }

        public async Task<List<Sesion>> GetSesion(string _meter)
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("sesionTable/" + _meter)
                .OnceAsync<Sesion>();

            List<Sesion> Measures = new List<Sesion>();

            foreach (var PR in Products)
            {
                Measures.Add(PR.Object);
            }
            return Measures;
        }
        #endregion
    }
}
