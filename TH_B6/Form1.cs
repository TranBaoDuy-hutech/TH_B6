using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TH_B6.MODEL;

namespace TH_B6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var context = new SachModel())
            {
                // Tải dữ liệu sách với thông tin loại sách
                var listSachs = context.Sach.Include(s => s.LoaiSach).ToList();
                var listLoaiSachs = context.LoaiSach.ToList();
                Fillloai(listLoaiSachs);
                BindGrid(listSachs);
            }
        }
        private void Fillloai(List<LoaiSach> listloai)
        {
            listloai.Insert(0, new LoaiSach());
            this.cmbtl.DataSource = listloai;
            this.cmbtl.DisplayMember = "TenLoai";
            this.cmbtl.ValueMember = "MaLoai";
            if (listloai.Count > 0)
            {
                cmbtl.SelectedIndex = 2; // Chọn thể loại đầu tiên làm mặc định
            }
        }
        private void BindGrid(List<Sach> listSach)
        {
            dgvSach.Rows.Clear();
            foreach (var item in listSach)
            {
                int index = dgvSach.Rows.Add();
                dgvSach.Rows[index].Cells[0].Value = item.MaSach;
                dgvSach.Rows[index].Cells[1].Value = item.TenSach;
                dgvSach.Rows[index].Cells[2].Value = item.NamXB;
                if (item.MaLoai != null)
                    dgvSach.Rows[index].Cells[3].Value =
                    item.LoaiSach.TenLoai;


            }
        }
        private void LoadData()
        {
            using (var context = new SachModel())
            {
                var listSachs = context.Sach.Include(s => s.LoaiSach).ToList();
                var listLoaiSachs = context.LoaiSach.ToList();
                Fillloai(listLoaiSachs);
                BindGrid(listSachs);
            }
        }


        private void btnthem_Click(object sender, EventArgs e)
        {
            var sach = new Sach
            {
                MaSach = txtma.Text,
                TenSach = txtten.Text,
                NamXB = int.Parse(txtnam.Text), // Chuyển đổi từ chuỗi sang số
                MaLoai = (int)cmbtl.SelectedValue // Lấy giá trị từ ComboBox
            };

            using (var context = new SachModel())
            {
                context.Sach.Add(sach);
                context.SaveChanges();
            }

            LoadData(); // Tải lại dữ liệu
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            var maSach = txtma.Text; // Lấy mã sách từ ô nhập liệu

            using (var context = new SachModel())
            {
                var sach = context.Sach.Find(maSach);
                if (sach != null)
                {
                    sach.TenSach = txtten.Text;
                    sach.NamXB = int.Parse(txtnam.Text);
                    sach.MaLoai = (int)cmbtl.SelectedValue;

                    context.SaveChanges();
                }
            }

            LoadData(); // Tải lại dữ liệu
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            var maSach = txtma.Text; // Lấy mã sách từ ô nhập liệu

            using (var context = new SachModel())
            {
                var sach = context.Sach.Find(maSach);
                if (sach != null)
                {
                    context.Sach.Remove(sach);
                    context.SaveChanges();
                }
            }

            LoadData(); // Tải lại dữ liệu
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSach.Rows[e.RowIndex];
                txtma.Text = row.Cells[0].Value.ToString();
                txtten.Text = row.Cells[1].Value.ToString();
                txtnam.Text = row.Cells[2].Value.ToString();
                // Lựa chọn thể loại từ ComboBox
                cmbtl.SelectedValue = row.Cells[3].Value;
            }
        }

        private void tHONGKETHEONAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Form2 F = new Form2();
            this.Hide();
            F.ShowDialog();
            this.Show();
           
        }

        private void btnTIM_Click(object sender, EventArgs e)
        {
            string maSach = txtTim.Text.Trim(); // Lấy mã sách từ TextBox

            using (var context = new SachModel())
            {
                // Tìm sách theo mã
                var sach = context.Sach.Include(s => s.LoaiSach)
                    .FirstOrDefault(s => s.MaSach == maSach);

                // Hiển thị kết quả
                dgvSach.Rows.Clear(); // Xóa dữ liệu cũ
                if (sach != null)
                {
                    int index = dgvSach.Rows.Add();
                    dgvSach.Rows[index].Cells[0].Value = sach.MaSach;
                    dgvSach.Rows[index].Cells[1].Value = sach.TenSach;
                    dgvSach.Rows[index].Cells[2].Value = sach.NamXB;
                    dgvSach.Rows[index].Cells[3].Value = sach.LoaiSach?.TenLoai; // Tên loại sách
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sách với mã đã nhập.");
                    LoadData(); // Tải lại dữ liệu nếu không tìm thấy
                }
            }
        }
    }
}
