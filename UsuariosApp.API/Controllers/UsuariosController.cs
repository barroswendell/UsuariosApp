using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Dtos;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //atributo somente leitura da interface que iremos acessar
        private readonly IUsuarioDomainService _usuarioDomainService;

        //construtor para inicializar a interface (injeção de dependência)
        public UsuariosController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }


        [Route("autenticar")]
        [HttpPost]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDto), 200)]
        public IActionResult Autenticar([FromBody] AutenticarUsuarioRequestDto request)
        {
            try
            {
                //HTTP 200 (OK)
                return StatusCode(200, _usuarioDomainService.AutenticarUsuario(request));

            }
            catch (ApplicationException e)
            {
                //HTTP 401 (UNAUTHORIZED)
                return StatusCode(401, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }


        [Route("criar")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarUsuarioResponseDto), 201)]
        public IActionResult Criar([FromBody] CriarUsuarioResquetDto request)
        {
            try
            {
                //HTTP 201 (CREATED)
                return StatusCode(201, _usuarioDomainService.CriarUsuario(request));
            }
            catch (ValidationException e)
            {
                //HTTP 400 (BAD REQUEST)
                return BadRequest(e.Errors.Select(e => e.ErrorMessage));
            }
            catch (ApplicationException e)
            {
                //HTTP 422 (UNPROCESSABLE ENTITY)
                return UnprocessableEntity(new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]//somente usuários autenticados podem acessar este endpoint
        [Route("minha-conta")]
        [HttpGet]
        [ProducesResponseType(typeof(MinhaContaResponseDto), 200)]
        public IActionResult Get()
        {
            try
            {
                //HTTP 200 (OK)
                return StatusCode(200, _usuarioDomainService.MinhaConta(User.Identity.Name));
            }
            catch (ApplicationException e)
            {
                //HTTP 422 (UNAUTHORIZED)
                return StatusCode(401, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }
    }
}



