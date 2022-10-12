using Microsoft.AspNetCore.Mvc;
using RestExercise1.Managers;
using RestExercise1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestExercise1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private FlowersManagerList _manager = new FlowersManagerList();

        // GET: api/<FlowersController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Flower>> Get()
        {
            IEnumerable<Flower> list = _manager.GetAll();
            if (list == null || list.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }
        }

        // GET api/<FlowersController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Flower> Get(int id)
        {
            Flower foundFlower = _manager.GetById(id);
            if (foundFlower == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(foundFlower);
            }
        }

        // POST api/<FlowersController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Flower> Post([FromBody] Flower newFlower)
        {
            try
            {
                Flower createdFlower = _manager.Add(newFlower);
                return Created("/" + createdFlower.Id, createdFlower);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<FlowersController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Flower> Put(int id, [FromBody] Flower updates)
        {
            try
            {
                Flower updatedFlower = _manager.Update(id, updates);
                if (updatedFlower == null)
                {
                    return NotFound();
                }
                return Ok(updatedFlower);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<FlowersController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Flower> Delete(int id)
        {
            Flower deletedFlower = _manager.Delete(id);
            if (deletedFlower == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(deletedFlower);
            }
        }
    }
}
