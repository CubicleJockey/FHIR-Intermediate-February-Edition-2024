## Connection

### FHIR version / Context

In Java HAPI FHIR, the first task is to define the context, where you need to specify the FHIR version. The FHIR context is "expensive," so it's better to keep only one instance for the lifetime of the app. It can be passed around (it's thread safe).

Options:
```java
FhirContext.forDSTU2();
FhirContext.forDSTU3();
FhirContext.forR4();
```

We will use the last one (R4) throughout this edition of our course.

### Endpoint

In Java HAPI FHIR, the server endpoint is defined using a parameter for the constructor.

![Example Icon](./images/example-icon.png)

#### Example #J-1: Set Server Context

This will set the context for FHIR R4 and assign our course server as the server.

```java
FhirContext ctx = FhirContext.forR4();
String serverBase = "http://fhir.hl7fundamentals.org/r4";
IGenericClient client = ctx.newRestfulGenericClient(serverBase);
```

![Micro-Assignment Icon](./images/micro-assignment-icon.png)

### Micro Assignment #J-1: Set up your Java HAPI FHIR environment

* Download the requirements (JDK8).
* Open the `fhir-intermediate-ma-ex` project and compile it. Maven should load all required dependencies defined by the `pom.xml` file.
* Run the project. You should only see this message: "I am just a skeleton so I do nothing," because this program only creates the context and defines the client.
