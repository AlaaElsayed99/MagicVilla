

namespace MagicVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaNumber _villaNumber;
        private readonly IMapper _mapper;
        private readonly ILogger<VillaNumberController> _logger;

        public VillaNumberController(IVillaNumber villaNumber, IMapper mapper, ILogger<VillaNumberController> logger)
        {
            _villaNumber = villaNumber;
            _mapper = mapper;
            _logger = logger;
            this._response = new();
        }

        [HttpGet("AllVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetAllVillaNumber()
        {
            try
            {
                var villaNumbers = await _villaNumber.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumbers);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetOneVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            if (id == null || id == 0)
            {
                _logger.LogError("Error Villa with this id" + id);
                return BadRequest();
            }
            try
            {
                var villaNumber = await _villaNumber.GetByIdAsync(v => v.VillaNo == id);
                if (villaNumber == null) return NotFound();
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };


            }
            return BadRequest("");
        }
        [HttpPost(Name = "CreateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AddNewVillaNumber(VillaNumberCreateDTO villaNumberDTO)
        {
            if (villaNumberDTO == null)
            {
                return BadRequest("Bad Request");
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    var VillaNumber = _mapper.Map<VillaNumber>(villaNumberDTO);
                    await _villaNumber.CreateAsync(VillaNumber);
                    _response.Result = _mapper.Map<VillaNumber>(villaNumberDTO);
                    _response.StatusCode = HttpStatusCode.Created;
                    await _villaNumber.SaveAsync();
                    return CreatedAtRoute("GetOneVillaNumber", new { id = villaNumberDTO.VillaNo }, _response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };

            }
            return NotFound("Not Found");
        }
        [HttpPut("{id:int}", Name = "EditVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditVilla(int id, [FromBody] VillaNumberUpdateDTO villaNumberDTO)
        {
            if (id != villaNumberDTO.VillaNo || villaNumberDTO == null)
            {
                _logger.LogError("error in villa" + id);
                return BadRequest();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    
                    var model = _mapper.Map<VillaNumber>(villaNumberDTO);
                    
                    await _villaNumber.UpdateAsync(model);
                    return Ok("DataSaved");

                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(ModelState);
        }

        
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == null) return BadRequest();
            var villa = await _villaNumber.GetByIdAsync(v => v.VillaNo == id);
            try
            {
                if (villa != null)
                {
                   await _villaNumber.DeleteAsync(villa);
                    await _villaNumber.SaveAsync();
                    return Ok("Villa Deleted");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return BadRequest(ModelState);
        }
    }
}
