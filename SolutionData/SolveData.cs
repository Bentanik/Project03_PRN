using Constract;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace SolutionData;

public class SolveData
{
    private List<StudentScores> _studentScores;

    public SolveData()
    {
        _studentScores = ReadCsvFile("D:\\WBF\\Project03\\Project03\\diem_thi_thpt_2024.csv");
    }

    public static List<Province> GetProvinces()
    {
        return new List<Province>
        {
            new Province { Code = "01", Name = "Hà Nội" },
            new Province { Code = "02", Name = "Hà Giang" },
            new Province { Code = "04", Name = "Cao Bằng" },
            new Province { Code = "06", Name = "Bắc Kạn" },
            new Province { Code = "08", Name = "Tuyên Quang" },
            new Province { Code = "10", Name = "Lào Cai" },
            new Province { Code = "11", Name = "Điện Biên" },
            new Province { Code = "12", Name = "Lai Châu" },
            new Province { Code = "14", Name = "Sơn La" },
            new Province { Code = "15", Name = "Yên Bái" },
            new Province { Code = "17", Name = "Hoà Bình" },
            new Province { Code = "19", Name = "Thái Nguyên" },
            new Province { Code = "20", Name = "Lạng Sơn" },
            new Province { Code = "22", Name = "Quảng Ninh" },
            new Province { Code = "24", Name = "Bắc Giang" },
            new Province { Code = "25", Name = "Bắc Ninh" },
            new Province { Code = "26", Name = "Hưng Yên" },
            new Province { Code = "27", Name = "Thái Bình" },
            new Province { Code = "30", Name = "Hà Nam" },
            new Province { Code = "31", Name = "Nam Định" },
            new Province { Code = "33", Name = "Ninh Bình" },
            new Province { Code = "34", Name = "Thanh Hóa" },
            new Province { Code = "35", Name = "Nghệ An" },
            new Province { Code = "36", Name = "Hà Tĩnh" },
            new Province { Code = "37", Name = "Quảng Bình" },
            new Province { Code = "38", Name = "Quảng Trị" },
            new Province { Code = "40", Name = "Thừa Thiên Huế" },
            new Province { Code = "42", Name = "Đà Nẵng" },
            new Province { Code = "44", Name = "Quảng Nam" },
            new Province { Code = "45", Name = "Quảng Ngãi" },
            new Province { Code = "46", Name = "Bình Định" },
            new Province { Code = "48", Name = "Phú Yên" },
            new Province { Code = "49", Name = "Khánh Hòa" },
            new Province { Code = "51", Name = "Ninh Thuận" },
            new Province { Code = "52", Name = "Bình Thuận" },
            new Province { Code = "54", Name = "Tây Ninh" },
            new Province { Code = "56", Name = "Bình Dương" },
            new Province { Code = "58", Name = "Đồng Nai" },
            new Province { Code = "60", Name = "Bà Rịa - Vũng Tàu" },
            new Province { Code = "62", Name = "Hồ Chí Minh" },
            new Province { Code = "64", Name = "Long An" },
            new Province { Code = "66", Name = "Tiền Giang" },
            new Province { Code = "67", Name = "Bến Tre" },
            new Province { Code = "68", Name = "Trà Vinh" },
            new Province { Code = "69", Name = "Vĩnh Long" },
            new Province { Code = "70", Name = "Cần Thơ" },
            new Province { Code = "72", Name = "Hậu Giang" },
            new Province { Code = "74", Name = "Sóc Trăng" },
            new Province { Code = "75", Name = "Bạc Liêu" },
            new Province { Code = "77", Name = "Cà Mau" }
        };
    }

