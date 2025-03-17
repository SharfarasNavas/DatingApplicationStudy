using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    public readonly Datacontext _context;
    public UserController(Datacontext context){
        _context = context;

        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers(){
        var Users = await _context.AppUsers.ToListAsync();
        return Ok(Users);
    }

    [HttpGet("{id:int}")]
    
    public async Task<ActionResult<AppUser>> GetAllUsers(int id)
    {
        var Users = await _context.AppUsers.FindAsync(id);
        if(Users==null){
            return BadRequest("Not Found");
        }
        return Ok(Users);
    }
}