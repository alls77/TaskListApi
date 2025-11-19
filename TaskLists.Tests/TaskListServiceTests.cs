using FluentAssertions;
using Moq;
using TaskLists.Application.Common;
using TaskLists.Application.DTOs;
using TaskLists.Application.Interfaces;
using TaskLists.Application.Services;
using TaskLists.Domain.Entities;

namespace TaskLists.Tests;

public class TaskListServiceTests
{
    private readonly Mock<ITaskListRepository> _repositoryMock;
    private readonly TaskListService _sut;

    public TaskListServiceTests()
    {
        _repositoryMock = new Mock<ITaskListRepository>();
        _sut = new TaskListService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetListsForUserAsync_WithValidUserId_ReturnsSuccessResult()
    {
        // Arrange
        var userId = "user123";
        var pager = Pager.Create(1, 10).Value;
        var expectedItems = new List<TaskListDto>
        {
            new("1", "Work Tasks")
        };
        var expectedPagedResult = new PagedResult<TaskListDto>(expectedItems, 1, 10, 1);

        _repositoryMock
            .Setup(x => x.GetListsForUserAsync(
                userId,
                pager!,
                It.Is<Sort>(s => s.SortBy == nameof(TaskList.CreatedAt) && s.SortDesc),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPagedResult);

        // Act
        var result = await _sut.GetListsForUserAsync(userId, pager!, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedPagedResult);
        result.Error.Should().BeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public async Task GetListsForUserAsync_WithInvalidUserId_ReturnsFailureResult(string invalidUserId)
    {
        // Arrange
        var pager = Pager.Create(1, 10).Value;

        // Act
        var result = await _sut.GetListsForUserAsync(invalidUserId, pager, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.Error.Should().Be("userId is required");
    }

    [Fact]
    public async Task GetListsForUserAsync_WithEmptyResult_ReturnsSuccessWithEmptyList()
    {
        // Arrange
        var userId = "user123";
        var pager = Pager.Create(1, 10).Value;
        var emptyResult = new PagedResult<TaskListDto>(new List<TaskListDto>(), 1, 10, 0);

        _repositoryMock
            .Setup(x => x.GetListsForUserAsync(
                userId,
                pager!,
                It.IsAny<Sort>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyResult);

        // Act
        var result = await _sut.GetListsForUserAsync(userId, pager!, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Items.Should().BeEmpty();
        result.Value.Total.Should().Be(0);
    }
}