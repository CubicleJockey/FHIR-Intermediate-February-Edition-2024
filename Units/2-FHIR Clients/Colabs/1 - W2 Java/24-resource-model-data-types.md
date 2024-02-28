# Resource Model – Data Types

HAPI FHIR classes for FHIR data types are named `|DataType|`. Example: `HumanName` is a FHIR DataType, and the HAPI FHIR class for `HumanName` is `HumanName`.

(That statement sounds pretty silly, but it's actually useful information, because the names of the classes for each FHIR data type in HAPI FHIR have changed from version to version.)

Each class allows read/write access to its own elements through different methods.

### Write Methods: Populating Resources
- `set|ElementName|`: set the element to a value
- `add|ElementName|`: add a new element to an array value

### Read Methods: Extracting Values from Elements
- `get|ElementName|`: gets the value of the element
- `has|ElementName|`: true if the element has any defined value

JAVA HAPI FHIR allows chaining all access methods in the same sentence.

For simple coded elements with codes defined in the FHIR specification, HAPI FHIR defines enums for each FHIR version, enforcing the use of the proper set of valid codes.

Let's review how to use the data types for some of the more commonly used data types in FHIR by populating a patient example.
