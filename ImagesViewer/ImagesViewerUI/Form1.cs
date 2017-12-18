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
        private IEnumerable<ImageModel> _picture = new List<ImageModel>();
        private Image _image = new Image();
        private CustomImageConverter _converter = new CustomImageConverter();

        public ImgViewerForm()
        {
            InitializeComponent();

            _picture = _image.GetAllImages();
            dataGridPictures.DataSource = _picture;
            dataGridPictures.Columns[0].Visible = false;
            dataGridPictures.Columns[4].Visible = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxSearchName.Text))
            {
                MessageBox.Show("Enter picture name!");
            }

            _picture = _image.SearchImages(txtBoxSearchName.Text);
            dataGridPictures.DataSource = _picture;
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Files|*.jpg;*.jpeg;*.png;",
                Multiselect = false,
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string imgPath = dialog.FileName;

                byte[] imgToUploadInBytes = _converter.ImgToBytes(imgPath);

                ImageModel imageToUpload = new ImageModel()
                {
                    PictureContent = string.Join(" ", imgToUploadInBytes),
                    PictureID = Guid.NewGuid().ToString(),
                    PictureName = dialog.SafeFileName,
                    Size = imgToUploadInBytes.Length
                };

                _image.UploadImage(imageToUpload);
                MessageBox.Show(imageToUpload.PictureName + " Uploaded");
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
                    _picture = _image.GetImage(pictureID);

                    string[] currentImgAsStringArr = _picture.First().PictureContent.Split(' ');
                    byte[] currentImbAsBytesArr = currentImgAsStringArr.Select(byte.Parse).ToArray();

                    System.Drawing.Image selectedImage = _converter.BytesToImage(currentImbAsBytesArr);

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
