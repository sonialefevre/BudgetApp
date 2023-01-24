using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("{controller}")]

public class CategoryController : ControllerBase
{
    private readonly MyDal db;
    private readonly IMapper mapper;

    public CategoryController(MyDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public SearchResultDTO[] GetAllCats(string searchText = "")
    {
        var daos = db.Categories;
        var dtos = daos.Select(dao => mapper.Map<SearchResultDTO>(dao)).ToArray();
        return dtos;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<CategoryDTO> GetOneCat(Guid id)
    {
        var dao = db.Categories.Find(id);
        var dto = mapper.Map<CategoryDTO>(dao);
        return Ok(dto);
    }

    [HttpPost("")]
    public async Task<object> PostCat([FromBody] CategoryDTO cat)
    {
        if (!ModelState.IsValid)
        {
            var messagesErreur = ModelState.SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToArray();
            return BadRequest(messagesErreur);
        }

        var dao = mapper.Map<CategoryDAO>(cat);
        db.Categories.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }

}