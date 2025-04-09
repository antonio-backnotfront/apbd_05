using apbd_02;
using Microsoft.AspNetCore.Mvc;

namespace apbd_assignment_05.Controllers;

[ApiController]
[Route("/api/devices")]
public class DeviceController : ControllerBase
{
    DeviceManager manager;

    public DeviceController()
    {
        this.manager = new DeviceManager("input.txt");
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
    
    
}