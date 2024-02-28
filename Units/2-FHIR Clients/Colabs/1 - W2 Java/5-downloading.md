## Downloading and referencing the library

### Downloading the HAPI FHIR

The easiest way is to use a build system like Apache Maven.

If you want to manually download HAPI FHIR, use the GitHub link: [https://github.com/jamesagnew/hapi-fhir/releases](https://github.com/jamesagnew/hapi-fhir/releases).

### Referencing the HAPI FHIR

In order to use HAPI FHIR to create a FHIR R4 compatible client, you will need the following dependencies in your project:

- **Core Library**
  - hapi-fhir-base: Context, parsers, and other support classes
  - hapi-fhir-utilities: Various utility methods
  - hapi-fhir-structures-r4: Model classes for FHIR R4

- **Client**
  - hapi-fhir-client: Core Client Framework, including an HTTP implementation

- **Validation**
  - hapi-fhir-validation: Validation of resources
  - hapi-fhir-validation-resources-r4: Specific for FHIR R4

In all cases, we validated our examples using version 3.8.0 of HAPI FHIR.

There is a pom.xml file in the examples folder, containing the references.

HAPI FHIR supports two interfaces for queries, "fluent" (similar to the C# interface) and "annotation". The fluent client is easier to use, and it is what we will use in our course.

### Interceptors in HAPI FHIR

Interceptors are registrable components to implement various options.

They can run before and after the client operations.

You just need to declare and parameterize your interceptor and then register it in your client before calling the affected operation.


> André's PERSONAL NOTES:
>
> DOCKER
>  * HAPI FHIR w/Sample Patients: https://hub.docker.com/r/smartonfhir/hapi 
>  * HAPI FHIR 5 w/Sample Datasets: https://hub.docker.com/r/smartonfhir/hapi-5