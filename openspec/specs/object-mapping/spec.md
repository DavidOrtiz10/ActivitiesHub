# object-mapping Specification

## Purpose

Defines how the application layer copies data from one object type to another through registered mapping profiles, so use cases can update entities from incoming data without hand-written member-by-member code.

## Requirements

### Requirement: Mapping onto an existing instance
The mapper SHALL copy every readable source member onto the destination member with the same name and a compatible type, and SHALL return the same destination instance it was given.

#### Scenario: Edit overwrites tracked entity
- **WHEN** an `Event` with changed `Title`, `Date` and `City` is mapped onto a tracked `Event` with the same `Id`
- **THEN** the tracked entity holds the new values for every mapped member
- **AND** the same instance is returned, so its change tracking is preserved

#### Scenario: Source member without a destination member
- **WHEN** the source type has a member that the destination type does not
- **THEN** that member is ignored and no error is raised

### Requirement: Mapping to a new instance
The mapper SHALL create a new destination instance and populate it from the source when no destination is supplied.

#### Scenario: Map to new instance
- **WHEN** a source object is mapped to a destination type with a parameterless constructor
- **THEN** a new, distinct destination instance is returned with the mapped member values

#### Scenario: Required members on the destination
- **WHEN** the destination type declares `required` members
- **THEN** the new instance is created and every required member is populated from the source

### Requirement: Mapping is defined by profiles
The mapper SHALL map only between type pairs that a profile has declared, and SHALL reject any other pair.

#### Scenario: Declared map is used
- **WHEN** a profile declares a map from `Event` to `Event` and an `Event` is mapped to `Event`
- **THEN** the mapping succeeds

#### Scenario: Undeclared map is rejected
- **WHEN** an object is mapped between a type pair that no profile declared
- **THEN** the mapper raises an error that names the source type and the destination type
- **AND** the destination instance, if one was supplied, is left unchanged

### Requirement: Profiles are discovered from assemblies
The system SHALL register every profile found in the assemblies it is given at startup, so adding a profile does not require changing the registration code.

#### Scenario: New profile is picked up
- **WHEN** a new profile class is added to a registered assembly and the application starts
- **THEN** the maps it declares are available through the mapper without further registration

#### Scenario: Duplicate map declaration
- **WHEN** two profiles declare a map for the same source and destination pair
- **THEN** startup fails with an error that names the pair and both profiles

### Requirement: Incompatible members fail at startup
The system SHALL fail at startup when a declared map has a source member and a destination member with the same name whose types are not compatible.

#### Scenario: Same name, incompatible types
- **WHEN** a profile declares a map where `Latitude` is `string` on the source and `double` on the destination
- **THEN** startup fails with an error that names the map and the member
- **AND** the application does not start accepting requests with a partially valid map

### Requirement: Null handling
The mapper SHALL reject a null source and SHALL NOT silently produce a null or empty result.

#### Scenario: Null source
- **WHEN** the source argument is null
- **THEN** the mapper raises an argument error and does not modify the destination
