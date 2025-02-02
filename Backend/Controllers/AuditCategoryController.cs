using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Attrabutes;
using Backend.Dtos.AuditCategoryDtos;
using Backend.Dtos.AuditQuestionsDto;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/apiauditCategory")]
    [ApiController]
    public class AuditCategoryController : ControllerBase
    {
        private readonly IAuditCategoryRepository _AuditCateRepo;

        private readonly IUserValidationService _userValid;

        private readonly IAuditRepository _AuditRepo; 

        private readonly IAuditQuestionRepository _AuditQuestionRepo;

        public AuditCategoryController(IAuditCategoryRepository AuditCaterepo,IUserValidationService uservalid,IAuditRepository AuditRepo, IAuditQuestionRepository AuditQuestionRepo){
                _AuditCateRepo = AuditCaterepo;
                _userValid = uservalid;
                _AuditRepo = AuditRepo;
                _AuditQuestionRepo = AuditQuestionRepo;
        }
     

        [HttpGet("{id:int}")]
       // [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> GetAuditCategory(int id){ //Bug doesn't display answer
            List<string> permissions = new List<string>() {"Admin","Auditor","ProjectManagment","Owner"};
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var ProjectValidation =  await _userValid.ValidationUserAndProjectAsync(username, projectId, role,permissions);

            if(ProjectValidation == false){
                return Unauthorized();
                }
            
            var AuditCate = await _AuditCateRepo.GetByAuditId(id,projectId);

            if(AuditCate == null){
                return NotFound();
            }
            return Ok(AuditCate);
        }
        [HttpPost("add-category/{id:int}")]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> CreateAuditCategorybyId([FromBody]CreateAuditCategoryDto auditCategoryDto,[FromRoute]int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var Audit = await _AuditRepo.GetAuditById(id,projectId);
            if(Audit == null)return NotFound();
            var AuditCategoryDto = auditCategoryDto.CreateAuditCategory(id);
            await _AuditCateRepo.CreateAuditCategory(AuditCategoryDto);
                foreach(var questionDto in auditCategoryDto.AuditQuestions){
                    var auditQuestion = questionDto.toAuditQuestion(AuditCategoryDto.Id);
                    await _AuditQuestionRepo.CreateAuditQuestion(auditQuestion);
                }
            return Ok (auditCategoryDto);
        } 

        [HttpPost("add-question/{CategoryId:int}/{AuditId:int}")]
        [ValidateUser("Admin","Auditor","ProjectManagement","Owner")]
        public async Task<IActionResult> CreateQuestionbyAuditCategoryId([FromRoute] int AuditId,[FromRoute]int CategoryId, [FromBody]AuditQuestionDto auditquestionModel){        
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);   
            var Audit = await _AuditRepo.GetAuditById(AuditId,projectId);
            if(Audit == null) return NotFound();
            var auditQuestion = auditquestionModel.toAuditQuestion(CategoryId);
            await _AuditQuestionRepo.CreateAuditQuestion(auditQuestion);

            return Ok(auditQuestion);
        }

        
        [HttpPut("{AuditId:int}/{QuestionId:int}")]
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")]
        public async Task<IActionResult> EditQuestionById([FromRoute] int AuditId,[FromRoute] int QuestionId,[FromBody] AuditUpdateQuestionDto QuestionModel){
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var Audit = await _AuditRepo.GetAuditById(AuditId,projectId);
            if(Audit == null) return NotFound();
            var AuditQuestion = await _AuditQuestionRepo.UpdateAuditQuestion(QuestionId,QuestionModel);
            if(AuditQuestion == null) return NotFound();
            return Ok(AuditQuestion);
        }

        [HttpPut("{auditId:int}/{auditCategoryId}")] //Problems requring quesiton text ? and in the controller I should add a Post for questions
        public async Task<IActionResult> UpdateCategory([FromRoute] int auditId, [FromRoute] int auditCategoryId,[FromBody]UpdatAuditCategory auditCategoryModel ){
              if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            List<string> permissions = new List<string>() {"Admin","Auditor","ProjectManagment","Owner"};
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var ProjectValidation =  await _userValid.ValidationUserAndProjectAsync(username, projectId, role,permissions);
            if(ProjectValidation == false){
                return Unauthorized();
                }

            var Audit = await _AuditRepo.GetAuditById(auditId,projectId);

            if(Audit == null){
                return NotFound();
            }

            var UpdatAuditCategory = await _AuditCateRepo.UpdateAuditCategory(auditId,auditCategoryId,auditCategoryModel);

            if(UpdatAuditCategory == null){
                return NotFound();
            }

            return Ok(UpdatAuditCategory);




        }
        [HttpDelete("{auditID:int}/{auditCategoryId:int}")] 
        [ValidateUser("Admin", "Auditor","ProjectManagment","Owner")] //Need to make a dto for the delete
        public async Task<IActionResult> DeleteAuditCategory([FromRoute] int auditID, [FromRoute] int auditCategoryId){
            var projectId = Convert.ToInt32(User.FindFirst("Project")?.Value);
            var Audit = await _AuditRepo.GetAuditById(auditID,projectId);
            if(Audit == null) return NotFound();
            var AuditCategory = await  _AuditCateRepo.RemoveAuditCategory(auditID,auditCategoryId);

            if(AuditCategory == null) return NotFound();

            return Ok("Deleted");
        }

    }
}