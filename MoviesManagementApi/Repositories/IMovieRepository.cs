using MoviesManagementApi.Dto;

namespace MoviesManagementApi.Repositories;

public interface IMovieRepository
{
    Task<List<MovieDto>> GetAll();
    Task Create(MovieDto movie);
    MovieDto GetById(int id);
    Task Edit(MovieDto movie);
    Task Delete(int id);
    Task CreateMultiple(List<MovieDto> movie);
}
