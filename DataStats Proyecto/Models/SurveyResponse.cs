namespace DataStats.Models;

public class SurveyResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime SurveyDate { get; set; } = DateTime.Today;
    public string Zone { get; set; } = "";
    public string Provider { get; set; } = "";
    public string ConnectionType { get; set; } = "";
    public decimal MonthlyCost { get; set; }
    public decimal SpeedMbps { get; set; }
    public int HouseholdUsers { get; set; } = 1;
    public string Satisfaction { get; set; } = "";
    public int Stability { get; set; } = 3;
    public string ProblemFrequency { get; set; } = "";
    public string WouldRecommend { get; set; } = "";
    public string Improvement { get; set; } = "";
}
