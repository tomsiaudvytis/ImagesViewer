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
    using ImageController.Controllers;

    public partial class ImgViewerForm : Form
    {
        private IEnumerable<ImageModel> _picture = new List<ImageModel>();

        private ImageController controller = new ImageController(new CustomImageConverter(), new MsSQLImageRepository());

        public ImgViewerForm()
        {
            InitializeComponent();

            _picture = controller.GetAllImages();
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
                    _picture = controller.SearchImages(txtBoxSearchName.Text);
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

                byte[] imgToUploadInBytes = controller.ImgToBytes(imgPath);

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
                    controller.UploadImage(imageToUpload);
                });

                var awaiter = task.GetAwaiter();

                awaiter.OnCompleted(() =>
                {
                    MessageBox.Show(imageToUpload.PictureName + " Uploaded");
                    InitializeComponent();
                    _picture = controller.GetAllImages();
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
                    IEnumerable<ImageModel> _picture = controller.GetImage(pictureID);
                    string[] currentImgAsStringArr = _picture.First().PictureContent.Split(' ');
                    byte[] currentImbAsBytesArr = currentImgAsStringArr.Select(byte.Parse).ToArray();
                    selectedImage = controller.BytesToImage(currentImbAsBytesArr);
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
