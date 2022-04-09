using DemoHollywood.Helpers;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DemoHollywood.Services
{
    public class FireBaseAuth
    {
        public FireBaseAuth(string webKey)
        {
            firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(webKey));
        }

        private readonly FirebaseAuthProvider firebaseAuth;


        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                var auth = await firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
                var freshAuth = await auth.GetFreshAuthAsync();

                string serializedAuth = JsonConvert.SerializeObject(freshAuth);
                Preferences.Set(Strings.AuthToken, serializedAuth);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<FirebaseAuth> GetProfileInfo()
        {
            FirebaseAuth savedFirebaseAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get(Strings.AuthToken, string.Empty));
            var refreshedAuth = await firebaseAuth.RefreshAuthAsync(savedFirebaseAuth);
            Preferences.Set(Strings.AuthToken, JsonConvert.SerializeObject(refreshedAuth));
            return savedFirebaseAuth;
        }

        public async Task<KeyValuePair<bool, string>> CreateUser(string email, string password, string displayName)
        {
            try
            {
                var createUser = await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password, displayName, false);
                var firebaseToken = createUser.FirebaseToken;
                return new KeyValuePair<bool, string>(true, firebaseToken);
            }
            catch (Exception)
            {
                return new KeyValuePair<bool, string>(false, string.Empty);
            }
        }
    }
}
