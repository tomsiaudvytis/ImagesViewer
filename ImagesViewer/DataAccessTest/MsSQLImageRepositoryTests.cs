namespace DataAccessTest
{
    using DataAccess;
    using DataAccess.Executors;
    using DataAccess.Models;
    using DataAccess.Repositories;
    using FakeItEasy;
    using log4net;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class MsSQLImageRepositoryTests
    {
        [Test]
        public void GetAllImages_LogsWhenExceptionIsThrown()
        {
            ILog logger = A.Fake<ILog>();
            ISqlExecutor <ImageModel> executor = A.Fake<ISqlExecutor<ImageModel>>();
            A.CallTo(() => executor.GetAllImages(A<string>.Ignored)).Throws(new Exception("Fake error"));

            IImageRepository repo = new MsSQLImageRepository(logger, executor);
            repo.GetAllImages();

            A.CallTo(() => logger.Error("Fake error")).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
