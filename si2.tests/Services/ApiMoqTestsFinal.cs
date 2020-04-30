using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using si2.bll.Helpers.Credits;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using si2.bll.Dtos.Results.University;
using si2.bll.Helpers;
using si2.bll.Services;
using si2.dal.Entities;
using si2.common;
using si2.dal.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;
using si2.bll.Dtos.Requests.University;
using System.Collections.ObjectModel;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Helpers.PagedList;

namespace si2.tests.Services
{
    [TestFixture]
    [Category("TestMoqFinal")]
    class ApiMoqTestsFinal
    {
        private /*readonly*/ Mock<IUnitOfWork> _mockUnitOfWork;
        private /*readonly*/ Mock<ILogger<UniversityService>> _mockLogger;
        private /*readonly*/ IMapper _mapper;
        private Mock<UniversityResourceParameters> _mockUniversityResourceParameters;

        private UniversityDto mockUniversityDto = new UniversityDto()
        {
            Id = new Guid("1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26"),
            Name = "USJ-جامعة القدّيس يوسف"
            //RowVersion = Convert.FromBase64String("AAAAAAAAB94=")
        };

        private University mockUniversity = new University()
        {
            Id = new Guid("1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26"),
            Name = "USJ-جامعة القدّيس يوسف"
            //RowVersion = Convert.FromBase64String("AAAAAAAAB94=")
        };

        private CreateUniversityDto mockUniversityCreateDto = new CreateUniversityDto()
        {
            Name = "LU"
            //RowVersion = Convert.FromBase64String("AAAAAAAAB94=")
        };

        private PagedList<UniversityDto> mockAllUniversityDto = new PagedList<UniversityDto>
        {
            new UniversityDto
        {
            Id = new Guid("1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26"),
                Name = "USJ-جامعة القدّيس يوسف"
            },
            new UniversityDto()
        {
            Id = new Guid("2697822C-9D46-4E16-5E60-08D7D5A16E26"),
                Name = "AUB"
            },
            new UniversityDto()
        {
            Id = new Guid("0C2F986C-C4B7-4D69-5E61-08D7D5A16E26"),
                Name = "LAU"
            }
        };

        private ICollection<University> mockAllUniversityCollection = new Collection<University>
        {
            new University
        {
            Id = new Guid("1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26"),
                Name = "USJ-جامعة القدّيس يوسف"
            },
            new University()
        {
            Id = new Guid("2697822C-9D46-4E16-5E60-08D7D5A16E26"),
                Name = "AUB"
            },
            new University()
        {
            Id = new Guid("0C2F986C-C4B7-4D69-5E61-08D7D5A16E26"),
                Name = "LAU"
            }
        };

        public IUniversityService _universityService;

        [SetUp]
        public void UniversityTest()
        {
            //executes or runs before each test method
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<UniversityService>>();
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }).CreateMapper();

            _universityService = new UniversityService(_mockUnitOfWork.Object, _mapper, _mockLogger.Object);

            _mockUniversityResourceParameters = new Mock<UniversityResourceParameters>();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //executes once
        }

        [Test]
        public void GetUniversityByIdAsync_WhenMatching()
        {
            // Arrange
            _mockUnitOfWork.Setup(_mockUnitOfWork => _mockUnitOfWork.Universities.GetAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockUniversity));

            // Act
            var expected = _universityService.GetUniversityByIdAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>()).Result;


            // Assert
            //Assert.AreEqual(expected, mockUniversityDto);
            Assert.That(expected, Is.EqualTo(mockUniversityDto));
            //_mockUnitOfWork.Verify(x => x.Universities.GetAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>()), Times.AtLeast(2));//fail
            //_mockUnitOfWork.Verify(x => x.Universities.GetAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>()), Times.Once);//passes
            //_mockUnitOfWork.Setup((_mockUnitOfWork => _mockUnitOfWork.Universities.GetAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>()))).Throws<InvalidOperationException>(); // passes but how
        }

        [Test]
        public void CreateUniversityAsync()
        {
            //Arrange
            _mockUnitOfWork.Setup(_mockUnitOfWork => _mockUnitOfWork.Universities.AddAsync(mockUniversity,It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockUniversity));

            //Act
            var expected = _universityService.CreateUniversityAsync(mockUniversityCreateDto, It.IsAny<CancellationToken>()).Result;

            var newId = expected.Id;

            var newMockUniversityDto = new UniversityDto()
            {
                Id = newId,
                Name = mockUniversityCreateDto.Name
            };

            var sameAsExpected = expected;

            //Assert
            //Assert.AreEqual(expected, newMockUniversityDto);//classic model of assertions(older)
            Assert.That(expected, Is.EqualTo(newMockUniversityDto));//constraint model of assertions(newer)
            //Assert.That(expected, Is.SameAs(newMockUniversityDto), "fails because it only compares references and not values");//fails because it only compares references and not values
            //Assert.That(expected, Is.SameAs(sameAsExpected));//passes
            //Assert.That(expected, Has.Exactly(1).Items);//fails because of the type of expected
            //Assert.That(expected,Is.Not.Null);
            //_mockUnitOfWork.SetupAllProperties();
          
        }

        [Test]
        [Ignore("Facing this error: The source IQueryable doesn't implement IAsyncEnumerable<si2.dal.Entities.University>. Only sources that implement IAsyncEnumerable can be used for Entity Framework asynchronous operations line 170")]
        public void Get_All_Universities()
        {
            // Arrange
            _mockUnitOfWork.Setup(_mockUnitOfWork => _mockUnitOfWork.Universities.GetAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockAllUniversityCollection));

            // Act
            var expected = _universityService.GetUniversitiesAsync(_mockUniversityResourceParameters.Object, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.That(expected, Is.EqualTo(mockAllUniversityDto));
        }

        [OneTimeTearDown]
        public void OnTimeTearDown()
        {
            //executes once
            //disposing of shared expensive setup performed in OneTimeSetup
        }
        
        [TearDown]
        public void TearDown()
        {
            //sut.Dispose();
            //executes or runs after each test method
        }
    }
}