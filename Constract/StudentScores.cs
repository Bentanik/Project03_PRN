using CsvHelper.Configuration.Attributes;
namespace Constract;

public class StudentScores
{
    [Name("sbd")]
    public string SBD { get; set; }

    [Name("toan")]
    public double? Toan { get; set; }

    [Name("ngu_van")]
    public double? NguVan { get; set; }

    [Name("ngoai_ngu")]
    public double? NgoaiNgu { get; set; }

    [Name("vat_li")]
    public double? VatLi { get; set; }

    [Name("hoa_hoc")]
    public double? HoaHoc { get; set; }

    [Name("sinh_hoc")]
    public double? SinhHoc { get; set; }

    [Name("lich_su")]
    public double? LichSu { get; set; }

    [Name("dia_li")]
    public double? DiaLi { get; set; }

    [Name("gdcd")]
    public double? GDCD { get; set; }

    [Name("ma_ngoai_ngu")]
    public string MaNgoaiNgu { get; set; }

    public double? TongDiemKhoiA
    {
        get
        {
            // Tính tổng điểm khối A, bỏ qua nếu một trong các môn bị null
            if (Toan.HasValue && VatLi.HasValue && HoaHoc.HasValue)
            {
                return Toan.Value + VatLi.Value + HoaHoc.Value;
            }
            return null;
        }
    }

    public string? Tinh { get; set; } = "123";
}