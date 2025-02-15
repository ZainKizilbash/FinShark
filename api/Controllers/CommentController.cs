using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Stock.Comment;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IStockRepository _stockrepository;

        public CommentController(ICommentRepository repository, IStockRepository stockrepository)
        {
            _repository = repository;
            _stockrepository = stockrepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _repository.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _repository.GetByIdAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments.ToCommentDto());
        }

        [HttpPost("{stockid:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockid, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockrepository.StockExists(stockid))
            {
                return BadRequest("Stock doesn't exist!");
            }
            var comment = commentDto.ToCommentFromCreate(stockid);
            await _repository.CreateAsync(comment);
            return Ok(comment);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _repository.UpdateAsync(id, updateDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _repository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Comment does not exist!");
            }
            return Ok(comment);
        }


    }
}