using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("{controller}")]
public class UserController : ControllerBase //ControllerBase n'intègre pas la notion de vue
{
    private readonly MyDal db;
    private readonly IMapper mapper;

    public UserController(MyDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public SearchResultDTO[] Get(string searchText = "")
    {
        var daos = db.Users;
        var dtos = daos.Select(dao => mapper.Map<SearchResultDTO>(dao)).ToArray();
        //Format results according to the file /Views/Users/GetUsers.cshtml
        return dtos;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<UserDTO> UserMovement(Guid id)
    {
        var user = db.Users.Include(u => u.Movements).FirstOrDefault(u => u.Id == id);
        var dto = mapper.Map<UserDTO>(user);
        // return dto;
        // var movements = user.Movements;
        // var mDto = movements.Select(m => new SearchResultDTO()
        // {
        //     Id = m.Id,
        //     I = m.Amount.ToString(),
        //     L = m.Label,
        // }).ToArray();
        // var dto = new UserDTO()
        // {
        //     Id = user.Id,
        //     P = user.FirstName,
        //     N = user.LastName,
        //     E = user.Email,
        //     M = mDto
        // };

        return Ok(dto);
    }

    [HttpPost("")]
    public async Task<object> PostMovement([FromBody] UserDTO user)
    {
        if (!ModelState.IsValid)
        {
            var messagesErreur = ModelState.SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToArray();
            return BadRequest(messagesErreur);
        }

        var dao = mapper.Map<UserDAO>(user);
        //Before using mapper
        // var dao = new UserDAO()
        // {
        //     LastName = user.N,
        //     FirstName = user.P,
        //     Email = user.E
        // };
        db.Users.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);

    }

}