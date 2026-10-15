using EventsHub.Application.Core.Mapping;
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

    private class PersonSummary
    {
        public string Name { get; set; } = "";
        public string Label { get; set; } = "";
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

    private class Address
    {
        public string City { get; set; } = "";
    }

    private class AddressCopy
    {
        public string City { get; set; } = "";
    }

    private class Order
    {
        public string Number { get; set; } = "";
        public Address? Ship { get; set; }
        public List<Address> Stops { get; set; } = [];
    }

    private class OrderCopy
    {
        public string Number { get; set; } = "";
        public AddressCopy? Ship { get; set; }
        public List<AddressCopy> Stops { get; set; } = [];
    }

    private class OrderWithArray
    {
        public AddressCopy[] Stops { get; set; } = [];
    }

    private class NoDefaultConstructor
    {
        public NoDefaultConstructor(int value) => Value = value;
        public int Value { get; }
        public string Name { get; set; } = "";
    }

    private class WithReadOnlyMember
    {
        public string Name { get; set; } = "";
        public string Shout => Name.ToUpperInvariant();
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

    private class OverriddenCoordinatesProfile : Profile
    {
        public OverriddenCoordinatesProfile() =>
            CreateMap<Coordinates, NumericCoordinates>()
                .ForMember(d => d.Latitude, o => o.MapFrom(s => double.Parse(s.Latitude)));
    }

    private class IgnoredCoordinatesProfile : Profile
    {
        public IgnoredCoordinatesProfile() =>
            CreateMap<Coordinates, NumericCoordinates>()
                .ForMember(d => d.Latitude, o => o.Ignore());
    }

    private class IgnoreAgeProfile : Profile
    {
        public IgnoreAgeProfile() =>
            CreateMap<Person, PersonCopy>().ForMember(d => d.Age, o => o.Ignore());
    }

    private class ComputedProfile : Profile
    {
        public ComputedProfile() =>
            CreateMap<Person, PersonSummary>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.OnlyInSource))
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Name + " (" + s.Age + ")"));
    }

    private class OrderProfile : Profile
    {
        public OrderProfile() => CreateMap<Order, OrderCopy>();
    }

    private class AddressProfile : Profile
    {
        public AddressProfile() => CreateMap<Address, AddressCopy>();
    }

    private class OrderWithArrayProfile : Profile
    {
        public OrderWithArrayProfile() => CreateMap<Order, OrderWithArray>();
    }

    private class NoDefaultConstructorProfile : Profile
    {
        public NoDefaultConstructorProfile() => CreateMap<Person, NoDefaultConstructor>();
    }

    private class ReadOnlySelectorProfile : Profile
    {
        public ReadOnlySelectorProfile() =>
            CreateMap<Person, WithReadOnlyMember>()
                .ForMember(d => d.Shout, o => o.MapFrom(s => s.Name));
    }

    private class EmptyOverrideProfile : Profile
    {
        public EmptyOverrideProfile() =>
            CreateMap<Person, PersonCopy>().ForMember(d => d.Name, o => { });
    }

    [Test]
    public void Map_WhenDestinationExists_CopiesMembersAndReturnsSameInstance()
    {
        // Arrange
        var mapper = new Mapper(new PersonProfile());
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
        var mapper = new Mapper(new PersonProfile());
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
        var mapper = new Mapper(new PersonProfile());
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
        var mapper = new Mapper(new PersonProfile());
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
        var mapper = new Mapper(new PersonProfile());
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
    public void Constructor_WhenMapIsDeclaredTwice_ThrowsNamingBothProfiles()
    {
        // Arrange
        var profiles = new Profile[] { new PersonProfile(), new DuplicatePersonProfile() };

        // Act
        TestDelegate act = () => new Mapper(profiles);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains(nameof(PersonProfile))
            .And.Message.Contains(nameof(DuplicatePersonProfile)));
    }

    [Test]
    public void Constructor_WhenMemberTypesAreIncompatible_ThrowsNamingTheMember()
    {
        // Arrange
        var profiles = new Profile[] { new IncompatibleProfile() };

        // Act
        TestDelegate act = () => new Mapper(profiles);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains("Latitude")
            .And.Message.Contains(nameof(Coordinates)));
    }

    [Test]
    public void Map_WhenIncompatibleMemberIsComputed_UsesTheExpression()
    {
        // Arrange
        var mapper = new Mapper(new OverriddenCoordinatesProfile());

        // Act
        var result = mapper.Map<NumericCoordinates>(new Coordinates { Latitude = "21.5" });

        // Assert
        Assert.That(result.Latitude, Is.EqualTo(21.5));
    }

    [Test]
    public void Map_WhenIncompatibleMemberIsIgnored_DoesNotThrowAndLeavesItUnchanged()
    {
        // Arrange
        var mapper = new Mapper(new IgnoredCoordinatesProfile());
        var destination = new NumericCoordinates { Latitude = 7 };

        // Act
        mapper.Map(new Coordinates { Latitude = "21.5" }, destination);

        // Assert
        Assert.That(destination.Latitude, Is.EqualTo(7));
    }

    [Test]
    public void Map_WhenMemberIsIgnored_PreservesDestinationValue()
    {
        // Arrange
        var mapper = new Mapper(new IgnoreAgeProfile());
        var destination = new PersonCopy { Name = "Old", Age = 99 };

        // Act
        mapper.Map(new Person { Name = "Ana", Age = 31 }, destination);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(destination.Name, Is.EqualTo("Ana"));
            Assert.That(destination.Age, Is.EqualTo(99));
        });
    }

    [Test]
    public void Map_WhenMemberIsComputed_UsesExpressionEvenIfSourceHasSameName()
    {
        // Arrange
        var mapper = new Mapper(new ComputedProfile());
        var source = new Person { Name = "Ana", Age = 31, OnlyInSource = "from other member" };

        // Act
        var result = mapper.Map<PersonSummary>(source);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("from other member"));
            Assert.That(result.Label, Is.EqualTo("Ana (31)"));
        });
    }

    [Test]
    public void ForMember_WhenNoRuleIsDeclared_ThrowsArgumentException()
    {
        // Act
        TestDelegate act = () => new EmptyOverrideProfile();

        // Assert
        Assert.That(act, Throws.TypeOf<ArgumentException>().With.Message.Contains("Name"));
    }

    [Test]
    public void ForMember_WhenSelectorIsNotWritable_ThrowsArgumentException()
    {
        // Act
        TestDelegate act = () => new ReadOnlySelectorProfile();

        // Assert
        Assert.That(act, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Map_WhenMemberTypesDifferAndMapExists_MapsNestedObject()
    {
        // Arrange
        var mapper = new Mapper(new OrderProfile(), new AddressProfile());
        var source = new Order { Number = "A-1", Ship = new Address { City = "Leon" } };

        // Act
        var result = mapper.Map<OrderCopy>(source);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Ship, Is.Not.Null);
            Assert.That(result.Ship!.City, Is.EqualTo("Leon"));
        });
    }

    [Test]
    public void Map_WhenNestedSourceIsNull_LeavesDestinationNull()
    {
        // Arrange
        var mapper = new Mapper(new OrderProfile(), new AddressProfile());

        // Act
        var result = mapper.Map<OrderCopy>(new Order { Number = "A-1", Ship = null });

        // Assert
        Assert.That(result.Ship, Is.Null);
    }

    [Test]
    public void Map_ResolvesNestedMapsRegardlessOfProfileOrder()
    {
        // Arrange
        var mapper = new Mapper(new AddressProfile(), new OrderProfile());
        var source = new Order { Ship = new Address { City = "Leon" } };

        // Act
        var result = mapper.Map<OrderCopy>(source);

        // Assert
        Assert.That(result.Ship!.City, Is.EqualTo("Leon"));
    }

    [Test]
    public void Map_WhenMemberIsCollectionOfMappedElements_MapsEachElementInOrder()
    {
        // Arrange
        var mapper = new Mapper(new OrderProfile(), new AddressProfile());
        var source = new Order
        {
            Stops = [new Address { City = "Leon" }, new Address { City = "Irapuato" }, new Address { City = "Celaya" }]
        };

        // Act
        var result = mapper.Map<OrderCopy>(source);

        // Assert
        Assert.That(result.Stops.Select(s => s.City), Is.EqualTo(new[] { "Leon", "Irapuato", "Celaya" }));
    }

    [Test]
    public void Constructor_WhenCollectionDestinationCannotBeBuilt_ThrowsNamingTheMember()
    {
        // Arrange
        var profiles = new Profile[] { new OrderWithArrayProfile(), new AddressProfile() };

        // Act
        TestDelegate act = () => new Mapper(profiles);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains("Stops"));
    }

    [Test]
    public void Constructor_WhenDestinationHasNoParameterlessConstructor_ThrowsNamingTheType()
    {
        // Arrange
        var profiles = new Profile[] { new NoDefaultConstructorProfile() };

        // Act
        TestDelegate act = () => new Mapper(profiles);

        // Assert
        Assert.That(act, Throws.TypeOf<InvalidOperationException>()
            .With.Message.Contains(nameof(NoDefaultConstructor)));
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
        var mapper = new Mapper(new PersonProfile());
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
