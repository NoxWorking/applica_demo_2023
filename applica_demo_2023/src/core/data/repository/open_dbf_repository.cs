﻿/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 */

namespace applica_demo_2023.src.core.data.repository {
    using applica_demo_2023.src.core.interfaces.repository;
    using NDbfReader;
    using System.Data;
    using System.Text;
    using System.Threading.Tasks;

    internal class OpenDbfRepository : IOpenDbfRepository {

        private string MyDbfPath { get; set; }
        private DataTable MyDataTable { get; set; }

        public OpenDbfRepository(string myDbfPath) {
            MyDbfPath = myDbfPath;
            MyDataTable = new DataTable();
        }

        protected Table DbfConnection() {
            return Table.Open(MyDbfPath);
        }

        public async Task<DataTable> GetAllAsDataTableAsync(string dbfPath) {
            MyDbfPath = dbfPath;
            Table db = DbfConnection();
            MyDataTable = await db.AsDataTableAsync();
            return MyDataTable;
        }

        public async Task<DataTable> GetAllAsDataTableAsync() {
            Table db = DbfConnection();
            MyDataTable = await db.AsDataTableAsync();
            return MyDataTable;
        }

    }
}
