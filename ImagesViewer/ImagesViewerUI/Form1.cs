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
    using DataAccess.Executors;
    using ImageController.Controllers;
    using log4net;

    public partial class ImgViewerForm : Form
    {
        private IEnumerable<ImageModel> _picture = new List<ImageModel>();
        private readonly ILog _logger;
        private readonly ISqlExecutor<ImageModel> _sqlExecutor;

        private ImageController _controller;

        public ImgViewerForm()
        {
            InitializeComponent();
            this._logger = LogManager.GetLogger(typeof(Form));
            this._sqlExecutor = new SqlExecutor();
            this._controller = new ImageController(new CustomImageConverter(this._logger), new MsSQLImageRepository(this._logger, this._sqlExecutor), this._logger);

            _picture = _controller.GetAllImages();
            if (_picture != null)
            {
                dataGridPictures.DataSource = _picture;
                dataGridPictures.Columns[0].Visible = false;
                dataGridPictures.Columns[4].Visible = false;
            }
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
                    _picture = _controller.SearchImages(txtBoxSearchName.Text);
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
                string imgPath = dialog.SafeFileName;
                Task uploadTask = Task.Factory.StartNew(() =>
                {
                    this._controller.UploadImage(imgPath);
                });

                var awaiter = uploadTask.GetAwaiter();

                awaiter.OnCompleted(() =>
                {
                    MessageBox.Show("Uploaded");
                    InitializeComponent();
                    try
                    {
                        _picture = _controller.GetAllImages();

                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        private void DataGridPictures_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridPictures!= null && dataGridPictures.CurrentRow != null && dataGridPictures.CurrentRow.Index != -1)
            {
                Image selectedImage = null;
                string pictureID = dataGridPictures.CurrentRow.Cells[0].Value.ToString();

                Task task = Task.Factory.StartNew(() =>
                {
                    selectedImage = _controller.GetImage(pictureID);
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
