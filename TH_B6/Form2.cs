using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TH_B6.MODEL;

namespace TH_B6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        /*   private void ThongKeSachTheoNam()
           {
               using (var context = new SachModel())
               {
                   var thongKe = context.Sach
                       .GroupBy(s => new { s.NamXB, s.MaLoai }) // Nhóm theo năm xuất bản và mã loại
                       .Select(g => new
                       {
                           NamXB = g.Key.NamXB,
                           MaLoai = g.Key.MaLoai,
                           SoLuong = g.Count(),
                           Saches = g.Select(s => s.TenSach).ToList(), // Lấy danh sách tên sách
                           TenLoai = context.LoaiSach.FirstOrDefault(l => l.MaLoai == g.Key.MaLoai).TenLoai // Lấy tên loại sách
                       })
                       .ToList();

                   // Hiển thị kết quả thống kê vào DataGridView
                   dgvThangKe.Rows.Clear();

                   // Tạo một danh sách tạm để nhóm lại các tên sách theo năm và loại
                   var groupedResults = thongKe.GroupBy(x => new { x.NamXB, x.TenLoai })
                       .Select(g => new
                       {
                           NamXB = g.Key.NamXB,
                           TenLoai = g.Key.TenLoai,
                           SoLuong = g.Sum(x => x.SoLuong),
                           Saches = string.Join(", ", g.SelectMany(x => x.Saches)) // Gộp tên sách thành chuỗi
                       })
                       .ToList();

                   foreach (var item in groupedResults)
                   {
                       int index = dgvThangKe.Rows.Add();
                       dgvThangKe.Rows[index].Cells[0].Value = item.NamXB;
                       dgvThangKe.Rows[index].Cells[1].Value = item.SoLuong;
                       dgvThangKe.Rows[index].Cells[2].Value = item.Saches; // Tên sách
                       dgvThangKe.Rows[index].Cells[3].Value = item.TenLoai; // Tên loại sách
                   }
               }
           }

   */
        private void ThongKeSachTheoNam()
        {
            using (var context = new SachModel())
            {
                var thongKe = context.Sach
                    .Select(s => new
                    {
                        s.NamXB,
                        s.MaSach,
                        s.TenSach,
                        TenLoai = context.LoaiSach.FirstOrDefault(l => l.MaLoai == s.MaLoai).TenLoai // Lấy tên loại sách
                    })
                    .ToList();

                // Hiển thị kết quả thống kê vào DataGridView
                dgvThangKe.Rows.Clear();

                foreach (var item in thongKe)
                {
                    int index = dgvThangKe.Rows.Add();
                    dgvThangKe.Rows[index].Cells[0].Value = item.NamXB;        // Năm xuất bản
                    dgvThangKe.Rows[index].Cells[1].Value = item.MaSach;      // Mã sách
                    dgvThangKe.Rows[index].Cells[2].Value = item.TenSach;     // Tên sách
                    dgvThangKe.Rows[index].Cells[3].Value = item.TenLoai;     // Tên loại sách
                }
            }
        }


        private void btntk_Click(object sender, EventArgs e)
        {
            ThongKeSachTheoNam();
        }
       
        private void Form2_Load(object sender, EventArgs e)
        {
         //   LoadData(); // Tải dữ liệu sách
            ThongKeSachTheoNam(); // Thống kê sách theo năm
        }
    }
}
