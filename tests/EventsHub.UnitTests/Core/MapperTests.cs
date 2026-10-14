using EventsHub.Application.Core;
using EventsHub.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.UnitTests.Core;

[TestFixture]
public class MapperTests
{
    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string OnlyInSource { get; set; } = "";
    }

    private class PersonCopy
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }

    private class RequiredPersonCopy
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
    }

    private class Unmapped
    {
        public string Name { get; set; } = "";
    }

    private class Coordinates
    {
        public string Latitude { get; set; } = "";
    }

    private class NumericCoordinates
    {
        public double Latitude { get; set; }
    }

    private class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonCopy>();
            CreateMap<Person, RequiredPersonCopy>();
        }
    }

    private class DuplicatePersonProfile : Profile
    {
        public DuplicatePersonProfile() => CreateMap<Person, PersonCopy>();
    }

    private class IncompatibleProfile : Profile
    {
        public IncompatibleProfile() => CreateMap<Coordinates, NumericCoordinates>();
    }

    private static IMapper CreateMapper(params Profile[] profiles)
        => new Mapper(MapperRegistry.FromProfiles(profiles));

    [Test]
    public void Map_WhenDestinationExists_CopiesMembersAndReturnsSameInstance()
    {
        // Arrange
        var mapper = CreateMapper(new PersonProfile());
        var source = new Person { Name = "Ana", Age = 31 };
        var destination = new PersonCopy { Name = "Old", Age = 1 };

        // Act
        var result = mapper.Map(source, destination);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.SameAs(destination));
            Assert.That(destination.Name, Is.EqualTo("Ana"));
            Assert.That(destination.Age, Is.EqualTo(31));
        });
    }

    [Test]
    public void Map_WhenSourceHasMemberMissingOnDestination_IgnoresIt()
    {
        // Arrange
        var mapper = CreateMapper(new PersonProfile());
        var source = new Person { Name = "Ana", Age = 31, OnlyInSource = "extra" };

        // Act
        var result = mapper.Map(source, new PersonCopy());

        // Assert
        Assert.That(result.Name, Is.EqualTo("Ana"));
    }

    [Test]
    public void Map_WhenNoDestinationIsGiven_ReturnsNewInstanceWithMappedValues()
    {
        // Arrange
        var mapper = CreateMapper(new PersonProfile());
        var source = new Person { Name = "Ana", Age = 31 };

        // Act
        var first = mapper.Map<PersonCopy>(source);
        var second = mapper.Map<PersonCopy>(source);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(first, Is.Not.SameAs(second));
            Assert.That(first.Name, Is.EqualTo("Ana"));
            Assert.That(first.Age, Is.EqualTo(31));
        });
    }

    [Test]
    public void Map_WhenDestinationHasRequiredMembers_PopulatesThem()
    {
        // Arrange
        var mapper = CreateMapper(new PersonProfile());
        var source = new Person { Name = "Ana", Age = 31 };

        // Act
        var result = mapper.Map<RequiredPersonCopy>(source);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("Ana"));
            Assert.That(result.Age, Is.EqualTo(31));
        });
    }

    [Test]
    public void Map_WhenMapIsNotDeclared_ThrowsNamingBothTypesAndLeavesDestinationUnchanged()
    {
        // Arrange
        var mapper = CreateMapper(new PersonProfile());
        var source = new Person { Name = "Ana", Age = 31 };
        var destination = new Unmapped { Name = "Untouched" };

        // Act
        TestDelegate act = () => mapper.Map(source, destination);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains(nameof(Person))
            .And.Message.Contains(nameof(Unmapped)));
        Assert.That(destination.Name, Is.EqualTo("Untouched"));
    }

    [Test]
    public void FromProfiles_WhenMapIsDeclaredTwice_ThrowsNamingBothProfiles()
    {
        // Arrange
        var profiles = new Profile[] { new PersonProfile(), new DuplicatePersonProfile() };

        // Act
        TestDelegate act = () => MapperRegistry.FromProfiles(profiles);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains(nameof(PersonProfile))
            .And.Message.Contains(nameof(DuplicatePersonProfile)));
    }

    [Test]
    public void FromProfiles_WhenMemberTypesAreIncompatible_ThrowsNamingTheMember()
    {
        // Arrange
        var profiles = new Profile[] { new IncompatibleProfile() };

        // Act
        TestDelegate act = () => MapperRegistry.FromProfiles(profiles);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains("Latitude")
            .And.Message.Contains(nameof(Coordinates)));
    }

    [Test]
    public void AddMapper_WhenApplicationAssemblyIsScanned_MapsEventOntoEvent()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMapper(typeof(MappingProfiles).Assembly);
        using var provider = services.BuildServiceProvider();
        var mapper = provider.GetRequiredService<IMapper>();

        var source = new Event
        {
            Title = "New title",
            Description = "New description",
            Category = "music",
            City = "Leon",
            Venue = "Forum",
            Latitude = "21.1",
            Longitude = "-101.6",
            IsCancelled = true
        };
        var destination = new Event
        {
            Title = "Old",
            Description = "Old",
            Category = "old",
            City = "Old",
            Venue = "Old",
            Latitude = "0",
            Longitude = "0"
        };

        // Act
        var result = mapper.Map(source, destination);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.SameAs(destination));
            Assert.That(destination.Id, Is.EqualTo(source.Id));
            Assert.That(destination.Title, Is.EqualTo("New title"));
            Assert.That(destination.City, Is.EqualTo("Leon"));
            Assert.That(destination.IsCancelled, Is.True);
        });
    }

    [Test]
    public void Map_WhenSourceIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = CreateMapper(new PersonProfile());
        var destination = new PersonCopy { Name = "Untouched" };

        // Act
        TestDelegate toNew = () => mapper.Map<PersonCopy>(null!);
        TestDelegate toExisting = () => mapper.Map<Person, PersonCopy>(null!, destination);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(toNew, Throws.TypeOf<ArgumentNullException>());
            Assert.That(toExisting, Throws.TypeOf<ArgumentNullException>());
            Assert.That(destination.Name, Is.EqualTo("Untouched"));
        });
    }
}
