using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImagesViewerUI
{
    public partial class Form1 : Form
    {
        private IEnumerable<Picture> picture = new List<Picture>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Image img = new Image();
            picture = img.GetImages(txtBoxSearchName.Text);
        }
    }
}
