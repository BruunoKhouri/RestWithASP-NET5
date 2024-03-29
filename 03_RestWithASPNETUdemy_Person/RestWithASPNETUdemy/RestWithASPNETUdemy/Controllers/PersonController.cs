﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetPerson(long id)
        {
            var person = _personService.FindById(id);

            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {

            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        }

       
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }

        [HttpPost("{id}/curriculo-pdf")]
        public IActionResult UploadCurriculoPdf(long id, [FromForm] IFormFile curriculoPdf)
        {
            try
            {
                if (curriculoPdf == null || curriculoPdf.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                _personService.UploadCurriculoPdf(id, curriculoPdf);


                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpGet("{id}/curriculo-pdf")]
        public IActionResult GetCurriculoPdf(long id)
        {
            var curriculoPdfBytes = _personService.GetCurriculoPdf(id);

            if (curriculoPdfBytes == null)
            {
                return NotFound("Curriculo PDF not found");
            }

            return File(curriculoPdfBytes, "application/pdf", "Curriculo.pdf");
        }

    }
}
