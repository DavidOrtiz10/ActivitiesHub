# Spec Delta

## ADDED Requirements

### Requirement: Per-member overrides
A profile SHALL be able to override how a single destination member is filled, by computing it from the source or by ignoring it, and an override SHALL take precedence over name matching for that member.

#### Scenario: Computed member
- **WHEN** a profile declares that destination member `X` is computed from an expression over the source
- **THEN** mapping sets `X` to the expression's result, even if the source also has a member named `X`

#### Scenario: Ignored member
- **WHEN** a profile declares that destination member `X` is ignored and a source is mapped onto an existing destination
- **THEN** `X` keeps the value it had before the call

#### Scenario: Override without a rule
- **WHEN** a profile selects a destination member for an override but declares neither a computation nor an ignore
- **THEN** the profile fails with an argument error naming the member

#### Scenario: Selector is not a writable member
- **WHEN** a profile selects something other than a public writable property of the destination
- **THEN** the profile fails with an argument error

### Requirement: Nested and collection members
The mapper SHALL map a member whose source and destination types differ through the declared map for those types, and SHALL map a collection member element by element in order through the map for the element types.

#### Scenario: Nested object
- **WHEN** a member's source and destination types differ and a map is declared between them, in any profile and in any registration order
- **THEN** the destination member is populated by that map
- **AND** a null source member leaves the destination member null

#### Scenario: Collection of mapped elements
- **WHEN** a source collection's element type has a declared map to the destination collection's element type
- **THEN** the destination holds one mapped element per source element, in the same order

### Requirement: Destination construction is validated at startup
The system SHALL fail at startup when a declared map's destination type has no public parameterless constructor.

#### Scenario: Destination cannot be constructed
- **WHEN** a profile declares a map to a type with only a constructor that takes arguments
- **THEN** startup fails with an error that names the destination type

## MODIFIED Requirements

### Requirement: Incompatible members fail at startup
The system SHALL fail at startup when a declared map has a source member and a destination member with the same name whose types are not compatible, unless an override, a nested map or a collection map converts that member.

#### Scenario: Same name, incompatible types
- **WHEN** a profile declares a map where `Latitude` is `string` on the source and `double` on the destination
- **THEN** startup fails with an error that names the map and the member
- **AND** the application does not start accepting requests with a partially valid map

#### Scenario: Override resolves the mismatch
- **WHEN** the same map declares `Latitude` as computed from the source or as ignored
- **THEN** startup succeeds

#### Scenario: Collection that cannot be built
- **WHEN** a source collection member maps to a destination collection type that a list of the mapped elements cannot be assigned to, such as an array
- **THEN** startup fails with an error that names the map and the member
