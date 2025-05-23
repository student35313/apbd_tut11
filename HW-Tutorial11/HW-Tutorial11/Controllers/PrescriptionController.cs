using HW_Tutorial11.DTOs;
using HW_Tutorial11.Services;
using Microsoft.AspNetCore.Mvc;
using Tutorial9.Exceptions;

namespace HW_Tutorial11.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IDbService _dbService;
    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("patient/{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        try
        {
            var result = await _dbService.GetPatientByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }

    
    [HttpPost]
    public async Task<IActionResult> AddPrescription(PrescriptionCreationDto creationDto)
    {
        try
        {
            await _dbService.AddPrescriptionAsync(creationDto);
            return Created("", "Prescription created successfully.");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);       
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Internal server error" });
        }
    }
    
}