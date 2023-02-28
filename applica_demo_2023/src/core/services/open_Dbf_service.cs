/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 */

namespace applica_demo_2023.src.core.services {
    using applica_demo_2023.src.core.data.configuration;
    using applica_demo_2023.src.core.data.repository;
    using applica_demo_2023.src.core.interfaces.repository;
    using applica_demo_2023.src.core.interfaces.services;
    using System.Data;
    using System.Threading.Tasks;

    internal class OpenDbfService : IOpenDbfService {
        private readonly DbfConfiguration MyDbfConfiguration;
        private readonly IOpenDbfRepository MyOpenDBFRepository;

        public OpenDbfService(DbfConfiguration myDbfConfiguration) {
            MyDbfConfiguration = myDbfConfiguration;
            MyOpenDBFRepository = new OpenDbfRepository(MyDbfConfiguration.MyDbfPath);
        }

        public Task<DataTable> GetAllAsDataTableAsync(string dbfPath) {
            return MyOpenDBFRepository.GetAllAsDataTableAsync(dbfPath);
        }

        public Task<DataTable> GetAllAsDataTableAsync() {
            return MyOpenDBFRepository.GetAllAsDataTableAsync();
        }

    }
}
