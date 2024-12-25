using KiemTra.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KiemTra
{
    public partial class Form1 : Form
    {
        private IEnumerable<object> listStudent;
        private object context;
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            Model1 context = new Model1();
            List<Lopp> dsLop = context.Lopps.ToList();
            DoDuLieuVaoCombobox(dsLop);

            List<SinhVienn> dsSV = context.SinhVienns.ToList();
            DoDuLieuVaoDatagridView(dsSV);
        }

        private void DoDuLieuVaoDatagridView(List<SinhVienn>dsSV)
        {
            dataGridView1.DataSource = dsSV;
            dataGridView1.Rows.Clear();
            foreach (var item in dsSV)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.MaSV;
               dataGridView1.Rows[index].Cells[1].Value = item.HoTenSV;

                dataGridView1.Rows[index].Cells[2].Value = item.NgaySinh;
               dataGridView1.Rows[index].Cells[3].Value = item.MaLop;
            }
           
        }
        private void DoDuLieuVaoCombobox(List<Lopp> dsLop)
        {
            cmbLopHoc.DataSource = dsLop;
            cmbLopHoc.DisplayMember = "TenLop";
           cmbLopHoc.ValueMember = "MaLop";
        }

        private bool kiemtrarangbuoc()
        {
            if (txtHoTen.Text == "" || txtMSV.Text == "")
            {
                MessageBox.Show("Cần Nhập Đầy Đủ Dữ Liệu");
                return false;
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (kiemtrarangbuoc())
            {

                Model1 context = new Model1();

               SinhVienn timSV = context.SinhVienns.Where(sv => sv.MaSV == txtMSV.Text).FirstOrDefault();
                if (timSV == null)
                {
                    SinhVienn sinhvien = new SinhVienn();
                    sinhvien.MaSV = txtMSV.Text;
                    sinhvien.HoTenSV = txtHoTen.Text;
                    sinhvien.NgaySinh = dtmNgaySinh.Value;
                    sinhvien.MaLop = (cmbLopHoc.SelectedItem as Lopp).MaLop;

                    context.SinhVienns.Add(sinhvien);
                    context.SaveChanges();
                    MessageBox.Show("Thêm sinh Viên Thành Công");
                    DoDuLieuVaoDatagridView(context.SinhVienns.ToList());

                }
                else
                    MessageBox.Show("Sinh Viên đã Tồn Tại");


            }
            cmbLopHoc.Text = "";
            }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kiemtrarangbuoc())
            {

                Model1 context = new Model1();
                SinhVienn timSV = context.SinhVienns.Where(sv => sv.MaSV == txtMSV.Text).SingleOrDefault();
                if (timSV != null)
                {

                    context.SinhVienns.Remove(timSV);
                    context.SaveChanges();
                    MessageBox.Show("Xoá sinh Viên Thành Công");
                    DoDuLieuVaoDatagridView(context.SinhVienns.ToList());

                }
                else
                    MessageBox.Show("Sinh Viên đã Tồn Tại");

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kiemtrarangbuoc())
            {

                Model1 context = new Model1();
                SinhVienn timSV = context.SinhVienns.Where(sv => sv.MaSV == txtMSV.Text).FirstOrDefault();
                if (timSV != null)
                {

                    timSV.HoTenSV = txtHoTen.Text;
                    timSV.NgaySinh = dtmNgaySinh.Value;
                    timSV.MaLop = (cmbLopHoc.SelectedItem as Lopp).MaLop;
                    context.SaveChanges();
                    MessageBox.Show("Sửa sinh Viên Thành Công");
                    DoDuLieuVaoDatagridView(context.SinhVienns.ToList());

                }
                else
                    MessageBox.Show("Sinh Viên đã Tồn Tại");

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
           
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(" Bạn có chacws chắn muốn thoát !", "xác Nhận", MessageBoxButtons.YesNo , MessageBoxIcon.Error);
            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
