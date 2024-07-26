using Application.UseCase.Contacts.CreateContact;
using Application.UseCase.Contacts.UpdateContact;
using Application.UseCase.Contacts.UpdateContacts;
using Domain.Dtos;
using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.UseCases.Contacts
{
    public class UpdateContactUseCaseTest
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IDDDRepository> _DDDRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly IUpdateContactUseCase _useCase;
        private readonly Mock<ILogger<UpdateContactUseCase>> _loggerMock;
        public UpdateContactUseCaseTest() 
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _DDDRepositoryMock = new Mock<IDDDRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger<UpdateContactUseCase>>();
            _useCase = new UpdateContactUseCase(_contactRepositoryMock.Object,
                                                                _DDDRepositoryMock.Object,
                                                                _tokenServiceMock.Object,
                                                                _userRepositoryMock.Object,
                                                                _loggerMock.Object);

        }

        [Fact(DisplayName = "Should Update a Contact Sucessfully")]
        public async Task ShouldUpdateContactSucefully()
        {
            //Arrange
            var input = new UpdateContactInput
            {
                Name = "Name2",
                Email = "email@email.com.br2",
                Phone = "(011)98877-6654",
            };

            _tokenServiceMock.Setup(x => x.DecodeToken(It.IsAny<string>()))
                .Returns(new TokenInfo { Name = "usuario", Email = "teste@teste.com" });

            _userRepositoryMock.Setup(x => x.GetByEmailAsync("teste@teste.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User("usuario", "teste@teste.com", "Fiap@123"));

            _DDDRepositoryMock.Setup(x => x.GetDDDByCodeAsync(input.Phone.Substring(1, 3)))
                .ReturnsAsync(new DDD { Id = 1, Code = "011", Region = "Sudeste", UF = "SP" });

            _contactRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var output = await _useCase.UpdateContactAsync(input, It.IsAny<string>(), It.IsAny<CancellationToken>());

            //Assert
            output.Should().BeTrue();
        }

        [Fact(DisplayName = "Should false if contact not exists")]
        public async Task ShouldUpdateContactFail()
        {
            //Arrange
            var input = new UpdateContactInput
            {
                Name = "Name3",
                Email = "emailfail@email.com.br",
                Phone = "(011)98877-6654",
            };

            _tokenServiceMock.Setup(x => x.DecodeToken(It.IsAny<string>()))
                .Returns(new TokenInfo { Name = "usuario", Email = "teste@teste.com" });

            _userRepositoryMock.Setup(x => x.GetByEmailAsync("teste@teste.com", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User("usuario", "teste@teste.com", "Fiap@123"));

            _DDDRepositoryMock.Setup(x => x.GetDDDByCodeAsync(input.Phone.Substring(1, 3)))
                .ReturnsAsync(new DDD { Id = 1, Code = "011", Region = "Sudeste", UF = "SP" });

            _contactRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            //Act
            var output = await _useCase.UpdateContactAsync(input, It.IsAny<string>(), It.IsAny<CancellationToken>());

            //Assert
            Assert.False(output);
        }
    }
}
