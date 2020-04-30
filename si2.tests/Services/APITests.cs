using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using si2.api.Controllers;
using si2.bll.Dtos.Results.University;
using si2.bll.Helpers;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.dal.Context;
using si2.dal.Entities;
using si2.dal.Repositories;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.tests.Services
{   
    [TestFixture]
    class APITests
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<UniversitiesController> _logger;
        private readonly IUniversityService _universityService;
        //private readonly IServiceProvider _serviceProvider;
        private ILoggerFactory loggerFactory;


        UniversitiesController obj;
        Guid id;
        UniversityRepository repository;
        Si2DbContext dbContext;
        CancellationTokenSource source;
        CancellationToken token;
        UniversityResourceParameters pagedResourceParameters;
        ActionContext context;
        UniversityDto universityDto = null;
        IUniversityService universityService = null;
        IUnitOfWork uow = null;
        IMapper _mapper;
        IServiceProvider _serviceProvider;
        private /*readonly*/ ILogger<UniversityService> logger;
        private UniversityDto universityDtoActual = new UniversityDto()
        {
            Id = new Guid("1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26"),
            Name = "USJ-جامعة القدّيس يوسف"
            //RowVersion = Convert.FromBase64String("AAAAAAAAB94=")
        };



        [SetUp]
        public void setUp()
        {
            obj = new UniversitiesController(_linkGenerator, _logger, _universityService);
            id = new Guid("1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26");
            repository = new UniversityRepository(dbContext);
            source = new CancellationTokenSource();
            token = source.Token;
            pagedResourceParameters = new UniversityResourceParameters();
            context = new ActionContext();
            universityDto = new UniversityDto();

            //serviceProvider
            var serviceCollection = new ServiceCollection();
            //serviceCollection.AddSingleton<IUniversityService, UniversityService>();
            //serviceCollection.AddTransient<University>();
            serviceCollection.AddTransient<IInstitutionService, InstitutionService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
            //--------------------------------------------------------------

            uow = new UnitOfWork(dbContext, _serviceProvider);

            //logger
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.University", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog();
            });
            logger = loggerFactory.CreateLogger<UniversityService>();
            logger.LogInformation("Example log message");
            //------------------------------------------------------------------

            _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }).CreateMapper();
            universityService = new UniversityService(uow, _mapper, logger);
        }

        [Test]
        public void GetUniversity()
        {
            // Arrange
            //var controller = new UniversitiesController(_linkGenerator, _logger, _universityService);
            //controller.Request = new HttpRequestMessage(); **Not for MVC
            //controller.Configuration = new HttpConfiguration(); **Not for MVC

            /*CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            // Act
            var response = controller.GetUniversity('1B7F86DD-FCD0-42A5-5E5F-08D7D5A16E26', token);*/

            // Assert
            /*University university;
            Assert.IsTrue(response.TryGetContentValue<university>(out university));
            Assert.AreEqual(10, university.Id);*/

            var actResult = obj.GetUniversity(id, token) as Task<ActionResult>;

            Assert.That(actResult, Is.InstanceOf<Task<ActionResult>>());

            // Get the actual result from the task
            //var viewresult = actResult.Result;

            // Get the model from the viewresult and cast it to the correct type 
            // (notice in the first line I changed ActionResult to ViewResult to make sure we can access the model.
            //var model = (List<University>)((ViewResult)viewresult).Model;

            //Assert
            //Assert.IsNotNull(actResult);
            //Assert.AreEqual(2, model.Count);
        }

        //[Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        /*public async Task GetUniversities_Should_Return_List_Of_Universities()
        {
            var universities = await obj.GetUniversities(pagedResourceParameters, token);
            var resultUniversities = universities.ExecuteResultAsync(context);
            //Assert.AreEqual(universities., 4);
        
        }*/

        [Test]
        public void TestUniversities()
        {
            /*var valueServiceMock = new Mock<IUniversityService>(); 
            valueServiceMock.Setup(service => service.GetUniversityByIdAsync(id, token))
                        //.Returns(new[] { "value1", "value2" });
                        .Returns(UniversityDto universityDto);

            var values = obj.GetUniversities(pagedResourceParameters, token);

            Assert.AreEqual(values.Length, 2);
            Assert.AreEqual(values[0], "value1");
            Assert.AreEqual(values[1], "value2");*/
        }

        //[Test]
        public async Task UniversityGetTest()
        {
            //using (var context = GetContextWithData())
            //{
                //var controller = new ConfigurationSearchController(context);
                //var items = context.Configurations.Count();

                var actionResult = await obj.GetUniversities(pagedResourceParameters, token);

                var okResult = actionResult as OkObjectResult;
                var actualConfiguration = okResult.Value as UniversityDto;

                // Now you can compare with expected values
                //actualConfiguration.Should().BeEquivalentTo(expected);
            //}
        }

        [Test]
        public void Test_Service_GetUniversity()
        {
            var expected = universityService.GetUniversityByIdAsync(id, It.IsAny<CancellationToken>());
            //var expected = universityService.GetUniversityByIdAsync(id, It.IsAny<CancellationToken>()).Result;
            //var universityDto = await _universityService.GetUniversityByIdAsync(id, token);
            Assert.IsNotNull(expected);
            //Assert.AreEqual(expected.Result.Id, universityDtoActual.Id);
        }
    }
}
