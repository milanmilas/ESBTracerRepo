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
            //_container.RegisterType<ICompositeFilterService, CompositeFilterService>();

            _container.RegisterInstance<ICompositeFilterService>(new CompositeFilterService());

            _container.RegisterType<IToolbarAView, ToolbarA>();
            _container.RegisterType<IToolbarAViewViewModel, ToolbarAViewViewModel>();

            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewModel, ContentAViewViewModel >();

            //_container.RegisterType<DbContext, AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext>();
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


            _container.RegisterType<IFilterA, FilterA>();
            _container.RegisterType<IFilterAViewViewModel, FilterAViewViewModel>();

            var vmF = _container.Resolve<IFilterAViewViewModel>();

            _regionManager.AddToRegion(RegionNames.FilterRegion, vmF.View);
        }

        private List<Log> GetFakeLogList()
        {
            return new List<Log>() { new Log{LogId = 134246, LogMessage = "Exception has occured", Body = "Body1", Header = "Header1"},
                                                        new Log{LogId = 2, LogMessage = "Message2", Body = "Body2", Header = "Header2"},
                                                        };
/*

            134246	01/08/2013 12:23:34	HRNHSP-HRSB-CreateDutyCxfTransformer	Exception has occured	requestMethod=IVR, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, JMSTimestamp=1375356214217, JMSDeliveryMode=2, StaffBankAction=CreateShiftRequest, wardCode=WD60, endTime=19:15:00, CamelJmsDeliveryMode=2, InterfaceEventsTime=2013-08-01T11:52:43, onCall=false, startTime=07:00:00, HealthRosterSession=.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/, BookingReferenceNumber=, mappedTrustId=RR8, JMSReplyTo=null, agencyId=, gender=, secondaryAssignmentCodeDuration=, JMSXGroupID=null, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-75:1:1:1:2, ExceptionStackTrace=java.lang.Exception: This is forced exception
 at sun.reflect.NativeConstructorAccessorImpl.newInstance0(Native Method)
 at sun.reflect.NativeConstructorAccessorImpl.newInstance(NativeConstructorAccessorImpl.java:39)
 at sun.reflect.DelegatingConstructorAccessorImpl.newInstance(DelegatingConstructorAccessorImpl.java:27)
 at java.lang.reflect.Constructor.newInstance(Constructor.java:513)
 at org.apache.aries.blueprint.utils.ReflectionUtils.newInstance(ReflectionUtils.java:329)
 at org.apache.aries.blueprint.container.BeanRecipe.newInstance(BeanRecipe.java:962)
 at org.apache.aries.blueprint.container.BeanRecipe.getInstance(BeanRecipe.java:331)
 at org.apache.aries.blueprint.container.BeanRecipe.internalCreate2(BeanRecipe.java:806)
 at org.apache.aries.blueprint.container.BeanRecipe.internalCreate(BeanRecipe.java:787)
 at org.apache.aries.blueprint.di.AbstractRecipe$1.call(AbstractRecipe.java:79)
 at java.util.concurrent.FutureTask$Sync.innerRun(FutureTask.java:303)
 at java.util.concurrent.FutureTask.run(FutureTask.java:138)
 at org.apache.aries.blueprint.di.AbstractRecipe.create(AbstractRecipe.java:88)
 at org.apache.aries.blueprint.container.BlueprintRepository.createInstances(BlueprintRepository.java:245)
 at org.apache.aries.blueprint.container.BlueprintRepository.createAll(BlueprintRepository.java:183)
 at org.apache.aries.blueprint.container.BlueprintContainerImpl.instantiateEagerComponents(BlueprintContainerImpl.java:649)
 at org.apache.aries.blueprint.container.BlueprintContainerImpl.doRun(BlueprintContainerImpl.java:356)
 at org.apache.aries.blueprint.container.BlueprintContainerImpl.run(BlueprintContainerImpl.java:255)
 at org.apache.aries.blueprint.container.BlueprintExtender.checkBundle(BlueprintExtender.java:325)
 at org.apache.aries.blueprint.container.BlueprintExtender.bundleChanged(BlueprintExtender.java:243)
 at org.apache.aries.blueprint.container.BlueprintExtender$BlueprintBundleTrackerCustomizer.modifiedBundle(BlueprintExtender.java:471)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$Tracked.customizerModified(BundleHookBundleTracker.java:198)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$Tracked.customizerModified(BundleHookBundleTracker.java:128)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$AbstractTracked.track(BundleHookBundleTracker.java:468)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$Tracked.bundleChanged(BundleHookBundleTracker.java:161)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$BundleEventHook.event(BundleHookBundleTracker.java:117)
 at org.apache.felix.framework.util.SecureAction.invokeBundleEventHook(SecureAction.java:1103)
 at org.apache.felix.framework.util.EventDispatcher.createWhitelistFromHooks(EventDispatcher.java:696)
 at org.apache.felix.framework.util.EventDispatcher.fireBundleEvent(EventDispatcher.java:484)
 at org.apache.felix.framework.Felix.fireBundleEvent(Felix.java:4479)
 at org.apache.felix.framework.Felix$4.run(Felix.java:2019)
 at org.apache.felix.framework.Felix$5.run(Felix.java:2061)
 at java.util.concurrent.Executors$RunnableAdapter.call(Executors.java:439)
 at java.util.concurrent.FutureTask$Sync.innerRun(FutureTask.java:303)
 at java.util.concurrent.FutureTask.run(FutureTask.java:138)
 at java.util.concurrent.ThreadPoolExecutor$Worker.runTask(ThreadPoolExecutor.java:886)
 at java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:908)
 at java.lang.Thread.run(Thread.java:662)
, HealthRosterUserId=esb, JMSExpiration=0, staffId=, TrustConfigurationId=1, Expires=-1, Server=Microsoft-IIS/7.5, CookieSource=Database, gradeType=RN, X-Powered-By=ASP.NET, latestTimestamp=2013-08-01T12:22:49.6300000, gradeCategory=Registered, requestorWebUserId=InterfaceRR8, JMSPriority=4, locationId=LGI, CamelHttpQuery=$filter=HealthRosterTrust eq 'RR8' and HealthRosterWard eq null and HealthRosterGradeType eq 'RN' and HealthRosterGradeCategory eq 'Registered' and (not AssignmentSkillsMappings/any())&$expand=AssignmentSkillsMappings, shiftType=, content-type=text/xml; charset=utf-8, noAgency=, wardId=WD60, Date=Thu, 01 Aug 2013 11:23:34 GMT, CamelHttpResponseCode=200, CamelHttpUri=SetBookingReference, Cache-Control=private, max-age=0, JMSDestination=queue://MSSQL-Logging, X-AspNet-Version=4.0.30319, JMSCorrelationID=null, routeIdLog=HRNHSP-HRSB-CreateDutyCxfTransformer, Accept=application/atom+xml;type=feed;charset=utf-8, EventType=DutySentToBank, JMSType=null, HealthRosterServer=http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5, LogMessage=Exception has occured, reasonCode=B10, X-Content-Type-Options=nosniff, requestId=382966, authorizationCode=, secondaryAssignmentCode=, JMSRedelivered=false, HealthRosterPassword=esbesb, assignmentCode=, trustId=RR8, isLocked=false, Content-Length=602, CamelHttpMethod=POST, Pragma=no-cache, jobId=Deep Clean, DataServiceVersion=1.0;, ExceptionMessage=This is forced exception, jobCode=B10, StaffBankResponse=ErrorResponseType, 	<ExecuteResponse xmlns="http://allocatesoftware.com/IVR" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"><ExecuteResult xsi:type="ErrorResponseType"><RequestId>382966</RequestId><ErrorCode>7</ErrorCode><ErrorDescription>Unknown Ward ID</ErrorDescription></ExecuteResult></ExecuteResponse>	ID-LON-DEV-MIMI-23733-1375351788466-70-37	ID-LON-DEV-MIMI-23733-1375351788466-70-36	This is forced exception	java.lang.Exception: This is forced exception
 at sun.reflect.NativeConstructorAccessorImpl.newInstance0(Native Method)
 at sun.reflect.NativeConstructorAccessorImpl.newInstance(NativeConstructorAccessorImpl.java:39)
 at sun.reflect.DelegatingConstructorAccessorImpl.newInstance(DelegatingConstructorAccessorImpl.java:27)
 at java.lang.reflect.Constructor.newInstance(Constructor.java:513)
 at org.apache.aries.blueprint.utils.ReflectionUtils.newInstance(ReflectionUtils.java:329)
 at org.apache.aries.blueprint.container.BeanRecipe.newInstance(BeanRecipe.java:962)
 at org.apache.aries.blueprint.container.BeanRecipe.getInstance(BeanRecipe.java:331)
 at org.apache.aries.blueprint.container.BeanRecipe.internalCreate2(BeanRecipe.java:806)
 at org.apache.aries.blueprint.container.BeanRecipe.internalCreate(BeanRecipe.java:787)
 at org.apache.aries.blueprint.di.AbstractRecipe$1.call(AbstractRecipe.java:79)
 at java.util.concurrent.FutureTask$Sync.innerRun(FutureTask.java:303)
 at java.util.concurrent.FutureTask.run(FutureTask.java:138)
 at org.apache.aries.blueprint.di.AbstractRecipe.create(AbstractRecipe.java:88)
 at org.apache.aries.blueprint.container.BlueprintRepository.createInstances(BlueprintRepository.java:245)
 at org.apache.aries.blueprint.container.BlueprintRepository.createAll(BlueprintRepository.java:183)
 at org.apache.aries.blueprint.container.BlueprintContainerImpl.instantiateEagerComponents(BlueprintContainerImpl.java:649)
 at org.apache.aries.blueprint.container.BlueprintContainerImpl.doRun(BlueprintContainerImpl.java:356)
 at org.apache.aries.blueprint.container.BlueprintContainerImpl.run(BlueprintContainerImpl.java:255)
 at org.apache.aries.blueprint.container.BlueprintExtender.checkBundle(BlueprintExtender.java:325)
 at org.apache.aries.blueprint.container.BlueprintExtender.bundleChanged(BlueprintExtender.java:243)
 at org.apache.aries.blueprint.container.BlueprintExtender$BlueprintBundleTrackerCustomizer.modifiedBundle(BlueprintExtender.java:471)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$Tracked.customizerModified(BundleHookBundleTracker.java:198)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$Tracked.customizerModified(BundleHookBundleTracker.java:128)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$AbstractTracked.track(BundleHookBundleTracker.java:468)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$Tracked.bundleChanged(BundleHookBundleTracker.java:161)
 at org.apache.aries.util.tracker.hook.BundleHookBundleTracker$BundleEventHook.event(BundleHookBundleTracker.java:117)
 at org.apache.felix.framework.util.SecureAction.invokeBundleEventHook(SecureAction.java:1103)
 at org.apache.felix.framework.util.EventDispatcher.createWhitelistFromHooks(EventDispatcher.java:696)
 at org.apache.felix.framework.util.EventDispatcher.fireBundleEvent(EventDispatcher.java:484)
 at org.apache.felix.framework.Felix.fireBundleEvent(Felix.java:4479)
 at org.apache.felix.framework.Felix$4.run(Felix.java:2019)
 at org.apache.felix.framework.Felix$5.run(Felix.java:2061)
 at java.util.concurrent.Executors$RunnableAdapter.call(Executors.java:439)
 at java.util.concurrent.FutureTask$Sync.innerRun(FutureTask.java:303)
 at java.util.concurrent.FutureTask.run(FutureTask.java:138)
 at java.util.concurrent.ThreadPoolExecutor$Worker.runTask(ThreadPoolExecutor.java:886)
 at java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:908)
 at java.lang.Thread.run(Thread.java:662)
	ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134245	01/08/2013 12:23:15	HRNHSP-HRSB-CreateDutyCxfTransformer	Processing Exchange	Pragma=no-cache, CookieSource=Database, mappedTrustId=RR8, wardCode=WD60, trustId=RR8, JMSDestination=queue://MSSQL-Logging, Accept=application/atom+xml;type=feed;charset=utf-8, locationId=LGI, Content-Length=602, HealthRosterUserId=esb, JMSDeliveryMode=2, DataServiceVersion=1.0;, jobCode=B10, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, wardId=A %26 E, gradeCategory=Registered, JMSXGroupID=null, EventType=DutySentToBank, InterfaceEventsTime=2013-08-01T11:52:43, X-Powered-By=ASP.NET, JMSType=null, TrustConfigurationId=1, routeIdLog=HRNHSP-HRSB-CreateDutyCxfTransformer, CamelHttpQuery=$filter=HealthRosterTrust eq 'RR8' and HealthRosterWard eq null and HealthRosterGradeType eq 'RN' and HealthRosterGradeCategory eq 'Registered' and (not AssignmentSkillsMappings/any())&$expand=AssignmentSkillsMappings, HealthRosterServer=http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5, JMSReplyTo=null, Content-Type=application/atom+xml;type=feed;charset=utf-8, Cache-Control=no-cache, CamelHttpMethod=GET, Expires=-1, LogMessage=Processing Exchange, gradeType=RN, Server=Microsoft-IIS/7.5, JMSExpiration=0, JMSPriority=4, HealthRosterPassword=esbesb, BookingReferenceNumber=, CamelJmsDeliveryMode=2, jobId=Deep Clean, assignmentCode=, X-Content-Type-Options=nosniff, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-75:1:1:1:1, HealthRosterSession=.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/, JMSCorrelationID=null, JMSTimestamp=1375356194840, CamelHttpResponseCode=200, latestTimestamp=2013-08-01T12:22:49.6300000, RequestId=12111, JMSRedelivered=false, X-AspNet-Version=4.0.30319, Date=Thu, 01 Aug 2013 11:23:14 GMT, 	<entry>
<id>http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5/OData/TempStaffing.svc/TempStaffInterfaceEvents(12111)</id>
<category scheme="http://schemas.microsoft.com/ado/2007/08/dataservices/scheme" term="TempStaffing.TempStaffInterfaceEvent"/>
<link href="TempStaffInterfaceEvents(12111)" rel="edit" title="TempStaffInterfaceEvent"/>
<title/>
<updated>2013-08-01T11:23:13Z</updated>
<author>
<name/>
</author>
<content type="application/xml">
<properties>
<ID type="Edm.Int32">12111</ID>
<OccuredOn type="Edm.DateTime">2013-08-01T12:22:49.63</OccuredOn>
<EventType>DutySentToBank</EventType>
<TempStaffingInterface>NHSp</TempStaffingInterface>
<BookingReference null="true"/>
<RequestDetails>&lt;TempStaffingODataRequestDetails ActualEnd="0001-01-01T00:00:00" ActualRestTime="0" ActualStart="0001-01-01T00:00:00" AssignmentCode="" DutyID="382966" FulfilmentLockedToNHSpWebPortal="False" PlannedEnd="2013-08-02T19:15:00" PlannedRestTime="45" PlannedStart="2013-08-02T07:00:00" RequiredGradeTypeCategoryName="Registered" RequiredGradeTypeName="RN" ShiftType="Day" TempStaffBookingReasonName="B10" TrustID="RR8" UnitCode="WD60"/&gt;</RequestDetails>
</properties>
</content>
</entry>	ID-LON-DEV-MIMI-23733-1375351788466-70-32	ID-LON-DEV-MIMI-23733-1375351788466-70-31			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134244	01/08/2013 12:23:14	HRNHSP-HRSB-CreateDutyEnricher	Processing Exchange	CamelJmsDeliveryMode=2, X-Powered-By=ASP.NET, wardId=A %26 E, HealthRosterServer=http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5, TrustConfigurationId=1, Expires=-1, latestTimestamp=2013-08-01T12:22:49.6300000, locationId=LGI, Cache-Control=no-cache, JMSReplyTo=null, jobId=Deep Clean, JMSType=null, CookieSource=Database, X-AspNet-Version=4.0.30319, HealthRosterSession=.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/, gradeCategory=Registered, JMSDestination=queue://MSSQL-Logging, DataServiceVersion=1.0;, routeIdLog=HRNHSP-HRSB-CreateDutyEnricher, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-73:1:1:1:1, JMSDeliveryMode=2, JMSPriority=4, RequestId=12111, JMSRedelivered=false, wardCode=WD60, JMSCorrelationID=null, X-Content-Type-Options=nosniff, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, Content-Type=application/atom+xml;type=feed;charset=utf-8, InterfaceEventsTime=2013-08-01T11:52:43, JMSXGroupID=null, CamelHttpResponseCode=200, EventType=DutySentToBank, Accept=application/atom+xml;type=feed;charset=utf-8, LogMessage=Processing Exchange, Server=Microsoft-IIS/7.5, JMSTimestamp=1375356194824, assignmentCode=, Pragma=no-cache, CamelHttpMethod=GET, gradeType=RN, mappedTrustId=RR8, Content-Length=602, trustId=RR8, HealthRosterUserId=esb, JMSExpiration=0, BookingReferenceNumber=, Date=Thu, 01 Aug 2013 11:23:14 GMT, CamelHttpQuery=$filter=HealthRosterTrust eq 'RR8' and HealthRosterWard eq null and HealthRosterGradeType eq 'RN' and HealthRosterGradeCategory eq 'Registered' and (not AssignmentSkillsMappings/any())&$expand=AssignmentSkillsMappings, HealthRosterPassword=esbesb, jobCode=B10, 	<entry>
<id>http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5/OData/TempStaffing.svc/TempStaffInterfaceEvents(12111)</id>
<category scheme="http://schemas.microsoft.com/ado/2007/08/dataservices/scheme" term="TempStaffing.TempStaffInterfaceEvent"/>
<link href="TempStaffInterfaceEvents(12111)" rel="edit" title="TempStaffInterfaceEvent"/>
<title/>
<updated>2013-08-01T11:23:13Z</updated>
<author>
<name/>
</author>
<content type="application/xml">
<properties>
<ID type="Edm.Int32">12111</ID>
<OccuredOn type="Edm.DateTime">2013-08-01T12:22:49.63</OccuredOn>
<EventType>DutySentToBank</EventType>
<TempStaffingInterface>NHSp</TempStaffingInterface>
<BookingReference null="true"/>
<RequestDetails>&lt;TempStaffingODataRequestDetails ActualEnd="0001-01-01T00:00:00" ActualRestTime="0" ActualStart="0001-01-01T00:00:00" AssignmentCode="" DutyID="382966" FulfilmentLockedToNHSpWebPortal="False" PlannedEnd="2013-08-02T19:15:00" PlannedRestTime="45" PlannedStart="2013-08-02T07:00:00" RequiredGradeTypeCategoryName="Registered" RequiredGradeTypeName="RN" ShiftType="Day" TempStaffBookingReasonName="B10" TrustID="RR8" UnitCode="WD60"/&gt;</RequestDetails>
</properties>
</content>
</entry>	ID-LON-DEV-MIMI-23733-1375351788466-70-27	ID-LON-DEV-MIMI-23733-1375351788466-70-26			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134243	01/08/2013 12:23:14	HRNHSP-HRSB-CreateDutyEnricher	Processing Exchange	latestTimestamp=2013-08-01T12:22:49.6300000, Server=Microsoft-IIS/7.5, JMSDeliveryMode=2, JMSPriority=4, JMSExpiration=0, JMSXGroupID=null, InterfaceEventsTime=2013-08-01T11:52:43, BookingReferenceNumber=, DataServiceVersion=1.0;, routeIdLog=HRNHSP-HRSB-CreateDutyEnricher, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-69:1:1:1:1, X-Powered-By=ASP.NET, JMSDestination=queue://MSSQL-Logging, CamelJmsDeliveryMode=2, HealthRosterSession=.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/, HealthRosterServer=http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5, CookieSource=Database, JMSCorrelationID=null, JMSTimestamp=1375356193990, Date=Thu, 01 Aug 2013 11:23:14 GMT, CamelHttpQuery=$filter=OccuredOn ge datetime'2013-08-01T11:52:43', Expires=-1, Cache-Control=no-cache, JMSRedelivered=false, X-AspNet-Version=4.0.30319, Pragma=no-cache, Content-Length=1808, HealthRosterPassword=esbesb, Content-Type=application/atom+xml;type=feed;charset=utf-8, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, RequestId=12111, CamelHttpResponseCode=200, TrustConfigurationId=1, X-Content-Type-Options=nosniff, JMSType=null, CamelHttpUri=DutySentToBank, LogMessage=Processing Exchange, JMSReplyTo=null, HealthRosterUserId=esb, CamelHttpMethod=GET, trustID=RR8, 	<entry>
<id>http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5/OData/TempStaffing.svc/TempStaffInterfaceEvents(12111)</id>
<category scheme="http://schemas.microsoft.com/ado/2007/08/dataservices/scheme" term="TempStaffing.TempStaffInterfaceEvent"/>
<link href="TempStaffInterfaceEvents(12111)" rel="edit" title="TempStaffInterfaceEvent"/>
<title/>
<updated>2013-08-01T11:23:13Z</updated>
<author>
<name/>
</author>
<content type="application/xml">
<properties>
<ID type="Edm.Int32">12111</ID>
<OccuredOn type="Edm.DateTime">2013-08-01T12:22:49.63</OccuredOn>
<EventType>DutySentToBank</EventType>
<TempStaffingInterface>NHSp</TempStaffingInterface>
<BookingReference null="true"/>
<RequestDetails>&lt;TempStaffingODataRequestDetails TrustID="RR8" DutyID="382966" UnitCode="A &amp;amp; E" ShiftType="Day" PlannedStart="2013-08-02T07:00:00" PlannedEnd="2013-08-02T19:15:00" PlannedRestTime="45" ActualStart="0001-01-01T00:00:00" ActualEnd="0001-01-01T00:00:00" ActualRestTime="0" TempStaffBookingReasonName="Deep Clean" RequiredGradeTypeName="RN" RequiredGradeTypeCategoryName="Registered" FulfilmentLockedToNHSpWebPortal="False" /&gt;</RequestDetails>
</properties>
</content>
</entry>	ID-LON-DEV-MIMI-23733-1375351788466-70-22	ID-LON-DEV-MIMI-23733-1375351788466-70-21			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134242	01/08/2013 12:23:14	HRNHSP-HRSB-EventTypeEnrichRouter	Processing Exchange	Server=Microsoft-IIS/7.5, TrustConfigurationId=1, Content-Length=1808, Content-Type=application/atom+xml;type=feed;charset=utf-8, JMSRedelivered=false, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, trustID=RR8, JMSExpiration=0, CamelHttpMethod=GET, X-AspNet-Version=4.0.30319, HealthRosterPassword=esbesb, JMSDestination=queue://MSSQL-Logging, CamelHttpResponseCode=200, Pragma=no-cache, RequestId=12111, HealthRosterServer=http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5, CamelHttpQuery=$filter=OccuredOn ge datetime'2013-08-01T11:52:43', InterfaceEventsTime=2013-08-01T11:52:43, JMSDeliveryMode=2, JMSReplyTo=null, CookieSource=Database, BookingReferenceNumber=, Cache-Control=no-cache, JMSXGroupID=null, X-Content-Type-Options=nosniff, CamelJmsDeliveryMode=2, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-65:1:1:1:1, JMSPriority=4, CamelHttpUri=DutySentToBank, LogMessage=Processing Exchange, latestTimestamp=2013-08-01T12:22:49.6300000, JMSTimestamp=1375356193974, routeIdLog=HRNHSP-HRSB-EventTypeEnrichRouter, X-Powered-By=ASP.NET, JMSCorrelationID=null, HealthRosterUserId=esb, HealthRosterSession=.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/, DataServiceVersion=1.0;, Date=Thu, 01 Aug 2013 11:23:14 GMT, Expires=-1, JMSType=null, 	<entry>
<id>http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5/OData/TempStaffing.svc/TempStaffInterfaceEvents(12111)</id>
<category scheme="http://schemas.microsoft.com/ado/2007/08/dataservices/scheme" term="TempStaffing.TempStaffInterfaceEvent"/>
<link href="TempStaffInterfaceEvents(12111)" rel="edit" title="TempStaffInterfaceEvent"/>
<title/>
<updated>2013-08-01T11:23:13Z</updated>
<author>
<name/>
</author>
<content type="application/xml">
<properties>
<ID type="Edm.Int32">12111</ID>
<OccuredOn type="Edm.DateTime">2013-08-01T12:22:49.63</OccuredOn>
<EventType>DutySentToBank</EventType>
<TempStaffingInterface>NHSp</TempStaffingInterface>
<BookingReference null="true"/>
<RequestDetails>&lt;TempStaffingODataRequestDetails TrustID="RR8" DutyID="382966" UnitCode="A &amp;amp; E" ShiftType="Day" PlannedStart="2013-08-02T07:00:00" PlannedEnd="2013-08-02T19:15:00" PlannedRestTime="45" ActualStart="0001-01-01T00:00:00" ActualEnd="0001-01-01T00:00:00" ActualRestTime="0" TempStaffBookingReasonName="Deep Clean" RequiredGradeTypeName="RN" RequiredGradeTypeCategoryName="Registered" FulfilmentLockedToNHSpWebPortal="False" /&gt;</RequestDetails>
</properties>
</content>
</entry>	ID-LON-DEV-MIMI-23733-1375351788466-70-17	ID-LON-DEV-MIMI-23733-1375351788466-70-16			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134241	01/08/2013 12:23:14	HRNHSP-HRSB-TrustTimePersister	Processing Exchange	JMSTimestamp=1375356193974, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-67:1:1:1:1, Cache-Control=no-cache, JMSXGroupID=null, HealthRosterPassword=esbesb, JMSExpiration=0, latestTimestamp=2013-08-01T12:22:49.6300000, JMSDestination=queue://MSSQL-Logging, CamelHttpQuery=$filter=OccuredOn ge datetime'2013-08-01T11:52:43', trustID=RR8, Pragma=no-cache, HealthRosterSession=.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/, HealthRosterServer=http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5, Content-Length=1808, InterfaceEventsTime=2013-08-01T11:52:43, CookieSource=Database, CamelHttpMethod=GET, Server=Microsoft-IIS/7.5, TrustConfigurationId=1, JMSRedelivered=false, routeIdLog=HRNHSP-HRSB-TrustTimePersister, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, Date=Thu, 01 Aug 2013 11:23:14 GMT, X-Powered-By=ASP.NET, X-AspNet-Version=4.0.30319, JMSPriority=4, Content-Type=application/atom+xml;type=feed;charset=utf-8, HealthRosterUserId=esb, LogMessage=Processing Exchange, X-Content-Type-Options=nosniff, DataServiceVersion=1.0;, JMSCorrelationID=null, JMSType=null, JMSReplyTo=null, JMSDeliveryMode=2, Expires=-1, CamelHttpResponseCode=200, 		ID-LON-DEV-MIMI-23733-1375351788466-70-12	ID-LON-DEV-MIMI-23733-1375351788466-70-11			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134240	01/08/2013 12:22:52	HRNHSP-HRSB-BulkInterfaceEventsSplitter	Processing Exchange	CamelHttpMethod=POST, JMSDestination=queue://MSSQL-Logging, JMSRedelivered=false, CamelHttpUri=http://lon-dev-mimi:8887/StaffBank/v1.0//InterfaceDataAvailable, routeIdLog=HRNHSP-HRSB-BulkInterfaceEventsSplitter, JMSExpiration=0, JMSDeliveryMode=2, latestTimestamp=2013-08-01T12:22:49.6300000, JMSCorrelationID=null, JMSTimestamp=1375356171739, JMSReplyTo=null, JMSPriority=4, JMSType=null, JMSXGroupID=null, trustID=RR8, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-61:1:1:1:1, LogMessage=Processing Exchange, org.restlet.startTime=1375356170890, 	<?xml version="1.0" encoding="UTF-8"?>
<o><latestTimestamp>2013-08-01T12:22:49.6300000</latestTimestamp><trustID>RR8</trustID></o>
	ID-LON-DEV-MIMI-23733-1375351788466-70-7	ID-LON-DEV-MIMI-23733-1375351788466-70-6			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134239	01/08/2013 12:22:51	HRNHSP-HRSB-HRRestletEndPointIn	Received message from HR on Restlet endpoint	JMSDeliveryMode=2, JMSDestination=queue://MSSQL-Logging, JMSRedelivered=false, JMSMessageID=ID:LON-DEV-MIMI-23726-1375351784824-59:1:1:1:1, CamelHttpUri=http://lon-dev-mimi:8887/StaffBank/v1.0//InterfaceDataAvailable, JMSCorrelationID=null, JMSXGroupID=null, LogMessage=Received message from HR on Restlet endpoint, breadcrumbId=ID-LON-DEV-MIMI-23733-1375351788466-56-1, JMSType=null, JMSPriority=4, org.restlet.startTime=1375356170890, JMSTimestamp=1375356171705, CamelHttpMethod=POST, JMSReplyTo=null, routeIdLog=HRNHSP-HRSB-HRRestletEndPointIn, JMSExpiration=0, 	{"trustID": "RR8", "latestTimestamp": "2013-08-01T12:22:49.6300000"}	ID-LON-DEV-MIMI-23733-1375351788466-70-2	ID-LON-DEV-MIMI-23733-1375351788466-70-1			ID-LON-DEV-MIMI-23733-1375351788466-56-1					
134238	01/08/2013 11:52:43	HRNHSP-HRSB-TrustTimePersister	Processing Exchange	X-AspNet-Version:4.0.30319;Date:Thu, 01 Aug 2013 10:52:43 GMT;LogMessage:Processing Exchange;TrustConfigurationId:1;CookieSource:Database;Pragma:no-cache;JMSType:null;JMSRedelivered:false;breadcrumbId:ID-LON-DEV-MIMI-23733-1375351788466-32-1;HealthRosterSession:.HRFedAuth_127_PQAKING5=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il9lZmNkMGIwOS01M2MyLTQ2MTMtYjA2ZS0wYWZhNWE5NzE4ODktRUZCMENFMTFEMUQzMDc0RUZFRjA4MERGMTE2MTk1REUiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6ZTA3MTZiOTAtNTBhNy00YWQxLTgzMGQtZjIyMWNkNjg0OWM0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+SVR6aHBWS29wSDhhNElrYis0aE1KaU5sdXhHZ3hLcWRpV0RqQTN2WDBDWGszWXJQek1IcTgralFTSDYrRnBpU1c0MDZIcmZNeEk4MDVYYmlDR3k5MTFaL0tGeDdYSzJOY0lDN24yWEJSWFBDU3ArQjVkMmJqTHZaUUhOQmUvb3MwUktVTkdndFU0WE0xNEJnbHJ4NTZITWlFVCsxVWN6MDFXNTZvQmhLZWE0eWkzZlVMc0JVRFBvNGcyNTEzQlJob1BmbXpHNlV6QUJURWI3UmkvTTBzQ2hlZUZWekV4UmkvQnJnRUY3ejhaVUV5QVlSbm9iVkUyZkpHRVFxbm1aYkRMajA4Zmp4NEZueitnSHhDQ1FiTUxweEhMUEhQbzY1eVcrSGhlWUwzY2hOQ2I0NWJkVW5LUXFTZHVtcDM5cFlWVGZ6dXViOVhYaWdKMHJMSE9XUE5wUmdIY214enl0QWtOVU5HTmZ0N2pVUkNaeXRGSWFndXpCNE05ZlEzTlZreDhpRzNDSFgwbFhBOUhIc0JxYkNrTWJHOFFQV3lRUjJqck5BLzAvMXFmRG5BWHM0N2VFYVFlMlZoOVNTSlRVOUV3bEVRcnFISE5mUnpjY09iRVE5UXVwMzZRT3BWeC9aOE5YaXJjNHkvWTR3RUpseEh4Nm9lQm1jZSszc21kSkxQZ2NQYllEVEJOUERLVHV4OEtPaW5YdmFCQ3dKQ3dtY2VhS01mMmhZcGRkYnI5WWRBMlpmVUFXRDB2OWZINElQMmRlME9iRUxGaHdYV3BOYm91aGUrTCtMN2pMTkNINXd2MGZuemhKM3FpcWEvc0pMeHNtMXlOR2FiaTMrYWVEakJkM0hUalJ3NklyVjdNTHZNNnVFN0o3b2NjUDdNbWRlUlNDRm1YWWZUajRISlpDK21Cek82b2x6eFNhbDNNSTBCNUV0eTZWem5yQ1VRVGF5SXN6UGFqalNWMjhDU2t6RmRPR1FsUDN2OEFhc0c2UlhIK0ZyckFlQVU2UkQxY2t5SHhUeUUva1NUWEZuNzl4RkQ4M05UdWlTcnd5aDZ2MER3RkRoL0hKK2F5UEpTNHkrcGFhNGxZMkt2emZwUWVwN1dlR2JoUXYvTHlqWDU4ZlRlNEswTDRaWFJoTHJucDUreTBxZldERnQ3Ukt1eXpmMFFrTUJtcG9PQWJCUmFpWDg8L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==; path=/HealthRoster/; HttpOnly;HealthRoster_10_2_127_1_PQAKING5=Session=171051&AuthenticatedUser=11652&AuthorisedUser=11652&VirtualOffset=00:00:00&LocaleChain=en_UK; path=/HealthRoster/;JMSTimestamp:1375354363469;Content-Length:626;latestTimestamp:2013-08-01T11:26:17.7270000;X-Powered-By:ASP.NET;JMSXGroupID:null;HealthRosterUserId:esb;Content-Type:application/atom+xml;type=feed;charset=utf-8;Server:Microsoft-IIS/7.5;trustID:RR8;JMSDestination:queue://MSSQL-Logging;JMSMessageID:ID:LON-DEV-MIMI-23726-1375351784824-47:1:1:1:1;JMSDeliveryMode:2;HealthRosterServer:http://sdc-devvcorap01/HealthRoster/10.2.127.1/PQAKING5;X-Content-Type-Options:nosniff;JMSPriority:4;Expires:-1;JMSReplyTo:null;Cache-Control:no-cache;JMSCorrelationID:null;CamelHttpQuery:$filter=OccuredOn ge datetime'2013-08-01T11:27:21';CamelHttpMethod:GET;JMSExpiration:0;CamelHttpResponseCode:200;DataServiceVersion:1.0;;routeIdLog:HRNHSP-HRSB-TrustTimePersister;HealthRosterPassword:esbesb;InterfaceEventsTime:2013-08-01T11:27:21;		ID-LON-DEV-MIMI-23733-1375351788466-46-12	ID-LON-DEV-MIMI-23733-1375351788466-46-11			ID-LON-DEV-MIMI-23733-1375351788466-32-1					
134237	01/08/2013 11:52:21	HRNHSP-HRSB-BulkInterfaceEventsSplitter	Processing Exchange	JMSMessageID:ID:LON-DEV-MIMI-23726-1375351784824-45:1:1:1:1;JMSRedelivered:false;JMSExpiration:0;breadcrumbId:ID-LON-DEV-MIMI-23733-1375351788466-32-1;LogMessage:Processing Exchange;trustID:RR8;JMSPriority:4;routeIdLog:HRNHSP-HRSB-BulkInterfaceEventsSplitter;JMSCorrelationID:null;JMSDeliveryMode:2;JMSType:null;JMSDestination:queue://MSSQL-Logging;JMSXGroupID:null;JMSTimestamp:1375354341015;CamelHttpUri:http://lon-dev-mimi:8887/StaffBank/v1.0//InterfaceDataAvailable;latestTimestamp:2013-08-01T11:26:17.7270000;CamelHttpMethod:POST;JMSReplyTo:null;org.restlet.startTime:1375354340720;	<?xml version="1.0" encoding="UTF-8"?>
<o><latestTimestamp>2013-08-01T11:26:17.7270000</latestTimestamp><trustID>RR8</trustID></o>
	ID-LON-DEV-MIMI-23733-1375351788466-46-7	ID-LON-DEV-MIMI-23733-1375351788466-46-6			ID-LON-DEV-MIMI-23733-1375351788466-32-1					
*/


        }
    }
}
