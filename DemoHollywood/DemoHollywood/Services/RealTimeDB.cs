using DemoHollywood.Helpers;
using DemoHollywood.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoHollywood.Services
{
    public sealed class RealTimeDB
    {
        public RealTimeDB(string key)
        {
            map = new Dictionary<Type, Action<Request>>();
            map.Add(typeof(ReueqstAppointments), RequestAppointmentPostAsync);
            map.Add(typeof(RequestRemoveAppointment), RequestRemoveAppointPostAsync);
            map.Add(typeof(RequestChangeUserProfile), RequestChangeUserData);
            client = new FirebaseClient(key);
        }


        private readonly Dictionary<Type, Action<Request>> map;
        private FirebaseClient client;

        public FirebaseClient Client => client;

        private async void RequestAppointmentPostAsync(Request request)
        {
            var content = JsonConvert.SerializeObject(request);
            await Client.Child(Strings.TableRequestAddAppoint).PostAsync(content);
        }
        private async void RequestRemoveAppointPostAsync(Request request)
        {
            var content = JsonConvert.SerializeObject(request);
            await Client.Child(Strings.TableRequestRemoveAppoint).PostAsync(content);
        }
        private async void RequestChangeUserData(Request request)
        {
            var content = JsonConvert.SerializeObject(request);
            await Client.Child(Strings.TableRequestChangeUserData).PostAsync(content);
        }
        public void Post(Request request)
        {
            map[request.GetType()].Invoke(request);
        }

        public async void Post(Message message)
        {
            var content = JsonConvert.SerializeObject(message);
            await Client.Child(Strings.TableChat).PostAsync(content);
        }

        public async Task<List<KeyValuePair<string,Appointment>>> GetAppointents(string tableName)
        {
            var collection = (await Client.Child(tableName).OnceAsync<Appointment>()).Select(element => new KeyValuePair<string, Appointment>(element.Key, element.Object)).ToList();
            return collection;
        }

        public async Task<KeyValuePair<string, UserProfile>> GetUserData(string tableName)
        {
            var collection = (await Client.Child(tableName).OnceAsync<UserProfile>()).Select(element => new KeyValuePair<string, UserProfile>(element.Key,element.Object)).ToList();
            return collection[0];
        }

        public async Task<List<string>> GetUsers(string tableName,string searchLine)
        {
            if (string.IsNullOrEmpty(searchLine))
                return new List<string>();
            var collcetion = (await Client.Child(tableName).OnceAsync<UserProfile>()).Select(element => element.Key).ToList();
            var result = collcetion.FindAll(element => element.Contains(searchLine));
            return result;
        }

        public async Task<KeyValuePair<bool, List<KeyValuePair<string, Appointment>>>> CanAddAppointment(string tableName,string appointKey,int duration)
        {
            var todayAppoints = await GetAppointents(tableName);
            var current = todayAppoints.FindIndex(element => element.Key == appointKey);

            List<KeyValuePair<string, Appointment>> appoints = new List<KeyValuePair<string, Appointment>>();

            for (int i = current; i < current + duration; i++)
            {
                if (todayAppoints[i].Value.IsBusy)
                {
                    appoints.Clear();
                    return new KeyValuePair<bool, List<KeyValuePair<string, Appointment>>>(false, appoints);
                }
                else
                    appoints.Add(todayAppoints[i]);
            }
            return new KeyValuePair<bool, List<KeyValuePair<string, Appointment>>>(true, appoints);
        }

        public async Task PostDocument(string path,Document document)
        {
            var content = JsonConvert.SerializeObject(document);
            await Client.Child(path).PostAsync(content);
        }

        public async Task RemoveDocument(string path,string fileName)
        {
            var dictionary = (await Client.Child(path).OnceAsync<Document>()).Select(element => new KeyValuePair<string, Document>(element.Key, element.Object)).ToList();
            var removableElement = dictionary.Find(element => element.Value.FileName == fileName);
            if (removableElement.Value != null)
                await Client.Child(path + "/" + removableElement.Key).DeleteAsync();
        }

        public async Task<List<Document>> GetDocuments(string path)
        {
            return (await Client.Child(path).OnceAsync<Document>()).Select(element => element.Object).ToList();
        }

        public async Task<IEnumerable<Service>> GetServices(string path)
        {
            return (await Client.Child(path).OnceAsync<Service>()).Select(element => element.Object);
        }
        public async Task PostDocument(string path, Service service)
        {
            var content = JsonConvert.SerializeObject(service);
            await Client.Child(path).PostAsync(content);
        }
    }
}
