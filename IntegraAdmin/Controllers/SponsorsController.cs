using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegraAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISponsorRepository _sponsorRepo;
        private readonly IUnitOfWork _unitOfWork;

        public SponsorsController(IMapper mapper, ISponsorRepository sponsorRepo, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _sponsorRepo = sponsorRepo;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetSponsors()
        {
            var sponsors = await _sponsorRepo.AllSponsors();
            var sponsorsResource = _mapper.Map<List<Sponsor>, List<SponsorResource>>(sponsors);
            return Ok(sponsorsResource);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSponsor(int id)
        {
            var sponsor = await _sponsorRepo.GetSponsor(id);

            if (sponsor == null)
                return NotFound();

            var sponsorResource = _mapper.Map<Sponsor, SponsorResource>(sponsor);
            return Ok(sponsorResource);
        }

        [Authorize(Roles = "Admin, Sponsor Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSponsor(int id, [FromBody] SponsorResource sponsorRes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sponsor = await _sponsorRepo.GetSponsor(id);

            if (sponsor == null)
                return NotFound();

            // update the sponsor
            _mapper.Map<SponsorResource, Sponsor>(sponsorRes, sponsor);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Sponsor, SponsorResource>(sponsor);

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSponsor([FromBody] SponsorResource sponsorResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sponsor = _mapper.Map<SponsorResource, Sponsor>(sponsorResource);

            _sponsorRepo.AddSponsor(sponsor);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Sponsor, SponsorResource>(sponsor);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sponsor = await _sponsorRepo.GetSponsor(id);

            if (sponsor == null)
                return NotFound();

            // delete the sponsor
            _sponsorRepo.RemoveSponsor(sponsor);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
