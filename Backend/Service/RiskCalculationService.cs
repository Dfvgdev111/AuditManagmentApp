using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Backend.Database;
using Backend.Dtos.AuditDetailsDto;
using Backend.Enums;
using Backend.Models;

namespace Backend.Service
{
    public interface IRiskCalculationService
    {
        public double CalculateCategoryRisks(AuditCategoriesDTO category);
        public double  CalculateAuditRisks(AuditDto audit);
    }


    public class RiskCalculationService : IRiskCalculationService
    {
        private readonly ILogger<RiskCalculationService> _logger;


        public RiskCalculationService(ILogger<RiskCalculationService> logger)
        {
            _logger = logger;
            _logger.LogInformation("RiskCaculationService Instantiad");
        }
    
        public int GetRiskValue (RiskLevelEnum riskLevel){

            return riskLevel switch 
            {
                RiskLevelEnum.High => 4,
                RiskLevelEnum.Medium => 2,
                RiskLevelEnum.low => 1,
                _=> 0
            };
        }

        public double CalculateCategoryRisks(AuditCategoriesDTO category)
        {
            if(category.QuestionDtos == null || !category.QuestionDtos.Any())
            return 0.0;

         var filteredQuestions = category.QuestionDtos
            .Where(x => x.QuestionAnswer == false)
            .Select(q => GetRiskValue(q.RiskLevel));

            if(!filteredQuestions.Any())
            return 0.0;
         
            double averagescore = filteredQuestions.Average();
            return averagescore;         

        }
public double CalculateAuditRisks(AuditDto audit)
{

    List<double> AuditRiskCaculation = new List<double> {};

    var AuditModel = audit.AuditCategories.ToList();

    int count = AuditModel.Count();

    for(int i = 0; i < count; i++){
       var riskvalue =  CalculateCategoryRisks(AuditModel[i])  * AuditModel[i].RiskLevel;
       AuditRiskCaculation.Add(riskvalue);
    }

    if(!AuditRiskCaculation.Any())
    return 0.0;

   double  AuditRiskCaculationResult  =  AuditRiskCaculation.Average();
   return AuditRiskCaculationResult;
}

    }
}