/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 */

namespace applica_demo_2023.src.core.viewModels {
    using Microsoft.Extensions.DependencyInjection;
    public class LocatorVm {
        public static MainVm MainVm => App.ServiceProvider!.GetRequiredService<MainVm>();
    }
}
