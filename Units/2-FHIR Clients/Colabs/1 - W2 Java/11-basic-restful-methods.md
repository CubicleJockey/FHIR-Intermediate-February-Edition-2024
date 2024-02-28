# Basic RESTful Methods

Right now we will cover the traditional RESTful methods (CRUD): create, read, update and delete resources, and in the last steps of this subunit we will cover searching.

Always remember to import the required classes from the HAPI model.

Note: Remember to import the appropriate classes from the HAPI FHIR model –see more details on resource population below, in the section "Java HAPI FHIR: Resource Model."

```java
import org.hl7.fhir.r4.model.|ResourceClass|;     // For each resource class  
import org.hl7.fhir.r4.model.DateType;              // For Dates  
import org.hl7.fhir.r4.model.Enumerations;          // For Enumerated Datatypes  
import org.hl7.fhir.instance.model.api.IIdType;     // For Resource ID  
import ca.uhn.fhir.rest.api.MethodOutcome;          // For Outcome of Creation  
```
