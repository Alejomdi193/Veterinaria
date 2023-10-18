using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{

    public class TratamientoMedicoController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TratamientoMedicoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<TratamientoMedicoDto>>> Get()
        {
            var tratamientomedico = await unitOfWork.TratamientoMedicos.GetAllAsync();
            return mapper.Map<List<TratamientoMedicoDto>>(tratamientomedico);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TratamientoMedicoDto>> GetByIdAsync(int id)
        {
            var tratamientoMedico = await unitOfWork.TratamientoMedicos.GetByIdAsync(id);
            if (tratamientoMedico == null)
            {
                return NotFound();
            }
            return mapper.Map<TratamientoMedicoDto>(tratamientoMedico);
        }  

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<TratamientoMedico>> Post(TratamientoMedicoDto tratamientoMedicoDto)
        {
            var tratamientoMedico = mapper.Map<TratamientoMedico>(tratamientoMedicoDto);
            unitOfWork.TratamientoMedicos.Add(tratamientoMedico);
            await unitOfWork.SaveAsync();
            if (tratamientoMedico== null)
            {
                return BadRequest();
            }

            tratamientoMedico.Id = tratamientoMedico.Id;
            return CreatedAtAction(nameof(Post), new { id = tratamientoMedico.Id }, tratamientoMedicoDto);
        } 

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<TratamientoMedicoDto>> Put(int id, [FromBody]TratamientoMedicoDto tratamientoMedicoDto)
        {
            if (tratamientoMedicoDto == null)
            {
                return BadRequest();
            }
            var tratamientoMedico = mapper.Map<TratamientoMedico>(tratamientoMedicoDto);
            unitOfWork.TratamientoMedicos.Update(tratamientoMedico);
            await unitOfWork.SaveAsync();
            return tratamientoMedicoDto;
        } 
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var tratamientomedico = await unitOfWork.TratamientoMedicos.GetByIdAsync(id);
            if (tratamientomedico == null)
            {
                return NotFound();
            }

            unitOfWork.TratamientoMedicos.Remove(tratamientomedico);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
        
    }
}