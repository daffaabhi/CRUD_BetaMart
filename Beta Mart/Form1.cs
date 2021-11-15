using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beta_Mart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.myDbTableAdapter.Fill(this.appData.MyDb);
                    myDbBindingSource.DataSource = this.appData.MyDb;
                    dataGridView.DataSource = myDbBindingSource;
                }
                else
                {
                    var query = from o in this.appData.MyDb
                                where o.Kode.Contains(txtSearch.Text) || o.NamaBarang.Contains(txtSearch.Text) || o.Jumlah.Contains(txtSearch.Text) || o.Harga.Contains(txtSearch.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Apakah kamu yakin ingin menghapus?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    myDbBindingSource.RemoveCurrent();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                txtKode.Focus();
                this.appData.MyDb.AddMyDbRow(this.appData.MyDb.NewMyDbRow());
                myDbBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myDbBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtKode.Focus();
            txtNama.Focus();
            txtJumlah.Focus();
            txtHarga.Focus();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                myDbBindingSource.EndEdit();
                myDbTableAdapter.Update(this.appData.MyDb);
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myDbBindingSource.ResetBindings(false);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKode.Clear();
            txtNama.Clear();
            txtJumlah.Clear();
            txtHarga.Clear();
            pictureBox.Image = null;
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                myDbBindingSource.RemoveCurrent();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.MyDb' table. You can move, or remove it, as needed.
            this.myDbTableAdapter.Fill(this.appData.MyDb);
            myDbBindingSource.DataSource = this.appData.MyDb;
        }
    }
}
