using ASG_ADAC_FE.Repository;
using Moq;
using Xunit;
using AutoMapper;
using ASG_ADAC_FE.Mapper;
using ASG_ADAC_FE.DTO;
using System.Threading.Tasks;
using ASG_ADAC_FE.Controllers;
using ASG_ADAC_FE.CoreModels;

namespace ADPORT_TEST
{
    public class EmployeeUnitTestController
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private IMapper _mapper;

        public EmployeeUnitTestController()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

          
        }

        #region Create Employee
        [Fact]
        public async Task CreateEmployee()
        {
            // arrange phase
            var employee = new EmployeeDTO {  Name = "Randhawa", Email="test@test.com", Active=true };           

            // processing phase
            var empcontroller = new EmployeeController(_mapper, _mockEmployeeRepository.Object);

            var result = await empcontroller.PostEmployee(employee);

            // assert phase
            Assert.NotNull(result);
            Assert.Equal("Employee has been created successfuly", result);
        }

        #endregion

        #region Find Employee     
        [Fact]  
        public void GetEmployeebyId()
        {
            var employeeDTO = new Employee()
            {
                Id = 1,
                Name = "Test",
                Active = true
            };
            _mockEmployeeRepository.Setup(p => p.GetEmployeeById(1)).ReturnsAsync(employeeDTO);
            var empcontroller = new EmployeeController(_mapper, _mockEmployeeRepository.Object);
            var result =  empcontroller.GetEmployee(1);
            Assert.True(employeeDTO.Id.Equals(result.Id));
        }

        #endregion

        #region Delete Employee     
        [Fact]
        public void DeleteEmployeebyId()
        {
            var employeeDTO = new Employee()
            {
                Id = 1,
                Name = "Test",
                Active = true
            };
            _mockEmployeeRepository.Setup(p => p.GetEmployeeById(1)).ReturnsAsync(employeeDTO);
            var empcontroller = new EmployeeController(_mapper, _mockEmployeeRepository.Object);
            var result = empcontroller.DeleteEmployee(1);
            Assert.True(employeeDTO.Id.Equals(result.Id));
        }

        #endregion

        #region Update Employees     
        [Fact]
        public void UpdateEmployeees()
        {
            var employee = new Employee()
            {
                Id = 1,
                Name = "Test",
                Active = true
            };

            var employeeDTO = new EmployeeDTO()
            {
                Id = 1,
                Name = "Test",
                Active = true
            };
            _mockEmployeeRepository.Setup(p => p.UpdateEmployee(employee));
            var empcontroller = new EmployeeController(_mapper, _mockEmployeeRepository.Object);
            var result = empcontroller.PutEmployee(1, employeeDTO);
            Assert.True(result);
        }

        #endregion

        //End of Testing Cases//

    }
}
