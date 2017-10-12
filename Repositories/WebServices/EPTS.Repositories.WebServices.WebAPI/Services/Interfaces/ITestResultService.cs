using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestResultService
    {
        Task<bool> CreateTestResult(TestResult entity);
        Task<bool> DeleteTestResult(Guid id);
        Task<IEnumerable<TestResult>> GetAllTestResults(string whereValue, string orderBy);
        Task<bool> UpdateTestResult(TestResult entity);
        Task<TestResult> GetTestResultById(Guid id);

    }
}