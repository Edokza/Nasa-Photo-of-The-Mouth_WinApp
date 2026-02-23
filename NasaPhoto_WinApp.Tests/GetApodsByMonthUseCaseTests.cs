using Xunit;
using Moq;
using NasaPhoto_WinApp.Application.UseCases;
using NasaPhoto_WinApp.Application.Interfaces;
using NasaPhoto_WinApp.Domain.Entities;

public class GetApodsByMonthUseCaseTests
{
    private readonly Mock<IApodRepository> _repoMock;
    private readonly GetApodsByMonthUseCase _useCase;

    public GetApodsByMonthUseCaseTests()
    {
        _repoMock = new Mock<IApodRepository>();
        _useCase = new GetApodsByMonthUseCase(_repoMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Date_Before_1995()
    {
        var result = await _useCase.ExecuteAsync(1994, 1);

        Assert.False(result.Success);
        Assert.Contains("1995", result.ErrorMessage);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Future_Date()
    {
        var futureYear = DateTime.Now.Year + 1;

        var result = await _useCase.ExecuteAsync(futureYear, 1);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Should_Return_Fail_When_No_Data()
    {
        _repoMock.Setup(r => r.GetApodsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                 .ReturnsAsync(new List<Apod>());

        var result = await _useCase.ExecuteAsync(2024, 1);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Should_Return_Success_When_Data_Exists()
    {
        _repoMock.Setup(r => r.GetApodsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
         .ReturnsAsync(new List<Apod>
         {
             new Apod(
                 date: DateTime.Now,
                 title: "Test",
                 explanation: "Test explanation",
                 url: "test.jpg",
                 hdUrl: "test_hd.jpg",
                 media_Type: "image",
                 copyright: null,
                 serviceVersion: "v1"
             )
         });

        var result = await _useCase.ExecuteAsync(2024, 1);
        Assert.NotNull(result);
    }
}