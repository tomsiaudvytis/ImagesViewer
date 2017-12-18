using DataAccess;
using DataAccess.Models;
using ImagesConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImagesViewerUI
{
    public partial class ImgViewerForm : Form
    {
        private IEnumerable<Picture> picture = new List<Picture>();
        Image Image = new Image();
        CustomImageConverter converter = new CustomImageConverter();

        public ImgViewerForm()
        {
            InitializeComponent();
            picture = Image.GetAllImages();
            dataGridPictures.DataSource = picture;
            dataGridPictures.Columns[0].Visible = false;
            dataGridPictures.Columns[4].Visible = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxSearchName.Text))
            {
                MessageBox.Show("Enter picture name!");
            }

            picture = Image.SearchImages(txtBoxSearchName.Text);
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
                    PictureName = dialog.SafeFileName,
                    Size = imgBytes.Length
                };

                Image.UploadImage(uploadPicture);
                MessageBox.Show(uploadPicture.PictureName + " Uploaded");
                InitializeComponent();
            }
        }

        private void dataGridPictures_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridPictures.CurrentRow.Index != -1)
                {
                    string pictureID = dataGridPictures.CurrentRow.Cells[0].Value.ToString();
                    picture = Image.GetImage(pictureID);

                    string[] currentImgAsStringArr = picture.First().PictureContent.Split(' ');
                    byte[] currentImbAsBytesArr = currentImgAsStringArr.Select(byte.Parse).ToArray();

                    System.Drawing.Image selectedImage = converter.BytesToImage(currentImbAsBytesArr);

                    picBox.Image = selectedImage;
                    picBox.Height = selectedImage.Height;
                    picBox.Width = selectedImage.Width;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
