using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("{controller}")]

public class MovementController : ControllerBase
{
    private readonly MyDal db;
    private readonly IMapper mapper;

    public MovementController(MyDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    public MovementDTO Get(Guid id)
    {
        var dao = db.Movements.Include(m => m.User).Include(m => m.Category).FirstOrDefault(u => u.Id == id);
        var dto = mapper.Map<MovementDTO>(dao);
        return dto;
        //BEFORE MAPPER
        // var user = movement.User;
        // var cat = movement.Category;
        // var dto = new MovementDTO()
        // {
        //     Id = movement.Id,
        //     A = movement.Amount,
        //     L = movement.Label,
        //     D = movement.Date,
        //     IdU = movement.IdUser,
        //     U = user.FirstName + " " + user.LastName,
        //     IdC = movement.IdCategory,
        //     C = cat.Label,
        //     DC = cat.Debit
        // };
        return dto;
    }

    [HttpPost("")]
    public async Task<object> PostMovement([FromBody] MovementDTO move)
    {
        if (!ModelState.IsValid)
        {
            var messagesErreur = ModelState.SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToArray();
            return BadRequest(messagesErreur);
        }
        var dao = mapper.Map<MovementDAO>(move);
        //BEFORE mapper
        // var dao = new MovementDAO()
        // {
        //     Amount = move.A,
        //     Label = move.L,
        //     IdUser = move.IdU,
        //     IdCategory = move.IdC,
        // };
        db.Movements.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }
}