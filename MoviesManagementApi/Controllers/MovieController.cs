using Microsoft.AspNetCore.Mvc;
using MoviesManagementApi.Dto;
using MoviesManagementApi.Repositories;

namespace MoviesManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _movieRepository.GetAll();
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var result = _movieRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]MovieDto dto)
        {
            await _movieRepository.Create(dto);
            return Ok(dto);
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit([FromBody]MovieDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            await _movieRepository.Edit(dto);
            return Ok(dto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieRepository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("createrange")]
        public async Task<IActionResult> CreateMultiple([FromBody]List<MovieDto> dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            await _movieRepository.CreateMultiple(dto);
            return Ok();
        }
    }
}
