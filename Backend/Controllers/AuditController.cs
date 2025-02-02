using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Attrabutes;
using Backend.Dtos.AuditDtoFolder;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/audits")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        
        private readonly IAuditRepository _AuditRepo;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IUserValidationService _userValid;

        private readonly UserManager<AppUser> _userManager;

        private readonly IRiskCalculationService _riskService;

        public AuditController(IAuditRepository AuditRepo,UserManager<AppUser> userManager, IUserValidationService uservalid, IRiskCalculationService riskservice){
            
            _AuditRepo = AuditRepo;
            _userValid = uservalid;
            _userManager = userManager;
            _riskService = riskservice;
        }
        [HttpPut("{id:int}/risk")]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> GetAuditRisk(int id)
        {
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var audit = await _AuditRepo.GetFullAuditDetails(id,projectId);
            if(audit == null) return NotFound();
            var risk = _riskService.CalculateAuditRisks(audit);
            var UpdateRisk = await _AuditRepo.UpdateRiskScore(audit,risk);
            return Ok(UpdateRisk);
        }
        [HttpGet]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> GetAuditsQuery([FromQuery]QueryObject? query){
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            query ??= new QueryObject(); 
            var audit = await _AuditRepo.GetAuditQueryable(query,projectId);
            if(audit == null){
                return NotFound();
            }
            var AuditDto = audit.Select(x=> x.toAuditDto()).ToList();
            return Ok(AuditDto);
        }
        [HttpPost]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> CreateAudit([FromBody]CreateAuditDto audit)       
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var Appuser = await _userManager.FindByNameAsync(username); 
            var Audit = await _AuditRepo.CreateAudit(audit.toCreateAudit(Appuser,projectId));
            return CreatedAtAction(nameof(GetAuditFromid), new {id =Audit.AuditID}, Audit.toAuditDto());
        }
        [HttpGet("{id:int}")]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> GetAuditFromid(int id){
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var audit = await _AuditRepo.GetFullAuditDetails(id,projectId);
            if(audit == null){
                return NotFound();
            }
            return Ok(audit); 
        
        }
        [HttpDelete("{id:int}")]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> DeleteAuditFromid([FromRoute]int id){
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var remove = await _AuditRepo.DeleteAuditById(id,projectId);
            if(remove == null){
                return NotFound("Audit not found");
            }
            return Ok(remove.toAuditDto());

        }

        [HttpGet("export/{auditId}")]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> ExportAuditCsv(int auditId){
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);      
            var Audit = await _AuditRepo.GetAuditById(auditId,projectId);
            if(Audit == null) return NotFound();        
            var csvBytes = await _AuditRepo.GenereateAuditCsv(auditId);
            if (csvBytes == null) return NotFound("Audit not found.");
            return File(csvBytes, "text/csv", $"audit_{auditId}.csv");
            }

         }
    }
