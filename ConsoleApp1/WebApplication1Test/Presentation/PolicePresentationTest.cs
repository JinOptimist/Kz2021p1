using AutoMapper;
using Moq;
using NUnit.Framework;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Presentation.Police;
using WebApplication1.Services;

namespace WebApplication1Test.Presentation
{
	public class PolicePresentationTest
	{
		private Mock<ICitizenRepository> _citizenRepositoryMock;
		private Mock<IUserService> _userServiceMock;
		private Mock<IMapper> _mapperMock;
		private Mock<IPolicePresentation> _policePresentation;

		[SetUp]
		public void Setup()
		{
			_citizenRepositoryMock = new Mock<ICitizenRepository>();
			_userServiceMock = new Mock<IUserService>();
			_mapperMock = new Mock<IMapper>();
			_policePresentation = new Mock<IPolicePresentation>();
		}

		[Test]
		public void CitizenToJailIfUserExist()
		{
			var result = _citizenRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).Returns((Citizen)null);

			Assert.IsFalse(result);
		}
	}
}
