# Conformance
Obtaining the server's capability statement
Conformance in the Java HAPI FHIR is represented using the capabilities() method of the client class.

Note: Remember to import the appropriate classes from the HAPI FHIR API.

To obtain the statement, just invoke it with the parameter ofType(CapabilityStatement.class).

```java
// Recipe # J-9: Obtain the Server Capability Statement
import org.hl7.fhir.r4.model.CapabilityStatement;  
CapabilityStatement MyConf = client.capabilities().  
   ofType(CapabilityStatement.class).execute();  
```
The response is a resource, so it can be serialized to JSON or XML, or explored like any other FHIR resource.

![Micro-Assessment](./images/micro-assignment-icon.png)

## Micro Assignment #J-8: Obtain the conformance resource of our course server
Endpoint: http://fhir.hl7fundamentals.org/r4
