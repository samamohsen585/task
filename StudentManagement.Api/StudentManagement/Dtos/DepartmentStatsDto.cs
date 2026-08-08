namespace StudentManagement.Api.Dtos
{
    public class DepartmentStatsDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public double AverageAge { get; set; }
        public int OldestAge { get; set; }
        public int YoungestAge { get; set; }
    }

    public class HighestLowestDeptDto
    {
        public string HighestDepartment { get; set; } = string.Empty;
        public string LowestDepartment { get; set; } = string.Empty;
    }
}