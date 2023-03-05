/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 */

namespace applica_demo_2023.src.core.data.configuration {
    internal class DbfConfiguration {

        public string MyDbfPath { get; }

        public DbfConfiguration(string myDbfPath) => MyDbfPath = myDbfPath;
        
    }
}
