using AutoMapper;
using DAL;

var configurationMapping = new MapperConfiguration(options =>
{
    options.CreateMap<UserDAO, UserDTO>()
        .ForMember(u => u.Id, option => option.MapFrom(u => u.Id))
        .ForMember(u => u.N, option => option.MapFrom(u => u.LastName))
        .ForMember(u => u.P, option => option.MapFrom(u => u.FirstName))
        .ForMember(u => u.E, option => option.MapFrom(u => u.Email))
        .ForMember(u => u.M, option => option.MapFrom(u => u.Movements))
        .ReverseMap();
    options.CreateMap<UserDAO, SearchResultDTO>()
        .ForMember(u => u.Id, option => option.MapFrom(u => u.Id))
        .ForMember(u => u.L, option => option.MapFrom(u => (u.FirstName + " " + u.LastName)))
        .ForMember(u => u.I, option => option.MapFrom(u => u.Email));
    options.CreateMap<CategoryDAO, CategoryDTO>()
        .ForMember(c => c.Id, option => option.MapFrom(c => c.Id))
        .ForMember(c => c.L, option => option.MapFrom(c => c.Label))
        .ReverseMap();
    options.CreateMap<CategoryDAO, SearchResultDTO>()
        .ForMember(c => c.Id, option => option.MapFrom(c => c.Id))
        .ForMember(c => c.L, option => option.MapFrom(c => c.Label));
    options.CreateMap<MovementDAO, MovementDTO>()
        .ForMember(m => m.Id, option => option.MapFrom(m => m.Id))
        .ForMember(m => m.A, option => option.MapFrom(m => m.Amount))
        .ForMember(m => m.L, option => option.MapFrom(m => m.Label))
        .ForMember(m => m.D, option => option.MapFrom(m => m.Date))
        .ForMember(m => m.IdU, option => option.MapFrom(m => m.IdUser))
        .ForMember(m => m.IdC, option => option.MapFrom(m => m.IdCategory))
        .ReverseMap();
    options.CreateMap<MovementDAO, SearchResultDTO>()
    .ForMember(m => m.Id, option => option.MapFrom(m => m.Id))
    .ForMember(m => m.I, option => option.MapFrom(m => m.Amount.ToString()))
    .ForMember(m => m.L, option => option.MapFrom(m => m.Label));

});

var mapper = configurationMapping.CreateMapper();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<MyDal>();

builder.Services.AddSingleton<IMapper>(mapper);

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.MapGet("/", () =>
{
    return "Server is working, olé!";
});

app.MapControllers();

app.Run();
