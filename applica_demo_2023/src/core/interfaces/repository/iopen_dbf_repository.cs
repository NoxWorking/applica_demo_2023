/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 */

namespace applica_demo_2023.src.core.interfaces.repository {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IOpenDbfRepository {

        //Task<IEnumerable<Vde>> GetAll(string dbfPath);
        Task<DataTable> GetAllAsDataTableAsync(string dbfPath);
        Task<DataTable> GetAllAsDataTableAsync();
        //Task<Vde> GetDetails(string v0Document);
    }
}
