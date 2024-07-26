using Application.UseCase.Contacts.CreateContact;
using Application.UseCase.Contacts.DeleteContacts;
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
    public class DeleteContactUseCaseTest
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly IDeleteContactUseCase _useCase;
        private readonly Mock<ILogger<DeleteContactUseCase>> _loggerMock;
        public DeleteContactUseCaseTest()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _loggerMock = new Mock<ILogger<DeleteContactUseCase>>();
            _useCase = new DeleteContactUseCase(_contactRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "Should Delete a Contact Sucessfully")]
        public async Task ShouldDeleteContactSucefully()
        {
            //Arrange
            _contactRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var output = await _useCase.DeleteContactAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            //Assert
            output.Should().BeTrue();
        }

        [Fact(DisplayName = "Should false if contact not exists")]
        public async Task ShouldThrowIfContactNotExists()
        {
            //Arrange
            var result = new ArgumentException("Contato não encontrado: 1");

            _contactRepositoryMock.Setup(x => x.DeleteAsync(1, It.IsAny<CancellationToken>()))
                .Throws(result);

            //Act
            var output = await _useCase.DeleteContactAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            //Assert
            Assert.False(output);
        }
    }
}
