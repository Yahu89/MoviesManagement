using Microsoft.EntityFrameworkCore;
using MoviesManagementApi.Database;
using MoviesManagementApi.Dto;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace MoviesManagementApi.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MoviesManagementDbContext _dbContext;
    public MovieRepository(MoviesManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private Expression<Func<Movie, MovieDto>> MapToDto = x => new MovieDto()
    {
        Id = x.Id,
        Title = x.Title,
        Year = x.Year,
        Director = x.Director,
        Rate = x.Rate
    };

    private Expression<Func<MovieDto, Movie>> MapToEntity = x => new Movie()
    {
        Id = x.Id,
        Title = x.Title,
        Year = x.Year,
        Director = x.Director,
        Rate = x.Rate
    };

    private Expression<Func<MovieDto, Movie>> MapToEntityFromExternal = x => new Movie()
    {
        Title = x.Title,
        Year = x.Year,
        Director = x.Director,
        Rate = x.Rate
    };
    public async Task<List<MovieDto>> GetAll()
    {
        try
        {
            var entities = await _dbContext.Movies.ToListAsync();
            var results = entities.Select(MapToDto.Compile()).ToList();
            return results;
        }
        catch (Exception ex)
        {
            throw;
        } 
    }

    public MovieDto GetById(int id)
    {
        try
        {
            var result = _dbContext.Movies.Select(MapToDto.Compile())
                                      .First(x => x.Id == id);

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
        
    }

    public async Task Edit(MovieDto movie)
    {
        try
        {
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw;
        }    
    }

    public async Task Create(MovieDto model)
    {
        try
        {
            _dbContext.Movies.Add(model);
            await _dbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw;
        }      
    }

    public async Task CreateMultiple(List<MovieDto> model)
    {
        var list = model.Select(MapToEntityFromExternal.Compile()).ToList();

        foreach (var movie in list)
        {        
            _dbContext.Movies.Add(movie);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                continue;
            }
        }
    }

    public async Task Delete(int id)
    {   
        try
        {
            var movie = await _dbContext.Movies.FirstAsync(x => x.Id == id);
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
        
    }
}
