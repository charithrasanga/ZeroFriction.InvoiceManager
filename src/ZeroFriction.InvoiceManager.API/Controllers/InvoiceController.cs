using System;
using System.Threading.Tasks;
using ZeroFriction.InvoiceManager.Application.Services;
using ZeroFriction.InvoiceManager.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ZeroFriction.InvoiceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;


        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;            
        }

        /// <summary>
        /// Get Invoices
        /// </summary>
        /// <returns>Returns a list of All Invoices</returns>
        [HttpGet]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _invoiceService.GetAll());
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Get Invoice by ID
        /// </summary>
        /// <param name="id">Invoice ID</param>
        /// <returns>Returns a Invoice by its ID</returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _invoiceService.GetById(id));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new Invoice
        /// </summary>
        /// <param name="invoiceViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] InvoiceViewModel invoiceViewModel)
        {
            try
            {
                return Ok(await _invoiceService.Create(invoiceViewModel));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new Invoice
        /// </summary>
        /// <param name="invoiceViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] InvoiceViewModel invoiceViewModel)
        {
            try
            {
                return Ok(await _invoiceService.Update(invoiceViewModel));
            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a Invoice
        /// </summary>
        /// <param name="id">Invoice ID</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            try
            {
                await _invoiceService.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                Log.Error($"Error: message: {ex.Message} ");

                return StatusCode(StatusCodes.Status500InternalServerError, new { exception_message = ex.Message });
            }
        }
    }
}
