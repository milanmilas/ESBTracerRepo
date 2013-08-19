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
using ESBTracerDataAccess;
using System.Data.Entity;

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
            _container.RegisterType<IToolbarAView, ToolbarA>();
            _container.RegisterType<IToolbarAViewViewModel, ToolbarAViewViewModel>();

            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewModel, ContentAViewViewModel >();
           // _container.RegisterType<DbContext, AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext>();
            //_container.RegisterType<IRepository<Log>, EFRepository<Log>>();
            _container.RegisterInstance<IList<Log>>(this.GetFakeLogList());

            _container.RegisterType<IRepository<Log>, StubRepository<Log>>();

            var vmT = _container.Resolve<IToolbarAViewViewModel>();

            IRegion regionT = _regionManager.Regions[RegionNames.ToolbarRegion];
            regionT.Add(vmT.View);

            var vm = _container.Resolve<IContentAViewModel>();
            //var observablecollection =  new ObservableCollection<Log>() { new Log{LogId = 1, LogMessage = "Message1", Body = "Body1", Header = "Header1"},
            //                                            new Log{LogId = 2, LogMessage = "Message2", Body = "Body2", Header = "Header2"},
            //                                            };
            IRepository<Log> ctx = _container.Resolve<IRepository<Log>>();

            //vm.ctx = ctx;

            //vm.Logs = new ObservableCollection<Log>(ctx.Logs.Take(10).ToList());

            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            region.Add(vm.View);


        }

        private List<Log> GetFakeLogList()
        {
            return new List<Log>() { new Log{LogId = 1, LogMessage = "Message1", Body = "Body1", Header = "Header1"},
                                                        new Log{LogId = 2, LogMessage = "Message2", Body = "Body2", Header = "Header2"},
                                                        };
        }
    }
}
