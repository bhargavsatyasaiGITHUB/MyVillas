using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVillas_Api.Data;
using MyVillas_Api.Logging;
using MyVillas_Api.Models;
using MyVillas_Api.Models.Dto;

namespace MyVillas_Api.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogging _logger;
        //public VillaAPIController(ILogging logger)
        //{
        //   _logger = logger;
        //}
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db,IMapper mapper)
        {
                _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {

            IEnumerable<Villa> villaList = await _db.villas.ToListAsync();
            return Ok( _mapper.Map<List<VillaDto>>(villaList));
        }

        [HttpGet("{id:int},", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
              //  _logger.Log("getting error with Id " + id,"error");
                return BadRequest();
            }
            var villa = await _db.villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDTO createDto)
        {
            if (await _db.villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa  Already Exists!!!!!");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            //if (villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            Villa model=_mapper.Map<Villa>(createDto);
            //Villa model = new()
            //{
            //    Amenity = createDto.Amenity,
            //    Details = " ",
            //    ImageUrl = createDto.ImageUrl,
            //    Name = createDto.Name,
            //    Occupancy = createDto.Occupancy,
            //    Rate = createDto.Rate,
            //    sqft = createDto.sqft
            //};
         await   _db.villas.AddAsync(model);
          await  _db.SaveChangesAsync();
          //  VillaStore.villaDtos.Add(villaDto);
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);

        }

        [HttpDelete("{id:int},", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.villas.Remove(villa);
          await  _db.SaveChangesAsync();
            //   VillaStore.villaDtos.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int},", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }
            Villa model=_mapper.Map<Villa>(updateDto);
            //Villa model = new()
            //{
            //    Amenity = updateDto.Amenity,
            //    Details = " ",
            //    Id= updateDto.Id,
            //    ImageUrl = updateDto.ImageUrl,
            //    Name = updateDto.Name,
            //    Occupancy = updateDto.Occupancy,
            //    Rate = updateDto.Rate,
            //    sqft = updateDto.sqft
            //};
            _db.villas.Update(model);
          await  _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int},", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
        {
            if (patchDto == null||id==0)
            {
               return BadRequest();

            }
            var villa =await _db.villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            VillaUpdateDTO villaDto = _mapper.Map<VillaUpdateDTO>(villa);
           

            patchDto.ApplyTo(villaDto, ModelState);
            Villa model = _mapper.Map<Villa>(villaDto);
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.villas.Update(model);
           await _db.SaveChangesAsync();

            return NoContent() ;
        }



    }
}
