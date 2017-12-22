using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using ImagesConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagesViewerUI
{
    public partial class ImgViewerForm : Form
    {
        private IEnumerable<ImageModel> _picture = new List<ImageModel>();
        private IImageRepository _imageRepo = new MsSQLImageRepository();
        private CustomImageConverter _converter = new CustomImageConverter();

        public ImgViewerForm()
        {
            InitializeComponent();

            _picture = _imageRepo.GetAllImages();
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
            else
            {
                Task taskSearch = Task.Factory.StartNew(() =>
                {
                    _picture = _imageRepo.SearchImages(txtBoxSearchName.Text);
                });

                var awaiter = taskSearch.GetAwaiter();

                awaiter.OnCompleted(() =>
                {
                    dataGridPictures.DataSource = _picture;
                });
            }
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

                    //Size ruffly in Megabytes
                    Size = imgToUploadInBytes.Count() / 1000000
                };

                Task task = Task.Factory.StartNew(() =>
                {
                    _imageRepo.UploadImage(imageToUpload);
                });

                var awaiter = task.GetAwaiter();

                awaiter.OnCompleted(() =>
                {
                    MessageBox.Show(imageToUpload.PictureName + " Uploaded");
                    InitializeComponent();
                    _picture = _imageRepo.GetAllImages();
                });

            }
        }

        private void DataGridPictures_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridPictures.CurrentRow.Index != -1)
            {
                Image selectedImage = null;
                string pictureID = dataGridPictures.CurrentRow.Cells[0].Value.ToString();

                Task task = Task.Factory.StartNew(() =>
                {
                    IEnumerable<ImageModel> _picture = _imageRepo.GetImage(pictureID);
                    string[] currentImgAsStringArr = _picture.First().PictureContent.Split(' ');
                    byte[] currentImbAsBytesArr = currentImgAsStringArr.Select(byte.Parse).ToArray();
                    selectedImage = _converter.BytesToImage(currentImbAsBytesArr);
                });

                var awaiter = task.GetAwaiter();

                awaiter.OnCompleted(() =>
                {
                    if (selectedImage != null)
                    {
                        picBox.Image = selectedImage;
                        picBox.Height = selectedImage.Height;
                        picBox.Width = selectedImage.Width;
                    }

                });
            }
        }
    }
}
