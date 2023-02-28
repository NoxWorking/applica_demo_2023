/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 */

namespace applica_demo_2023.src.core.interfaces.services {
    using System.Data;
    using System.Threading.Tasks;

    internal interface IOpenDbfService {
        //Task<IEnumerable<Vde>> GetAll(string dbfPath);
        Task<DataTable> GetAllAsDataTableAsync(string dbfPath);
        Task<DataTable> GetAllAsDataTableAsync();
        //Task<Vde> GetDetails(string v0Document);
    }
}
