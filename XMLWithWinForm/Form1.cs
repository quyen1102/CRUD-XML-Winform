using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLWithWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataUtil data = new DataUtil();

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            s.id = txtId.Text;
            s.name = txtName.Text;
            s.age = txtAge.Text;
            s.city = txtCity.Text;

            data.AddStudent(s);
            ClearTextboxs();
            DisplayData();
        }
        private void ClearTextboxs()
        {
            txtId.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtCity.Clear();
            ActiveControl = txtId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void DisplayData()
        {
            dataGridView1.DataSource = data.GetAllStudents();
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 200;
            labelCountStudent.Text = dataGridView1.Rows.Count + "";
        }

        private void GetAStudent(object sender, DataGridViewCellEventArgs e)
        {
            Student s = (Student)dataGridView1.CurrentRow.DataBoundItem;
            txtId.Text = s.id;
            txtName.Text = s.name;
            txtAge.Text = s.age;
            txtCity.Text = s.city;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            s.id = txtId.Text;
            s.name = txtName.Text;
            s.age = txtAge.Text;
            s.city = txtCity.Text;
            bool kt = data.UpdateStudent(s);
            if (!kt)
            {
                MessageBox.Show("Cap nhat khong thanh cong " + s.id);
                return;
            }
            DisplayData();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Ban co chac muon xoa phan tu nay khong",
                "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Error
                );
            if(d == DialogResult.Yes)
            {
                bool kt = data.DeleteStudent(txtId.Text);
                if (!kt)
                {
                    MessageBox.Show("Co loi khi xoa ", "Thong bao" );
                }
                DisplayData();
                ClearTextboxs();
            }
        }

        private void btnFileById_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            List<Student> listStudent = new List<Student>();
            Student s = data.FindByID(id);
            if (s != null)
            {
                listStudent.Add(s);
                dataGridView1.DataSource = listStudent;
                labelCountStudent.Text = dataGridView1.Rows.Count + "";
                txtId.Text = s.id;
                txtName.Text = s.name;
                txtAge.Text = s.age;
                txtCity.Text = s.city;
            }
            else
            {
                MessageBox.Show("Khong sinh vien ma " + id);
            }
        }
    }
}
