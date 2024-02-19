using Quan_Ly_D22_TH02;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SinhVienTH02> danhSach = new List<SinhVienTH02>();

            //Quy tac Add ptu truc tiep vao danh sach trong ham main:
            //+ MaSV: DH******** (Khong duoc trung)
            //+ Ho ten: Binh thuong
            //+ Nam sinh: Tu 2000 den 2004
            //+ Que quan: Binh thuong
            //+ Gioi tinh: NAM hoac NU
            danhSach.Add(new SinhVienTH02("DH11111111", "Anh", 2000, "HCM", "NU"));
            danhSach.Add(new SinhVienTH02("DH22222222", "Hieu", 2004, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH33333333", "Cuong", 2003, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH44444444", "Nam", 2002, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH55555555", "Phat", 2004, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH66666666", "Hang", 2000, "HCM", "NU"));
            danhSach.Add(new SinhVienTH02("DH77777777", "Khoa", 2000, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH88888888", "Khai", 2000, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH99999999", "Nghia", 2000, "HCM", "NAM"));
            danhSach.Add(new SinhVienTH02("DH99999999", "Bang", 2000, "HCM", "NAM"));

            List<SinhVienTH02> danhSachMoi = new List<SinhVienTH02>();
            QuanLy.Menu(ref danhSach, ref danhSachMoi);
        }
    }
}