using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHollywood.Services
{
    public class ServiceManager
    {
        public ServiceManager(RealTimeDB realTimeDB,FireBaseAuth fireBaseAuth, VkClient vkClient,Storage storage)
        {
            this.realTimeDB = realTimeDB;
            this.fireBaseAuth = fireBaseAuth;
            this.vkClient = vkClient;
            this.storage = storage;
        }

        private readonly RealTimeDB realTimeDB;
        private readonly FireBaseAuth fireBaseAuth;
        private readonly VkClient vkClient;
        private readonly Storage storage;

        public RealTimeDB RealTimeDB => realTimeDB;
        public FireBaseAuth FireBaseAuth => fireBaseAuth;
        public VkClient VkClient => vkClient;
        public Storage Storage => storage;
    }
}
