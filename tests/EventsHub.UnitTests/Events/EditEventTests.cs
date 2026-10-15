using EventsHub.Application.Core.Mapping;
using EventsHub.Application.Events.Commands;
using EventsHub.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.UnitTests.Events;

[TestFixture]
public class EditEventTests
{
    private ServiceProvider _serviceProvider;
    private EditEvent.Handler _handler;
    private string _eventId;
    private string _originalTitle;
    private string _originalCity;

    [SetUp]
    public async Task Setup()
    {
        var services = new ServiceCollection();
        services.AddMapper(typeof(MappingProfiles).Assembly);
        _serviceProvider = services.BuildServiceProvider();

        _handler = new EditEvent.Handler(
            GlobalTestSetup.AppDbContext,
            _serviceProvider.GetRequiredService<IMapper>());

        var existing = await GlobalTestSetup.AppDbContext.Events.FirstAsync();
        _eventId = existing.Id;
        _originalTitle = existing.Title;
        _originalCity = existing.City;
    }

    [TearDown]
    public async Task TearDown()
    {
        // The test database is shared by every test, so put the edited event back.
        var tracked = await GlobalTestSetup.AppDbContext.Events.FindAsync(_eventId);
        tracked!.Title = _originalTitle;
        tracked.City = _originalCity;
        await GlobalTestSetup.AppDbContext.SaveChangesAsync();

        _serviceProvider.Dispose();
    }

    [Test]
    public async Task Handle_WhenEventExists_PersistsEditedValues()
    {
        // Arrange
        var tracked = await GlobalTestSetup.AppDbContext.Events.FindAsync(_eventId);
        var edited = new Event
        {
            Id = _eventId,
            Title = "Edited title",
            Description = tracked!.Description,
            Category = tracked.Category,
            City = "Edited city",
            Venue = tracked.Venue,
            Latitude = tracked.Latitude,
            Longitude = tracked.Longitude,
            Date = tracked.Date,
            IsCancelled = tracked.IsCancelled
        };

        // Act
        await _handler.Handle(new EditEvent.Command { Event = edited }, CancellationToken.None);

        // Assert
        var reloaded = await GlobalTestSetup.AppDbContext.Events
            .AsNoTracking()
            .FirstAsync(e => e.Id == _eventId);
        Assert.Multiple(() =>
        {
            Assert.That(reloaded.Title, Is.EqualTo("Edited title"));
            Assert.That(reloaded.City, Is.EqualTo("Edited city"));
        });
    }

    [Test]
    public void Handle_WhenEventDoesNotExist_Throws()
    {
        // Arrange
        var missing = new Event
        {
            Title = "Missing",
            Description = "Missing",
            Category = "none",
            City = "Nowhere",
            Venue = "Nowhere",
            Latitude = "0",
            Longitude = "0"
        };

        // Act
        AsyncTestDelegate act = () =>
            _handler.Handle(new EditEvent.Command { Event = missing }, CancellationToken.None);

        // Assert
        Assert.That(act, Throws.Exception.With.Message.EqualTo("Event not found"));
    }
}
