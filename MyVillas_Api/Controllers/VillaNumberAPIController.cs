using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVillas_Api.Data;
using MyVillas_Api.Logging;
using MyVillas_Api.Models;
using MyVillas_Api.Models.Dto;
using MyVillas_Api.Repository.IRepository;
using System.Net;

namespace MyVillas_Api.Controllers
{
    [Route("api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            this._response = new();
            _dbVilla = dbVilla;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int},", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {


                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDto)
        {
            try
            {


                if (await _dbVillaNumber.GetAsync(u => u.VillNo == createDto.VillNo) != null)
                {
                    ModelState.AddModelError("", "Villa Number  Already Exists!!!!!");
                    return BadRequest(ModelState);
                }

                if (await _dbVilla.GetAsync(u=>u.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("", "Villa Id  id Invalid!!!!!");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }


                VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDto);

                await _dbVillaNumber.CreateAsync(villaNumber);
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villaNumber.VillNo }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpDelete("{id:int},", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                await _dbVillaNumber.RemoveAsync(villaNumber);
                // await  _db.SaveChangesAsync();
                //   VillaStore.villaDtos.Remove(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int},", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.VillNo)
                {
                    return BadRequest();
                }
                if (await _dbVilla.GetAsync(u => u.Id == updateDto.VillaId) == null)
                {
                    ModelState.AddModelError("", "Villa Id  id Invalid!!!!!");
                    return BadRequest(ModelState);
                }
                VillaNumber model = _mapper.Map<VillaNumber>(updateDto);

                await _dbVillaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpPatch("{id:int},", Name = "UpdatePartialVillaNumber")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDTO> patchDto)
        //{
        //    if (patchDto == null || id == 0)
        //    {
        //        return BadRequest();

        //    }
        //    var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillNo == id, tracked: false);
        //    if (villaNumber == null)
        //    {
        //        return BadRequest();
        //    }
        //    VillaNumberUpdateDTO villaNumberDto = _mapper.Map<VillaUpdateDTO>(villaNumber);


        //    patchDto.ApplyTo(villaNumberDto, ModelState);
        //    VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDto);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    await _dbVillaNumber.UpdateAsync(model);



        //    return NoContent();
        //}



    }
}
