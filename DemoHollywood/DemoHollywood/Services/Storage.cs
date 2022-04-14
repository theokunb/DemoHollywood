using DemoHollywood.Helpers;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DemoHollywood.Services
{
    public class Storage
    {
        public Storage(string link)
        {
            firebaseStorage = new FirebaseStorage(link);
        }

        private FirebaseStorage firebaseStorage;




        public async Task<FirebaseStorageTask> PutDocument(string path,FileResult file)
        {
            return firebaseStorage.Child(path).Child(file.FileName).PutAsync(await file.OpenReadAsync());
        }

        public async Task RemoveDocument(string path, string fileName)
        {
            await firebaseStorage.Child(path).Child(fileName).DeleteAsync();
        }
    }
}
