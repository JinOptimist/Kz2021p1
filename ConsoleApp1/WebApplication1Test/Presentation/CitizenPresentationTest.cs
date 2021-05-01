using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Presentation;
using WebApplication1.Services;

namespace WebApplication1Test.Presentation
{
    public class CitizenPresentationTest
    {
        private Mock<ICitizenRepository> citizenRepositoryMock;
        private Mock<IUserService> userServiceMock;
        private Mock<IMapper> mapperMock;
        private CitizenPresentation citizenPresentation; 

        [SetUp]
        public void Setup()
        {
            citizenRepositoryMock = new Mock<ICitizenRepository>();
            userServiceMock = new Mock<IUserService>();
            mapperMock = new Mock<IMapper>();
            citizenPresentation = new CitizenPresentation(
                citizenRepositoryMock.Object,
                userServiceMock.Object,
                mapperMock.Object);
        }

        [Test]
        [TestCase("Smile")]
        [TestCase("Duck")]
        [TestCase("NiceGirl")]
        public void Remove_ExistUser(string name)
        {
            //Подготовка
            var smileUser = new Citizen();
            citizenRepositoryMock
                .Setup(x => x.GetByName(name))
                .Returns(smileUser);

            //Действие
            var result = citizenPresentation.Remove(name);

            //Проверки
            citizenRepositoryMock
                .Verify(x => x.Remove(smileUser), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void Remove_UnexistUser()
        {
            //Подготовка

            //Действие
            var result = citizenPresentation.Remove("fun");

            //Проверки
            citizenRepositoryMock
                .Verify(x => x.Remove(It.IsAny<Citizen>()), Times.Never);
            Assert.IsFalse(result);
        }
    
        [Test]
        public void FullProfile()
        {
            var user = new Citizen();
            userServiceMock.Setup(x => x.GetUser()).Returns(user);

            var viewModel = new FullProfileViewModel();
            mapperMock.Setup(x => x.Map<FullProfileViewModel>(user))
                .Returns(viewModel);

            var result = citizenPresentation.FullProfile();

            userServiceMock.Verify(x => x.GetUser(), Times.Once);
            mapperMock.Verify(x => x.Map<FullProfileViewModel>(user), Times.Once);

            Assert.AreEqual(viewModel, result);
        }
    }
}
