using Microsoft.AspNetCore.Mvc;
using PersonApp.Controllers;
using PersonApp.Data;
using PersonApp.Repository;
using Xunit;
using Moq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PersonApp.Models;


namespace PersonTests
{
    public class PersonTests
    {
        private readonly Mock<IPersonRepository> _mockrepo;
        private readonly PersonController _controller;
        //private readonly PersonRepository _personRepository;
        public PersonTests()
        {
            _mockrepo = new Mock<IPersonRepository>();
            _controller = new PersonController(_mockrepo.Object);
        }
        List<PersonModel> personlist = new List<PersonModel> {
            new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "happy",
                LastName = "notes",
                Age = 24,
            },
            new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            }
        };
        //GET ALL TESTS
        
        [Fact]
        public void GetallPersons_WhenCalled_ReturnsOkResult()
        {
            //arrange
            _mockrepo.Setup(c => c.GetallPersonsAsync()).Returns(Task.FromResult(personlist));

            //act
            var result = _controller.GetallPersons();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async void GetAll_PersonListEmpty_ShouldReturnNotFound()
        {
            //arrange
            List<PersonModel> plist = new List<PersonModel> { };
            _mockrepo.Setup(c => c.GetallPersonsAsync()).Returns(Task.FromResult(plist));
            //act
            var result = _controller.GetallPersons() ;
            //assert
            Assert.Equal(typeof(NotFoundResult), result.Result.GetType() );
        }
        
        //GET SINGLE PERSON TESTS
        [Fact]
        public async void GetPersonById_ValidID_ShouldReturnOk()
        {
            PersonModel? request = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            Person req = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            //arrange
            _mockrepo.Setup(c => c.GetPersonByIdAsync(request.Id)).Returns(Task.FromResult(request));
            //act
            IActionResult response = await _controller.GetPersonById(request.Id);
            // Assert
            Assert.Equal( typeof(OkObjectResult) , response.GetType());
        }
        [Fact]
        public async void GetPersonById_InvalidID_ShouldReturnNotFound()
        {
            PersonModel? request = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            Person req = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            //arrange
            _mockrepo.Setup(c => c.GetPersonByIdAsync(request.Id)).Returns(Task.FromResult(request));
            //act
            IActionResult response = await _controller.GetPersonById(req.Id);
            // Assert
            Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
        }
        //POST TESTS

        [Fact]
        public async void Post_InvalidInput_NullObject_ShouldReturnBadRequest()
        {
            //Arrange
            PersonModel? request = new PersonModel();
            Person req = new Person(); 
            req = null;
            request = null;
            //Act
            _mockrepo.Setup(c => c.AddPersonAsync(request)).Returns(Task.FromResult(req));
            var result = _controller.AddPersonAsync(request);
            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.Result.GetType());
            _mockrepo.Verify(m => m.AddPersonAsync(It.IsAny<PersonModel>()), Times.Never);
        }
        [Fact]
        public async void Post_ValidInput_All_ShouldReturnOk()
        {
            //Arrange
            PersonModel? request = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            Person req = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            //Act
            _mockrepo.Setup(c => c.AddPersonAsync(request)).Returns(Task.FromResult(req));
            var result = _controller.AddPersonAsync(request);
            

            //Assert
            Assert.NotNull(result);
            //Assert.Equal(201, result.StatusCode);


            IActionResult response = await _controller.AddPersonAsync(request);

            // Assert
            // Assert
            //Assert.Equal(typeof(BadRequestObjectResult), result.Result.GetType());
            ObjectResult objectResponse = Assert.IsType<CreatedAtActionResult>(response);

        }
        //PUT TESTS
        [Fact]
        public async void Put_InvalidInput_NullObject_ShouldReturnBadRequest()
        {
            //Arrange
            PersonModel? request = new PersonModel();
            Person req = new Person();
            req = null;
            request = null;
            //Act
            var result = await _controller.UpdatePersonAsync(request, Guid.NewGuid());

            // Act
            IActionResult response = await _controller.UpdatePersonAsync(request, Guid.NewGuid());

            // Assert
            // Assert
            //Assert.Equal(typeof(BadRequestObjectResult), result.Result.GetType());
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);

            Assert.Equal(400, objectResponse.StatusCode);
        }
        [Fact]
        public async void Put_ValidInput_ShouldReturnNoContent()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            PersonModel? request = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            Person req = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            _mockrepo.Setup(c => c.UpdatePersonAsync(request, request.Id)).Returns(Task.FromResult(req));

            //Act
            IActionResult response = await _controller.UpdatePersonAsync(request, request.Id);
            // Assert
            Assert.Equal(response.GetType(), typeof(NoContentResult));

        }
        //DELETE TESTS
        [Fact]
        public async void Delete_InvalidInput_ShouldReturnNotFound()
        {
            // Act
            IActionResult response = await _controller.DeletePersonAsync(Guid.NewGuid());

            // Assert
            ObjectResult objectResponse = Assert.IsType<NotFoundObjectResult>(response);

            Assert.Equal(404, objectResponse.StatusCode);
        }
        [Fact]
        public async void Delete_validInput_ShouldReturnNoContent()
        {
            //Arrange
            PersonModel? request = new PersonModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            Person req = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "blue",
                LastName = "yellow",
                Age = 34,
            };
            //_mockrepo.Setup(c => c.GetPersonByIdAsync(id)).Returns(Task.FromException<PersonModel>(new InvalidOperationException()) );
            _mockrepo.Setup(c => c.GetPersonByIdAsync(request.Id)).Returns(Task.FromResult(request) );
            //Act
            IActionResult response = await _controller.DeletePersonAsync(request.Id);
            // Assert
            Assert.Equal(response.GetType(), typeof(NoContentResult));
        }
    }
    
}