using apbd_assignment_05;
using Microsoft.AspNetCore.Mvc;

namespace apbd_assignment_05.Controllers;

[ApiController]
[Route("/api/devices")]
public class DeviceController : ControllerBase
{
    private readonly DeviceManager manager;

    public DeviceController(DeviceManager manager)
    {
        this.manager = manager;
    }

    
    [HttpGet("getDevices")]
    public IActionResult GetDevices()
    {
        return Ok(manager.getDevices());   
    }
    [HttpGet("{indexNumber}")]
    public IActionResult GetDevice(string indexNumber)
    {
        Device device = manager.getDevices().Find(e => e.Id.ToString() == indexNumber);
        if (device == null)
        {
            return NotFound($"Device with id {indexNumber} does not exist");
        }
        return Ok(device);   
    }
    
    [HttpPost]
    public IActionResult AddDevice([FromBody] DeviceDto dto)
    {
        try
        {
            Device device = dto.Type switch
            {
                "SW" => new Smartwatch
                {
                    Id = manager.getDevices().Count + 1,
                    Name = dto.Name,
                    IsTurnedOn = dto.IsTurnedOn,
                    BatteryPercentage = dto.BatteryPercentage ?? throw new ArgumentException("BatteryPercentage is required for Smartwatch")
                },
                "P" => new PersonalComputer
                {
                    Id = manager.getDevices().Count + 1,
                    Name = dto.Name,
                    IsTurnedOn = dto.IsTurnedOn,
                    OperatingSystem = dto.OperatingSystem
                },
                "ED" => new EmbeddedDevice
                {
                    Id = manager.getDevices().Count + 1,
                    Name = dto.Name,
                    IsTurnedOn = dto.IsTurnedOn,
                    IpAddress = dto.IpAddress ?? throw new ArgumentException("IpAddress is required for EmbeddedDevice"),
                    NetworkName = dto.NetworkName ?? throw new ArgumentException("NetworkName is required for EmbeddedDevice")
                },
                _ => throw new ArgumentException("Invalid device type")
            };

            manager.AddDevice(device);
            manager.SaveDevices();
            
            return Created($"/api/devices/{device.Id}", device);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    
    
}