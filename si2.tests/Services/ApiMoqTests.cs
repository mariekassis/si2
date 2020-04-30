using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using si2.api.Controllers;
using si2.bll.Dtos.Requests.University;
using si2.bll.Dtos.Results.University;
using si2.bll.Helpers;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.dal.Context;
using si2.dal.Entities;
using si2.dal.Interfaces;
using si2.dal.Repositories;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.tests.Services
{
    [TestFixture]
    public class ApiMoqTests
    {

        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<UniversitiesController> _logger;
        private /*readonly*/ IUniversityService _universityService;
        private /*readonly*/ Mock<ILogger<UniversityService>> _mockLogger;
        private /*readonly*/ IMapper _mapper;
        Mock<IUnitOfWork> _mockUnitOfWork;
        PagedList<UniversityDto> expectedUniversities;
        Mock<IUniversityService> mockUniversityService;
        UniversitiesController universitiesController;
        UniversityResourceParameters pagedResourceParameters;
        CancellationTokenSource source;
        CancellationToken token;
        UniversityService universityService = null;
        IUnitOfWork uow = null;
        IMapper mapper;
        ILogger<UniversityService> logger;
        Si2DbContext dbContext;
        IServiceProvider serviceProvider = null;
        IUniversityService iuniversityService = null;
        IUniversityRepository Universities;

        private Collection<UniversityDto> mockAllUniversityDto = new Collection<UniversityDto>
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

        [SetUp]
        public void InitializeTestData()
        {
            //Setup test data
            expectedUniversities = GetExpectedUniversities();
            //Arrange
            mockUniversityService = new Mock<IUniversityService>();
            universitiesController = new UniversitiesController(_linkGenerator, _logger, _universityService);
            //var serviceProviderGet = serviceProvider.GetRequiredService<IUniversityService>();
            uow = new UnitOfWork(dbContext, serviceProvider);
            //universityService = new UniversityService(uow, mapper, logger);
            //iuniversityService = new UniversityService(uow, mapper, logger);
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<UniversityService>>();
            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }).CreateMapper();

            _universityService = new UniversityService(_mockUnitOfWork.Object, _mapper, _mockLogger.Object);

            //Setup
            source = new CancellationTokenSource();
            token = source.Token;
            pagedResourceParameters = new UniversityResourceParameters();
            //var expectedUniversitiesTask = expectedUniversitiesAsync();
            mockUniversityService.Setup(m => m.GetUniversitiesAsync(pagedResourceParameters,token)).ReturnsAsync(expectedUniversities);

            //mockUnitOfWork.Setup(mo => mo.Universities).Returns(Universities);
            //mockUnitOfWork.Setup(_mockUnitOfWork => _mockUnitOfWork.Universities.GetAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockUniversity));
            

            

            /*mockUniversityService.Setup(m => m.CreateUniversityAsync(It.IsAny<CreateUniversityDto>(),token)).Returns(
                (UniversityDto target) =>
                {
                    expectedUniversities.Add(target);

                    return expectedUniversities;
                });*/

            /*mockProductRepository.Setup(m => m.UpdateProduct(It.IsAny<ProductJSON>())).Returns(
                (ProductJSON target) =>
                {
                    var product = expectedProducts.Where(p => p.Id == target.Id).FirstOrDefault();

                    if (product == null)
                    {
                        return false;
                    }

                    product.Name = target.Name;
                    product.Category = target.Category;
                    product.Price = target.Price;

                    return true;
                });

            mockProductRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns(
                (int productId) =>
                {
                    var product = expectedProducts.Where(p => p.Id == productId).FirstOrDefault();

                    if (product == null)
                    {
                        return false;
                    }

                    expectedProducts.Remove(product);

                    return true;
                });*/
        }

        //[Test]
        public void Get_All_Universities()
        {
            //Act
            //var actualProducts = mockProductRepository.Object.GetProducts();
            //var actualProducts = productController.GetProducts();
            //var actualUniversities = universitiesController.GetUniversities(pagedResourceParameters, token);
            //var actualUniversities = await iuniversityService.GetUniversitiesAsync(pagedResourceParameters, token);

            //var expectedUniversitiesTask = await expectedUniversitiesAsync();

            //Assert
            //Assert.AreSame(expectedUniversitiesTask, actualUniversities);

            // Arrange
            //_mockUnitOfWork.Setup(_mockUnitOfWork => _mockUnitOfWork.Universities.GetAsync(mockUniversityDto.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockUniversity));
            _mockUnitOfWork.Setup(_mockUnitOfWork => _mockUnitOfWork.Universities.GetAllAsync(It.IsAny<CancellationToken>()));

            // Act
            var expected = _universityService.GetUniversitiesAsync(pagedResourceParameters, It.IsAny<CancellationToken>()).Result;


            // Assert
            Assert.AreEqual(expected, mockAllUniversityDto);
        }
        /*[Test]
        public void Add_Product()
        {
            //int productCount = mockProductRepository.Object.GetProducts().Count;
            int productCount = productController.GetProducts().Count;

            Assert.AreEqual(2, productCount);

            //Prepare
            ProductJSON newProduct = GetNewProduct("N3", "C3", 33.55M);
            //Act
            //mockProductRepository.Object.AddProduct(newProduct);
            productController.AddProduct(newProduct);

            //productCount = mockProductRepository.Object.GetProducts().Count;
            productCount = productController.GetProducts().Count;
            //Assert
            Assert.AreEqual(3, productCount);
        }*/
        /*[TestMethod]
        public void Update_Product()
        {
            ProductJSON product = new ProductJSON()
            {
                Id = 2,
                Name = "N22",//Changed Name
                Category = "P2",
                Price = 22
            };

            //mockProductRepository.Object.UpdateProduct(product);
            productController.UpdateProduct(product);

            // Verify the change
            //Assert.AreEqual("N22", mockProductRepository.Object.GetProducts()[1].Name);
            Assert.AreEqual("N22", productController.GetProducts()[1].Name);
        }
        [TestMethod]
        public void Delete_Product()
        {
            //Assert.AreEqual(2, mockProductRepository.Object.GetProducts().Count);
            Assert.AreEqual(2, productController.GetProducts().Count);

            //mockProductRepository.Object.Delete(1);
            productController.Delete(1);

            // Verify the change
            //Assert.AreEqual(1, mockProductRepository.Object.GetProducts().Count);
            Assert.AreEqual(1, productController.GetProducts().Count);
        }

        [TestCleanup]
        public void CleanUpTestData()
        {
            expectedProducts = null;
            mockProductRepository = null;
        }*/

        public async Task<PagedList<UniversityDto>> expectedUniversitiesAsync()
        {
            return expectedUniversities;
        }

        #region HelperMethods
        private static PagedList<UniversityDto> GetExpectedUniversities()
        {
            return new PagedList<UniversityDto>()
        {
            new UniversityDto()
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
        }
        private static UniversityDto GetNewProduct(string name)
        {
            return new UniversityDto()
            {
                Name = name
            };
        }
        #endregion
    }
    
}
