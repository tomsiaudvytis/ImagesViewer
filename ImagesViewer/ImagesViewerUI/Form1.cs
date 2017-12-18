using DataAccess;
using DataAccess.Models;
using ImagesConverter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImagesViewerUI
{
    public partial class Form1 : Form
    {
        private IEnumerable<Picture> picture = new List<Picture>();
        DataAccess.Image Image = new DataAccess.Image();
        CustomImageConverter converter = new CustomImageConverter();

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            picture = Image.GetImages(txtBoxSearchName.Text);
            dataGridPictures.DataSource = picture;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Files|*.jpg;*.jpeg;*.png;",
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;

                byte[] imgBytes = converter.ImgToBytes(path);

                Picture uploadPicture = new Picture()
                {
                    PictureContent = string.Join(" ", imgBytes),
                    PictureID = Guid.NewGuid().ToString(),
                    PictureName = dialog.SafeFileName
                };

                Image.UploadPicture(uploadPicture);
                MessageBox.Show(uploadPicture.PictureName + " Uploaded");

            }
        }
    }
}
