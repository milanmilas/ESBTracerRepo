using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ESBInfrastructureLibrary;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleAModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<ToolbarA>();
            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewModel, ContentAViewViewModel >();
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarA));

            var vm = _container.Resolve<IContentAViewModel>();
            vm.Message = "First View";
            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            region.Add(vm.View);

            //create second View
            var vm2 = _container.Resolve<IContentAViewModel>();
            vm2.Message = "Second View";

            region.Deactivate(vm.View);
            region.Add(vm2.View);
        }
    }
}
