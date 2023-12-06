
using Azure;
using MagicVilla.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVilla _villa;
        private readonly IMapper _mapper;
        private readonly ILogger<VillaController> _logger;

        public VillaController(IVilla villa, IMapper mapper,ILogger<VillaController> logger)
        {
            _villa = villa;
            _mapper = mapper;
            _logger = logger;
            this._response = new();
        }

        [HttpGet("AllVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Getall()
        {
            try
            {
                var villas = await _villa.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villas);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessage=new List<string> { ex.Message};
            }
            return _response;
        }
        [HttpGet("{id:int}",Name ="GetOneVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            if (id == null || id == 0)
            {
                _logger.LogError("Error Villa with this id" + id);
                return BadRequest();
            }
            try { 
            var villa = await _villa.GetByIdAsync(v=>v.Id==id);
            if (villa == null) return NotFound();
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessage = new List<string> { ex.Message };


            }
            return BadRequest("");
        }
        [HttpPost(Name ="CreateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task< ActionResult<APIResponse>> AddNew(VillaDTO villaDTO)
        {
            if (villaDTO == null)
            {
                return BadRequest("Bad Request");
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            try { 
            if (ModelState.IsValid)
            {
                var Villa = _mapper.Map<VillaAPI>(villaDTO);
                await _villa.CreateAsync(Villa);
                _response.Result = _mapper.Map<VillaAPI>(villaDTO);
                _response.StatusCode = HttpStatusCode.Created;
                await _villa.SaveAsync();
                return CreatedAtRoute("GetOneVilla", new {id=villaDTO.Id}, _response);
            }
            }catch(Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessage = new List<string> { ex.Message };

            }
            return NotFound("Not Found");
        }
        [HttpPut("{id:int}",Name ="EditVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(id!=villaDTO.Id||villaDTO==null)
            {
                _logger.LogError("error in villa"+id);
                return BadRequest();
            }

            try {
            if (ModelState.IsValid)
            {
                //var villa = await _villa.GetVillaById(id);
                var model = _mapper.Map<VillaAPI>(villaDTO);
                //if (villa != null)
                //{
                //    //var data= _mapper.Map<VillaDTO>(villaAPI);
                //    villa.Name = villaAPI.Name;
                //    villa.CreatedDate = villaAPI.CreatedDate;
                await _villa.UpdateAsync(model);
                await _villa.SaveAsync();
                return Ok("DataSaved");
   
            }
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// / Patch villa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        //public async Task<ActionResult> UpdateParialVilla(int id,JsonPatchDocument<VillaDTO> patchDTO)
        //{
        //    if (patchDTO == null||id==null) return BadRequest();
        //    var villa =await _villa.GetVillaById(id);
        //    if(villa != null)
        //    {
        //        await patchDTO.ApplyTo(villa, ModelState);
        //    }

        //}

        [HttpDelete("{id:int}",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == null) return BadRequest();
            var villa =await _villa.GetByIdAsync(v => v.Id== id);
            try { 
            if (villa != null)
            {
                await _villa.DeleteAsync(villa);
                await _villa.SaveAsync();
                return Ok("Villa Deleted");
            }
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(ModelState);
        }
        

    }
}
