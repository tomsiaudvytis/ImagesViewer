namespace ImageControllerTests
{
    using NUnit.Framework;
    using FakeItEasy;
    using ImageController.Controllers;
    using ImagesConverter;
    using DataAccess.Repositories;
    using DataAccess.Models;
    using log4net;
    using DataAccess;
    using System;

    [TestFixture]
    public class ImageControllerTests
    {
        [Test]
        public void UploadImage_ShouldCallUploadImageFromImageRepository()
        {
            IConvert converter = A.Fake<IConvert>();
            IImageRepository repo = A.Fake<IImageRepository>();
            ILog logger = A.Fake<ILog>();

            ImageController controller = new ImageController(converter, repo, logger);

            controller.UploadImage("some path");

            A.CallTo(() => repo.UploadImage(A<ImageModel>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Delete_ShouldCallDeleteImageFromImageRepository()
        {
            IConvert converter = A.Fake<IConvert>();
            IImageRepository repo = A.Fake<IImageRepository>();
            ILog logger = A.Fake<ILog>();

            ImageController controller = new ImageController(converter, repo, logger);

            controller.DeleteImage("FakeId");

            A.CallTo(() => repo.DeleteImage("FakeId")).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
