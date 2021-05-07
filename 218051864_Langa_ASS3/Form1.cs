using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;

namespace _218051864_Langa_ASS3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<string> lstReleaseYears;
        private void Form1_Load(object sender, EventArgs e)
        {
            clsValidateRegex populator = new clsValidateRegex();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ContentType = txtType.Text;
            string ContentYear = txtYear.Text;
            clsValidateRegex obj = new clsValidateRegex();
            listBox1.Items.Clear();
           var ListFromCriteria =  obj.GetContentFromNewCriteria(ContentType,ContentYear);

            foreach (var item in ListFromCriteria.exported)
            {
                listBox1.Items.Add(item);
            }
            obj.generateDirectorReports();
            obj.generateShowReports();

            dirCount.Text = obj.DirCount.ToString();
            tvRatings.Text = obj.ShowRatingCount.ToString();
            MessageBox.Show(ListFromCriteria.msg);
            

        
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void rectangleDiagram_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, rectangleDiagram.ClientRectangle,
                       Color.Black, 1, ButtonBorderStyle.Dashed, // left
                       Color.Black, 1, ButtonBorderStyle.Dashed, // top
                       Color.Black, 1, ButtonBorderStyle.Dashed, // right
                       Color.Black, 1, ButtonBorderStyle.Dashed);// bottom
        }
    }
}
