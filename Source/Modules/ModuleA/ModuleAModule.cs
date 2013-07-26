using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ESBInfrastructureLibrary;
using System.Collections.ObjectModel;
using ESBTracerDataAccess.Models;

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
            /*_container.RegisterType<ToolbarA>();*/
            _container.RegisterType<IToolbarAView, ToolbarA>();
            _container.RegisterType<IToolbarAViewViewModel, ToolbarAViewViewModel>();

            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewModel, ContentAViewViewModel >();
/*            _container.RegisterType<IContentAViewModel, ContentAViewViewModel>();*/
            /*_regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarA));*/

            var vmT = _container.Resolve<IToolbarAViewViewModel>();

            IRegion regionT = _regionManager.Regions[RegionNames.ToolbarRegion];
            regionT.Add(vmT.View);

            var vm = _container.Resolve<IContentAViewModel>();
            vm.Message = "First View";
            var observablecollection =  new ObservableCollection<Log>() { new Log{LogId = 1, LogMessage = "Message1", Body = "Body1", Header = "Header1"},
                                                        new Log{LogId = 2, LogMessage = "Message2", Body = "Body2", Header = "Header2"},
                                                        };
            AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext ctx = new AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext();



            vm.ctx = ctx;

            vm.Logs = new ObservableCollection<Log>(ctx.Logs.Take(10).ToList());

            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            region.Add(vm.View);

            //create second View
            //var vm2 = _container.Resolve<IContentAViewModel>();
            //vm2.Message = "Second View";

            //region.Deactivate(vm.View);
            //region.Add(vm2.View);
        }
    }
}
