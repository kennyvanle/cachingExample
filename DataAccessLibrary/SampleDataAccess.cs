using System;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
	public class SampleDataAccess
	{
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }
		public List<EmployeeModel> GetEmployees()
		{
			List<EmployeeModel> employees = new List<EmployeeModel>();
			employees.Add(new() { FirstName = "Kenny", LastName = "Le" });
            employees.Add(new() { FirstName = "Sisco", LastName = "Heffner" });
            employees.Add(new() { FirstName = "Ollie", LastName = "Jack" });

			Thread.Sleep(3000);
			return employees;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            employees.Add(new() { FirstName = "Kenny", LastName = "Le" });
            employees.Add(new() { FirstName = "Sisco", LastName = "Heffner" });
            employees.Add(new() { FirstName = "Ollie", LastName = "Jack" });

            await Task.Delay(3000);
            return employees;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            List<EmployeeModel> output;
            output = _memoryCache.Get<List<EmployeeModel>>("employees");
            if(null == output) {
                output = new();
                output.Add(new() { FirstName = "Kenny", LastName = "Le" });
                output.Add(new() { FirstName = "Sisco", LastName = "Heffner" });
                output.Add(new() { FirstName = "Ollie", LastName = "Jack" });
                await Task.Delay(3000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }

            return output;
        }

    }
}

