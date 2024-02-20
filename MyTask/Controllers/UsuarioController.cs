﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace MyTask.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("v1/usuario")]
        public async Task<IActionResult> GetAsync([FromServices] DbContextImpacta context)
        {
            try
            {
                var usuarios = await context.Usuarios.ToListAsync();
                return Ok(usuarios);
            }
            catch
            {
                return StatusCode(500, "EX 001 -  Não foi possível recuperar os usuários");
            }
        }

        [HttpGet("v1/usuarios/{id:int}")]
        public async Task<IActionResult> GetByIdassync([FromServices] DbContextImpacta context, int id)
        {
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario == null)
                    return NotFound("Conteúdo não encontrado");

                return Ok(usuario);
            }
            catch
            {
                return StatusCode(500, ("Fala interna no servidor"));

            }
        }

        [HttpPost("v1/usuarios/")]
        public async Task<IActionResult> Post([FromServices] DbContextImpacta context, [FromBody] Usuario model)
        {
            try
            {
                await context.Usuarios.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/usuarios/{model.Id}",(model));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,("EX 001 -  Não foi possível Cadastrar usuario"));

            }
            catch
            {
                return StatusCode(500, ("EX 002 - Falha interna no servidor"));

            }
        }

        [HttpPut("v1/usuarios/{id:int}")]
        public async Task<IActionResult> Put([FromServices] DbContextImpacta context, [FromBody] Usuario model, int id)
        {
        
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario == null)
                    return NotFound("Conteúdo não encontrado");

                usuario.Nome = model.Nome;
                usuario.Email = model.Email;
                
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();

                return Ok(usuario);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,("EX 005 -  Não foi possível atualizar a categoria"));

            }
            catch (Exception e)
            {
                return StatusCode(500,("EX 002 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/usuarios/{id:int}")]
        public async Task<IActionResult> Delete([FromServices] DbContextImpacta context, int id)
        {
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario == null)
                    return NotFound("Conteúdo não encontrado");

                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,("EX 005 -  Não foi possível Excluir a categoria"));
            }
            catch (Exception e)
            {
                return StatusCode(500,("EX 002 - Falha interna no servidor"));
            }
        }
    }
}