using Application.UseCase.Contacts.CreateContact;
using Application.UseCase.Users.CreateUser;
using Castle.Core.Logging;
using Domain.Dtos;
using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.UseCases.Contacts
{
    public class CreateContactUseCaseTest
    {
        private readonly Mock<IContactRepository>            _contactRepositoryMock;
        private readonly Mock<IUserRepository>               _userRepositoryMock;
        private readonly Mock<IDDDRepository>                _DDDRepositoryMock;
        private readonly Mock<ITokenService>                 _tokenServiceMock;
        private readonly ICreateContactUseCase               _useCase;
        private readonly Mock<ILogger<CreateContactUseCase>> _loggerMock;
        
        public CreateContactUseCaseTest()
        {
            _contactRepositoryMock  = new Mock<IContactRepository>();
            _userRepositoryMock     = new Mock<IUserRepository>();
            _DDDRepositoryMock      = new Mock<IDDDRepository>();
            _tokenServiceMock       = new Mock<ITokenService>();
            _loggerMock             = new Mock<ILogger<CreateContactUseCase>>();
            _useCase                = new CreateContactUseCase(_contactRepositoryMock.Object, 
                                                               _userRepositoryMock.Object,
                                                               _tokenServiceMock.Object,
                                                               _DDDRepositoryMock.Object,
                                                               _loggerMock.Object);
        }

        [Fact(DisplayName = "Should Create a Contact Sucessfully")]
        public async Task ShouldCreateContactSucefully()
        {
            //Arrange
            var input = new CreateContactInput
            {
                Name = "Name",
                Email = "email@email.com.br",
                Phone = "(011)98877-6655",
            };

            _tokenServiceMock.Setup(x => x.DecodeToken(It.IsAny<string>()))
                .Returns(new TokenInfo { Name = "usuario", Email = "teste@teste.com" });

            _userRepositoryMock.Setup(x => x.GetByEmailAsync("teste@teste.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User("usuario", "teste@teste.com", "Fiap@123"));

            _DDDRepositoryMock.Setup(x => x.GetDDDByCodeAsync(input.Phone.Substring(1, 3)))
                .ReturnsAsync(new DDD { Id = 1, Code = "011", Region = "Sudeste", UF = "SP" });

            _contactRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var output = await _useCase.CreateContactAsync(input, It.IsAny<string>(), It.IsAny<CancellationToken>());

            //Assert
            output.Should().BeTrue();
        }

        [Fact(DisplayName = "Should throw if contact exists")]
        public async Task ShouldThrowIfEmailAlreadyRegistered()
        {
            var input = new CreateContactInput
            {
                Name = "Name",
                Email = "email@email.com.br",
                Phone = "(011)98877-6655",
            };

            var contactResponse = new Contact(1, "Name", "email@email.com.br", "(011)98877-6655");

            _tokenServiceMock.Setup(x => x.DecodeToken(It.IsAny<string>()))
                .Returns(new TokenInfo { Name = "usuario", Email = "teste@teste.com" });

            _contactRepositoryMock.Setup(x => x.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(contactResponse);

            _userRepositoryMock.Setup(x => x.GetByEmailAsync("teste@teste.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User("usuario", "teste@teste.com", "Fiap@123"));

            Func<Task> act = async () => await _useCase.CreateContactAsync(input, It.IsAny<string>(), It.IsAny<CancellationToken>());

            await act.Should().ThrowAsync<InvalidOperationException>();
        }
    }     
}
