using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos.Csv
{
    public class AuditCsvDto
    {
    public string AuditTitle { get; set; }
    public DateTime CreatedOn { get; set; }
    public double AuditRiskLevel { get; set; }

    public string CategoryName { get; set; }
    public double CategoryRiskLevel { get; set; }

    public string QuestionText { get; set; }
    public int QuestionRisk { get; set; }
    public string QuestionAnswer { get; set; }
    }
}