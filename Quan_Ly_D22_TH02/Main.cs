using System;
using System.Collections.Generic;
using System.Threading.Channels;


namespace Quan_Ly_D22_TH02
{
    public class SinhVienTH02
    {
        private string MaSV;
        public string HoTen;
        public int NamSinh;
        public string QueQuan;
        public string GioiTinh;

        public string MaSV1 {
            get => MaSV;
            set => MaSV = value;
        }

        public SinhVienTH02(string maSV,string hoTen,int namSinh, string queQuan, string gioiTinh)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NamSinh = namSinh;
            QueQuan = queQuan;
            GioiTinh= gioiTinh;
        }

        public void Info()
        {
            Console.WriteLine($"Ma SV: {MaSV}");
            Console.WriteLine($"Ho ten: {HoTen}");
            Console.WriteLine($"Nam sinh: {NamSinh}");
            Console.WriteLine($"Que quan: {QueQuan}");
            Console.WriteLine($"Gioi tinh: {GioiTinh}");
        }
    }

    #region Interface
    interface IAdd
    {
        void ThemSinhVienVaoViTri(ref List<SinhVienTH02> danhSach);//Insert
        void ThemSinhVienCuoiDanhSach(ref List<SinhVienTH02> danhSach);//Add
        void ThemMotDanhSachVaoViTri(ref List<SinhVienTH02> danhSachSinhVienMoi, ref List<SinhVienTH02> danhSach);//Insert Range
    }

    interface IRemove
    {
        void XoaSvTheoCode(ref List<SinhVienTH02> danhSach);//Remove
        void XoaSvTheoViTri(ref List<SinhVienTH02> danhSach);//RemoveAt
    }

    interface IFilterTheList
    {
        void Loc(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachMoi);//Contains
    }

    interface ISort
    {
        void SortIncrease(ref List<SinhVienTH02> danhSach);
    }
    #endregion

    public class QuanLy : SinhVienTH02, ISort, IFilterTheList, IAdd, IRemove
    {
        public QuanLy(string maSV, string hoTen, int namSinh, string queQuan, string gioiTinh):base(maSV,hoTen,namSinh, queQuan,gioiTinh) { }

        #region Add Sinh vien
        public void addSinhVien(ref List<SinhVienTH02> danhSach)
        {
            
            //Nhap MaSv kiem tra trung va khuon dang
            Console.Write("Nhap ma sinh vien: ");
            string code = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(code)) 
                {
                    Console.Write("Ban da bo trong! Moi nhap lai: ");
                    code = Console.ReadLine();
                    continue; 
                }
                if (code.Length < 10)
                {
                    Console.Write("Nhap sai! Moi nhap lai: ");
                    code = Console.ReadLine();
                }
                if ((code[0] == 'D' || code[0] == 'd') && (code[1] == 'H' || code[1] == 'h') && code.Length == 10)
                {
                    if (danhSach.Exists(item => item.MaSV1 == code.ToUpper()))
                    {
                        Console.Write("Ma sinh vien trung! Moi nhap lai: ");
                        code = Console.ReadLine();
                    }
                    else break;
                }
                else 
                {
                    Console.Write("Nhap sai! Moi nhap lai: ");
                    code = Console.ReadLine();
                }
            }
            //Nhap Ho ten
            Console.Write("Nhap ho ten: ");
            string name = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(name)) 
                {
                    Console.Write("Ban da bo trong! Moi nhap lai: ");
                    name = Console.ReadLine();
                    continue; 
                }
                else break;
            }

            //Nhap Nam Sinh
            Console.Write("Nhap nam sinh: ");
            string tempYear = Console.ReadLine();
            int year;
            while (true)
            {
                if(int.TryParse(tempYear, out year))
                {
                    if (year < 2000 || year > 2004)
                    {
                        Console.WriteLine("Nam sinh khong hop le (Hay chac rang ban khong nham lan)!");
                        Console.Write("Moi nhap lai nam sinh: ");
                        tempYear = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi nhap lai nam sinh: ");
                    tempYear = Console.ReadLine();
                }
            }

            //Nhap que quan
            Console.Write("Nhap que quan sinh vien: ");
            string country = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(country))
                {
                    Console.Write("Ban da bo trong! Moi nhap lai: ");
                    country = Console.ReadLine();
                    continue;
                }
                else break;
            }

            //Nhap gioi tinh
            string gender;
            string genderToUpper;
            do
            {
                Console.Write("Nhap gioi tinh: ");
                gender = Console.ReadLine();
                while (true)
                {
                    if (string.IsNullOrWhiteSpace(gender))
                    {
                        Console.Write("Ban da bo trong! Moi nhap lai: ");
                        gender = Console.ReadLine();
                        continue;
                    }
                    else break;
                }
                genderToUpper = gender.ToUpper();
                if(genderToUpper == "NAM" ||  genderToUpper == "NU")
                {
                    break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi nhap lai gioi tinh: ");
                    gender = Console.ReadLine();
                }
            } while (string.IsNullOrEmpty(gender));

            SinhVienTH02 person = new SinhVienTH02(code.ToUpper(), name, year, country, gender.ToUpper());
            danhSach.Add(person);
        }
        #endregion

        //Xuat danh sach sinh vien
        public static void XuatDanhSach(List<SinhVienTH02> danhSach)
        {
            int count = 0;
            if(danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach rong!!!");
            }
            else
            {
                foreach (var item in danhSach)
                {
                    count++;
                    Console.WriteLine($"--STT{count}--");
                    item.Info();
                    Console.WriteLine();
                }
            }
            
        }

        //Sắp xếp danh sách SV tăng dần theo tên
        public void SortIncrease(ref List<SinhVienTH02> danhSach)
        {
            for (int i = 0; i < danhSach.Count - 1; i++)
            {
                for (int j = i + 1; j < danhSach.Count; j++)
                {
                    string strA = danhSach[i].HoTen;
                    string strB = danhSach[j].HoTen;

                    // Tìm phần Tên của strA và strB
                    string[] partsA = strA.Split(' ');
                    string[] partsB = strB.Split(' ');
                    string tenA = partsA[partsA.Length - 1];
                    string tenB = partsB[partsB.Length - 1];

                    // So sánh toàn bộ phần cuối của Tên
                    int compareResult = string.Compare(tenA, tenB, StringComparison.OrdinalIgnoreCase);

                    if (compareResult > 0) // Nếu phần cuối của Tên của sinh viên i lớn hơn sinh viên j, hoán đổi vị trí
                    {
                        SinhVienTH02 temp = danhSach[i];
                        danhSach[i] = danhSach[j];
                        danhSach[j] = temp;
                    }
                }
            }
        }

        public void Loc(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachMoi)//Contains
        {
            Console.WriteLine("---Tim kiem---");
            Console.WriteLine("1. Ma sinh vien.");
            Console.WriteLine("2. Ten sinh vien.");
            Console.WriteLine("3. Nam sinh.");
            Console.WriteLine("4. Que quan.");
            Console.WriteLine("5. Gioi tinh.");
            Console.WriteLine("6. Quay lai Menu chinh.");
            Console.WriteLine("7. Thoat chuong trinh.");
            Console.Write("Ban muon kiem tra gi? Chon: ");
            string tempChoose = Console.ReadLine();
            int choose;
            while(true){
                if(int.TryParse(tempChoose, out choose))
                {
                    if (choose < 0 || choose > 7)
                    {
                        Console.Write("Khong the thuc hien! Moi nhap lai: ");
                        tempChoose = Console.ReadLine();
                    }
                    else
                        break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi nhap lai: ");
                    tempChoose = Console.ReadLine();
                }
            }

            switch (choose)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Nhap ma sinh vien can tim: ");
                    string tempCheckMaSV = Console.ReadLine();
                    bool flagMaSv = false;
                    foreach (var item in danhSach)
                    {
                        if(item.MaSV1 == tempCheckMaSV.ToUpper())
                        {
                            flagMaSv = true;
                            item.Info();
                            Console.WriteLine();
                        }
                    }
                    if(!flagMaSv)
                        Console.WriteLine($"Khong tim thay sinh vien nao co ma {tempCheckMaSV}");
                    Loc(ref danhSach, ref danhSachMoi);
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("Nhap ho ten sinh vien muon tim: ");
                    string tempCheckHoTen = Console.ReadLine();
                    bool flagHoTen = false;
                    foreach (var item in danhSach)
                    {
                        if(item.HoTen == tempCheckHoTen)
                        {
                            flagHoTen = true;
                            item.Info();
                            Console.WriteLine();
                        }
                    }
                    if (!flagHoTen)
                    {
                        Console.WriteLine($"Khong tim thay sinh vien {tempCheckHoTen}");
                        Console.WriteLine("Hay thu lai va chac rang da nhap dung ho ten.");
                    }
                    Loc(ref danhSach, ref danhSachMoi);
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Nhap nam sinh sinh vien muon tim: ");
                    string tempCheckNamSinh = Console.ReadLine();
                    bool flagNamSinh = false;
                    int checkNamSinh;
                    while (true)
                    {
                        if(int.TryParse(tempCheckNamSinh, out checkNamSinh))
                        {
                            foreach (var item in danhSach)
                            {
                                if (item.NamSinh == checkNamSinh)
                                {
                                    flagNamSinh = true;
                                    item.Info();
                                    Console.WriteLine();
                                }
                            }
                            break;
                        }
                        else
                        {
                            Console.Write("Nam sinh khong hop le! Moi nhap lai");
                            tempCheckNamSinh = Console.ReadLine();
                        }
                    }

                    if (!flagNamSinh)
                    {
                        Console.WriteLine($"Khong tim thay sinh vien sinh nam {checkNamSinh}");
                        Console.WriteLine("Hay thu lai va chac rang da nhap dung nam sinh.");
                    }
                    Loc(ref danhSach, ref danhSachMoi);
                    break;
                case 4:
                    Console.Clear();
                    Console.Write("Nhap que quan sinh vien muon tim: ");
                    string tempCheckQueQuan = Console.ReadLine();
                    bool flagQueQuan = false;
                    foreach (var item in danhSach)
                    {
                        if (item.QueQuan == tempCheckQueQuan)
                        {
                            flagHoTen = true;
                            item.Info();
                            Console.WriteLine();
                        }
                    }
                    if (!flagQueQuan)
                    {
                        Console.WriteLine($"Khong tim thay sinh vien {tempCheckQueQuan}");
                        Console.WriteLine("Hay thu lai va chac rang da nhap dung que quan.");
                    }
                    Loc(ref danhSach, ref danhSachMoi);
                    break;
                case 5:
                    Console.Clear();
                    Console.Write("Nhap gioi tinh sinh vien muon tim: ");
                    string tempCheckGioiTinh = Console.ReadLine();
                    string checkGioiTinh = tempCheckGioiTinh.ToUpper();
                    bool flagGioiTinh = false;
                    foreach (var item in danhSach)
                    {
                        if(item.GioiTinh == checkGioiTinh)
                        {
                            flagGioiTinh = true;
                            item.Info();
                            Console.WriteLine();
                        }
                    }
                    if (!flagGioiTinh)
                    {
                        Console.WriteLine($"Danh sach khong ton tai sinh vien gioi tinh {checkGioiTinh}");
                    }
                    Loc(ref danhSach, ref danhSachMoi);
                    break;
                case 6:
                    Console.Clear();
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine("Da thoat chuong trinh");
                    break;
                default:
                    break;
            }
        }

        public void ThemSinhVienVaoViTri(ref List<SinhVienTH02> danhSach)//Insert
        {
            //Nhap MaSv kiem tra trung va khuon dang
            Console.Write("Nhap ma sinh vien: ");
            string code = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    Console.Write("Ban da bo trong! Moi nhap lai: ");
                    code = Console.ReadLine();
                    continue;
                }
                if(code.Length < 10)
                {
                    Console.Write("Nhap sai! Moi nhap lai: ");
                    code = Console.ReadLine();
                }
                if ((code[0] == 'D' || code[0] == 'd') && (code[1] == 'H' || code[1] == 'h') && code.Length == 10)
                {
                    if (danhSach.Exists(item => item.MaSV1 == code.ToUpper()))
                    {
                        Console.Write("Ma sinh vien trung! Moi nhap lai: ");
                        code = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi nhap lai: ");
                    code = Console.ReadLine();
                }
            }
            //Nhap Ho ten
            Console.Write("Nhap ho ten: ");
            string name = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Ban da bo trong! Moi nhap lai: ");
                    name = Console.ReadLine();
                    continue;
                }
                else break;
            }

            //Nhap Nam Sinh
            Console.Write("Nhap nam sinh: ");
            string tempYear = Console.ReadLine();
            int year;
            while (true)
            {
                if (int.TryParse(tempYear, out year))
                {
                    if (year < 2000 || year > 2004)
                    {
                        Console.WriteLine("Nam sinh khong hop le (Hay chac rang ban khong nham lan)!");
                        Console.Write("Moi nhap lai nam sinh: ");
                        tempYear = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi nhap lai nam sinh: ");
                    tempYear = Console.ReadLine();
                }
            }

            //Nhap que quan
            Console.Write("Nhap que quan sinh vien: ");
            string country = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(country))
                {
                    Console.Write("Ban da bo trong! Moi nhap lai: ");
                    country = Console.ReadLine();
                    continue;
                }
                else break;
            }

            //Nhap gioi tinh
            string gender;
            string genderToUpper;
            do
            {
                Console.Write("Nhap gioi tinh: ");
                gender = Console.ReadLine();
                while (true)
                {
                    if (string.IsNullOrWhiteSpace(gender))
                    {
                        Console.Write("Ban da bo trong! Moi nhap lai: ");
                        gender = Console.ReadLine();
                        continue;
                    }
                    else break;
                }
                genderToUpper = gender.ToUpper();
                if (genderToUpper == "NAM" || genderToUpper == "NU")
                {
                    break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi nhap lai gioi tinh: ");
                    gender = Console.ReadLine();
                }
            } while (string.IsNullOrEmpty(gender));

            SinhVienTH02 person = new SinhVienTH02(code.ToUpper(), name, year, country, gender.ToUpper());
            Console.WriteLine("---Thong tin sinh vien se chen vao danh sach---");
            person.Info();
            Console.Write("Nhap vi tri muon chen vao danh sach: ");
            string tempIndex = Console.ReadLine();
            int index;
            while (true)
            {
                if(int.TryParse(tempIndex, out index))
                {
                    if (index < 0 || index > danhSach.Count)
                    {
                        Console.Write("Vi tri khong hop le! Nhap lai vi tri: ");
                        tempIndex = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.WriteLine("Nhap sai! Nhap lai vi tri: ");
                    tempIndex = Console.ReadLine();
                }
            }
            danhSach.Insert(index, person);
        }

        //Them mot sinh vien vao cuoi danh sach
        public void ThemSinhVienCuoiDanhSach(ref List<SinhVienTH02> danhSach)//Add
        {
            QuanLy newStu = new QuanLy("", "", 0, "", "");
            newStu.addSinhVien(ref danhSach);
            Console.WriteLine("Da them sinh vien vao danh sach...");
        }

        //Them mot danh sach A  vao vi tri nao do trong mot Danh Sach co san
        //Sau khi them xong danh Sach A se bi xoa toan bo phan tu
        public void ThemMotDanhSachVaoViTri(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachSinhVienMoi)//Insert Range
        {
            // Can phai tao san 1 list moi de nguoi dung co the dung chuc nang nay bat cu luc nao
            QuanLy newStu = new QuanLy("", "", 0, "", "");
            int choose;
            string option;
            do
            {
                newStu.addSinhVien(ref danhSachSinhVienMoi);
                Console.WriteLine("Ban co muon nhap tiep khong?");
                Console.WriteLine("1. Co\n2. Khong");
                Console.Write("Chon: ");
                option = Console.ReadLine();
                while (true)
                {
                    if(int.TryParse(option, out choose))
                    {
                        if (choose < 0 || choose > 2)
                        {
                            Console.WriteLine("Chon sai! Ban co muon nhap tiep khong?");
                            Console.WriteLine("1. Co\n2. Khong");
                            Console.Write("Chon: ");
                            option = Console.ReadLine();
                        }
                        else break;
                    }
                    else
                    {
                        Console.WriteLine("Chon sai! Ban co muon nhap tiep khong?");
                        Console.WriteLine("1. Co\n2. Khong");
                        Console.Write("Chon: ");
                        option = Console.ReadLine();
                    }
                }
            } while (choose == 1);
            Console.WriteLine("-----Danh sach ban vua tao bao gom cac sinh vien-----");
            foreach (var item in danhSachSinhVienMoi)
            {
                item.Info();
                Console.WriteLine();
            }

            Console.WriteLine("Nhan Enter de tiep tuc....");
            Console.ReadKey();
            Console.Clear();

            Console.Write("Nhap vi tri muon chen vao danh sach");
            string tempIndex = Console.ReadLine();
            int index;
            while (true)
            {
                if (int.TryParse(tempIndex, out index))
                {
                    if (index < 0 || index > danhSach.Count)
                    {
                        Console.Write("Vi tri khong hop le! Nhap lai vi tri: ");
                        tempIndex = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Nhap lai vi tri: ");
                    tempIndex = Console.ReadLine();
                }
            }
            danhSach.InsertRange(index, danhSachSinhVienMoi);
            //Xoa toan bo ptu cua danh sach can them (danh sach sinh vien moi)
            danhSachSinhVienMoi.Clear();
        }

        public void XoaSvTheoCode(ref List<SinhVienTH02> danhSach)//Remove
        {
            if(danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach rong!!!");
            }
            else
            {
                Console.Write("Nhap ma sinh vien muon xoa: ");
                string sinhVienCanXoa = Console.ReadLine();
                bool flagStatus = false;
                for (int i = 0; i < danhSach.Count; i++)
                {
                    if (danhSach[i].MaSV1 == sinhVienCanXoa)
                    {
                        flagStatus = true;
                        danhSach.Remove(danhSach[i]);
                    }
                }
                if (flagStatus)
                    Console.WriteLine($"Da xoa sinh vien co ma {sinhVienCanXoa} khoi danh sach.");
                else
                    Console.WriteLine($"Khong tim thay sinh vien co ma {sinhVienCanXoa} trong danh sach.");
            }
        }
        public void XoaSvTheoViTri(ref List<SinhVienTH02> danhSach)//RemoveAt
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach rong!!!");
            }
            else
            {
                Console.Write("Nhap ma sinh vien muon xoa: ");
                string sinhVienCanXoa = Console.ReadLine();
                bool flagStatus = false;
                for (int i = 0; i < danhSach.Count; i++)
                {
                    if (danhSach[i].MaSV1 == sinhVienCanXoa)
                    {
                        flagStatus = true;
                        danhSach.RemoveAt(i);
                    }
                }
                if (flagStatus)
                    Console.WriteLine($"Da xoa sinh vien co ma {sinhVienCanXoa} khoi danh sach.");
                else
                    Console.WriteLine($"Khong tim thay sinh vien co ma {sinhVienCanXoa} trong danh sach.");
            }
        }

        public static void Add(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachMoi)
        {
            Console.WriteLine("---Menu them sinh vien---");
            Console.WriteLine("1. Them sinh vien vao cuoi danh sach.");
            Console.WriteLine("2. Them sinh vien vao vi tri chi dinh.");
            Console.WriteLine("3. Them mot danh sach vao vi tri chi dinh trong danh sach hien tai.");
            Console.WriteLine("4. Quay lai Menu chinh.");
            Console.WriteLine("5. Thoat chuong trinh.");
            string tempChoose; int choose;
            Console.Write("Chon chuc nang: ");
            tempChoose = Console.ReadLine();
            while (true)
            {
                if (int.TryParse(tempChoose, out choose))
                {
                    if (choose < 1 || choose > 5)
                    {
                        Console.Write("Khong the thuc hien! Moi chon lai chuc nang: ");
                        tempChoose = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi chon lai chuc nang: ");
                    tempChoose = Console.ReadLine();
                }
            }
            switch (choose)
            {
                case 1:
                    Console.Clear();
                    QuanLy Stu = new QuanLy("","",0,"","");
                    Stu.ThemSinhVienCuoiDanhSach(ref danhSach);
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 2:
                    Console.Clear();
                    QuanLy Stu1 = new QuanLy("", "", 0, "", "");
                    Stu1.ThemSinhVienVaoViTri(ref danhSach);
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 3:
                    Console.Clear();
                    QuanLy Stu2 = new QuanLy("", "", 0, "", "");
                    Stu2.ThemMotDanhSachVaoViTri(ref danhSach, ref danhSachMoi);
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 4:
                    Console.Clear();
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Da thoat chuong trinh!!!");
                    break;
                default:
                    break;
            }
        }

        public static void Remove(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachMoi)
        {
            Console.WriteLine("---Menu xoa sinh vien---");
            Console.WriteLine("1. Xoa mot sinh vien theo ma sinh vien.");
            Console.WriteLine("2. Xoa mot sinh vien theo vi tri chi dinh.");
            Console.WriteLine("3. Quay lai Menu chinh.");
            Console.WriteLine("4. Thoat chuong trinh.");
            string tempChoose; int choose;
            Console.Write("Chon chuc nang: ");
            tempChoose = Console.ReadLine();
            while (true)
            {
                if (int.TryParse(tempChoose, out choose))
                {
                    if (choose < 1 || choose > 4)
                    {
                        Console.Write("Khong the thuc hien! Moi chon lai chuc nang: ");
                        tempChoose = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi chon lai chuc nang: ");
                    tempChoose = Console.ReadLine();
                }
            }
            switch (choose)
            {
                case 1:
                    Console.Clear();
                    QuanLy Stu = new QuanLy("", "", 0, "", "");
                    Stu.XoaSvTheoCode(ref danhSach);
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 2:
                    Console.Clear();
                    QuanLy Stu1 = new QuanLy("", "", 0, "", "");
                    Stu1.XoaSvTheoViTri(ref danhSach);
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 3:
                    Console.Clear();
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Da thoat chuong trinh!!!");
                    break;
                default:
                    break;
            }
        }

        public static void LocSinhVien(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachMoi)
        {
            if(danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach rong!!!");
            }
            else
            {
                QuanLy Stu = new QuanLy("", "", 0, "", "");
                Stu.Loc(ref danhSach, ref danhSachMoi);
            }
        }

        public static void Menu(ref List<SinhVienTH02> danhSach, ref List<SinhVienTH02> danhSachMoi)
        {
            Console.WriteLine("---Menu Quan Ly Sinh Vien TH02---");
            Console.WriteLine("1. Them sinh vien.");
            Console.WriteLine("2. Xuat danh sach sinh vien.");
            Console.WriteLine("3. Xoa sinh vien.");
            Console.WriteLine("4. Loc sinh vien.");
            Console.WriteLine("5. Sap xep danh sach sinh vien.");
            Console.WriteLine("6. Thoat chuong trinh.");
            Console.WriteLine();
            string tempChoose; int choose;
            Console.Write("Chon chuc nang: ");
            tempChoose = Console.ReadLine();
            while (true)
            {
                if(int.TryParse(tempChoose, out choose))
                {
                    if (choose < 1 || choose > 6)
                    {
                        Console.Write("Khong the thuc hien! Moi chon lai chuc nang: ");
                        tempChoose = Console.ReadLine();
                    }
                    else break;
                }
                else
                {
                    Console.Write("Nhap sai! Moi chon lai chuc nang: ");
                    tempChoose = Console.ReadLine();
                }
            }
            switch (choose)
            {
                case 1:
                    Console.Clear();
                    QuanLy.Add(ref danhSach, ref danhSachMoi);
                    break;
                case 2:
                    Console.Clear();
                    QuanLy.XuatDanhSach(danhSach);
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 3:
                    Console.Clear();
                    QuanLy.Remove(ref danhSach, ref danhSachMoi);
                    break;
                case 4:
                    Console.Clear();
                    QuanLy.LocSinhVien(ref danhSach, ref danhSachMoi);
                    //QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 5:
                    Console.Clear();
                    if(danhSach.Count == 0)
                    {
                        Console.WriteLine("Danh sach rong!!!");
                    }
                    else
                    {
                        QuanLy Stu = new QuanLy("", "", 0, "", "");
                        Stu.SortIncrease(ref danhSach);
                        Console.WriteLine("Da sap xep xong danh sach tang dan theo ten...");
                    }
                    QuanLy.Menu(ref danhSach, ref danhSachMoi);
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Da thoat chuong trinh.");
                    break;
                default:
                    break;
            }
        }

    }

    
}
