using AioiTest.Model;
using AioiTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace AioiTest.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var response = await _customerService.GetCustomerById(id);
                return Ok(ResponseModel<CustomerModel>.Success(response));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseModel<string>.Failure(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerList()
        {
            try
            {
                var response = await _customerService.GetCustomerList();
                return Ok(ResponseModel<List<CustomerModel>>.Success(response));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseModel<string>.Failure(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                bool result = await _customerService.AddCustomer(customer);
                return Ok(ResponseModel<bool>.Success(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseModel<string>.Failure(ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                bool result = await _customerService.UpdateCustomer(customer);
                return Ok(ResponseModel<object>.Success(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseModel<string>.Failure(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerById(int id)
        {
            try
            {
                bool result = await _customerService.DeleteCustomerById(id);
                return Ok(ResponseModel<bool>.Success(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseModel<string>.Failure(ex.Message));
            }
        }
    }
}
