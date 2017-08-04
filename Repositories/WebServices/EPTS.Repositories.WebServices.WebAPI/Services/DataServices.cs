using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Services;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{

    public interface IDataServices
    {
        BusinessUnitService BusinessUnitService { get; set; }
        FamilyService FamilyService { get; set; }
        FlowService FlowService { get; set; }
        LineService LineService { get; set; }
        ModelService ModelService { get; set; }

        ModelDetailService ModelDetailService { get; set; }
        PartNumberService PartNumberService { get; set; }
        StationService StationService { get; set; }

        TestGroupLinkService TestGroupLinkService { get; set; }
        TestGroupService TestGroupService { get; set; }
        TestLinkService TestLinkService { get; set; }
        TestPlanLinkService TestPlanLinkService { get; set; }
        TestPlanService TestPlanService { get; set; }
        TestService TestService { get; set; }
        TestTypeService TestTypeService { get; set; }
        TestUnitService TestUnitService { get; set; }

    }
    public class DataServices : IDataServices
    {
        public DataServices(BusinessUnitService businessUnitService, FamilyService familyService, FlowService flowService, LineService lineService, ModelService modelService, ModelDetailService modelDetailService, PartNumberService partNumberService, StationService stationService, TestGroupLinkService testGroupLinkService, TestGroupService testGroupService, TestLinkService testLinkService, TestPlanLinkService testPlanLinkService, TestPlanService testPlanService, TestService testService, TestTypeService testTypeService, TestUnitService testUnitService)
        {
            BusinessUnitService = businessUnitService;
            FamilyService = familyService;
            FlowService = flowService;
            LineService = lineService;
            ModelService = modelService;
            ModelDetailService = modelDetailService;
            PartNumberService = partNumberService;
            StationService = stationService;
            TestGroupLinkService = testGroupLinkService;
            TestGroupService = testGroupService;
            TestLinkService = testLinkService;
            TestPlanLinkService = testPlanLinkService;
            TestPlanService = testPlanService;
            TestService = testService;
            TestTypeService = testTypeService;
            TestUnitService = testUnitService;
        }

        public BusinessUnitService BusinessUnitService { get; set; }
        public FamilyService FamilyService { get;  set; }
        public FlowService FlowService { get;  set; }
        public LineService LineService { get;  set; }
        public ModelService ModelService { get;  set; }
        public ModelDetailService ModelDetailService { get; set; }
        public PartNumberService PartNumberService { get;  set; }
        public StationService StationService { get;  set; }

        public TestGroupLinkService TestGroupLinkService { get; set; }
        public TestGroupService TestGroupService { get; set; }
        public TestLinkService TestLinkService { get; set; }
        public TestPlanLinkService TestPlanLinkService { get; set; }
        public TestPlanService TestPlanService { get; set; }
        public TestService TestService { get; set; }
        public TestTypeService TestTypeService { get; set; }
        public TestUnitService TestUnitService { get; set; }
    }
}