    public List<StudentScores> ReadCsvFile(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null,
            HeaderValidated = null
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<StudentScores>().ToList();
            return records;
        }
    }
    public List<StudentScores> ProcessData(List<StudentScores> students)
    {
        // Trích xuất 2 số đầu từ số báo danh để xác định tỉnh
        foreach (var student in students)
        {
            student.Tinh = student.SBD.Length >= 2 ? student.SBD.Substring(0, 2) : "Unknown";
        }

        return students;
    }

    public static string GetProvinceNameByCode(string code)
    {
        List<Province> list = GetProvinces();
        var province = GetProvinces().FirstOrDefault(p => p.Code == code);
        return province?.Name ?? "Unknown";
    }

    public List<ProvinceMath> CalculateAverageScoresByProvince(List<StudentScores> students)
    {
        var averages = students
            .GroupBy(s => s.Tinh)
            .Select(g => new ProvinceMath
            {
                Code = g.Key,
                Name = GetProvinceNameByCode(g.Key),
                AvgTotal = g.Average(s => s.Toan.GetValueOrDefault())
            })
            .ToList();

        return averages;
    }

    public (List<ProvinceMath> Top5Highest, List<ProvinceMath> Top5Lowest) GetTopAndBottom5(List<ProvinceMath> averages)
    {
        var sortedAverages = averages.OrderByDescending(a => a.AvgTotal).ToList();

        var top5Highest = sortedAverages.Take(5).ToList();
        var top5Lowest = sortedAverages.OrderBy(a => a.AvgTotal).Take(5).ToList();

        return (top5Highest, top5Lowest);
    }

    public (int khoaHocTuNhienCount, int khoaHocXaHoiCount) CountParticipantsByGroupExcludingMathLangEnglish(List<StudentScores> students)
    {
        int khoaHocTuNhienCount = students.Count(s =>
            (s.VatLi.HasValue || s.HoaHoc.HasValue || s.SinhHoc.HasValue) &&
            !(s.Toan.HasValue && s.NguVan.HasValue && s.NgoaiNgu.HasValue));

        int khoaHocXaHoiCount = students.Count(s =>
            (s.LichSu.HasValue || s.DiaLi.HasValue || s.GDCD.HasValue) &&
            !(s.Toan.HasValue && s.NguVan.HasValue && s.NgoaiNgu.HasValue));

        return (khoaHocTuNhienCount, khoaHocXaHoiCount);
    }

    public (List<ProvinceMath> Top5Highest, List<ProvinceMath> Top5Lowest) QuestionOne()
    {
        _studentScores = ProcessData(_studentScores);

        var averages = CalculateAverageScoresByProvince(_studentScores);

        var top5 = GetTopAndBottom5(averages).Top5Highest;
        var bottom5 = GetTopAndBottom5(averages).Top5Lowest;

        return (top5, bottom5);
    }

    public StudentScores QuestionTwo()
    {
        // Tìm học sinh có tổng điểm khối A cao nhất
        return _studentScores
            .Where(s => s.TongDiemKhoiA.HasValue) // Bỏ qua những học sinh không đủ điểm để tính khối A
            .OrderByDescending(s => s.TongDiemKhoiA) // Sắp xếp theo tổng điểm khối A giảm dần
            .FirstOrDefault(); // Lấy học sinh có tổng điểm cao nhất
    }

   

    public SubjectParticipantCounts QuestionThree()
    {
        var counts = new SubjectParticipantCounts
        {
            Toan = _studentScores.Count(s => s.Toan.HasValue),
            VatLi = _studentScores.Count(s => s.VatLi.HasValue),
            HoaHoc = _studentScores.Count(s => s.HoaHoc.HasValue),
            NguVan = _studentScores.Count(s => s.NguVan.HasValue),
            SinhHoc = _studentScores.Count(s => s.SinhHoc.HasValue),
            LichSu = _studentScores.Count(s => s.LichSu.HasValue),
            DiaLi = _studentScores.Count(s => s.DiaLi.HasValue),
            GDCD = _studentScores.Count(s => s.GDCD.HasValue),
            NgoaiNgu = _studentScores.Count(s => s.NgoaiNgu.HasValue)
        };

        return counts;
    }

    public (int khoaHocTuNhienCount, int khoaHocXaHoiCount) QuestionFour()
    {
        int khoaHocTuNhienCount = _studentScores.Count(s =>
           (s.VatLi.HasValue || s.HoaHoc.HasValue || s.SinhHoc.HasValue) &&
           !(s.Toan.HasValue && s.NguVan.HasValue && s.NgoaiNgu.HasValue));

        int khoaHocXaHoiCount = _studentScores.Count(s =>
            (s.LichSu.HasValue || s.DiaLi.HasValue || s.GDCD.HasValue) &&
            !(s.Toan.HasValue && s.NguVan.HasValue && s.NgoaiNgu.HasValue));

        return (khoaHocTuNhienCount, khoaHocXaHoiCount);
    }


}
