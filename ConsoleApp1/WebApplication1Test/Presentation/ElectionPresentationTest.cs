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
    public class ElectionPresentationTest
    {
        private Mock<IElectionRepository> electionRepositoryMock;
        private Mock<ICandidateRepository> candidateRepositoryMock;
        private Mock<IBallotRepository> ballotRepositoryMock;
        private Mock<IUserService> userServiceMock;
        private Mock<IMapper> mapperMock;
        private ElectionPresentation electionPresentation; 

        [SetUp]
        public void Setup()
        {
            electionRepositoryMock = new Mock<IElectionRepository>();
            candidateRepositoryMock = new Mock<ICandidateRepository>();
            ballotRepositoryMock = new Mock<IBallotRepository>();
            userServiceMock = new Mock<IUserService>();
            mapperMock = new Mock<IMapper>();
            
            electionPresentation = new ElectionPresentation(
                candidateRepositoryMock.Object,
                electionRepositoryMock.Object,
                ballotRepositoryMock.Object,
                userServiceMock.Object,
                mapperMock.Object);
        }
        
        [Test]
        [TestCase("888")]
        [TestCase("12")]
        [TestCase("1099")]
        public void Remove_ExistElection(long id)
        {
            var testElection = new Election();
            electionRepositoryMock
                .Setup(x => x.Get(id))
                .Returns(testElection);

            var result = electionPresentation.DeleteElection(id);

            electionRepositoryMock
                .Verify(x => x.Remove(testElection), Times.Once);
            Assert.IsTrue(result);
        }
        
        [Test]
        public void Remove_UnexistElection()
        {
            var result = electionPresentation.DeleteElection(123);

            electionRepositoryMock
                .Verify(x => x.Remove(It.IsAny<Election>()), Times.Never);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(1123)]
        [TestCase(666)]
        [TestCase("1099")]
        public void Remove_ExistCandidate(long id)
        {
            var smileCandidate = new Candidate();
            candidateRepositoryMock
                .Setup(x => x.Get(id))
                .Returns(smileCandidate);

            var result = electionPresentation.DeleteCandidate(id);

            candidateRepositoryMock
                .Verify(x => x.Remove(smileCandidate), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void Remove_UnexistCandidate()
        {
            var result = electionPresentation.DeleteCandidate(123);

            candidateRepositoryMock
                .Verify(x => x.Remove(It.IsAny<Candidate>()), Times.Never);
            Assert.IsFalse(result);
        }
    }
}