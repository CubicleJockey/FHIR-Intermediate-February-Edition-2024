# Update a Resource

![Receipe Icon](./images/recipe-icon.png)

## Recipe # J-7: Update a Resource Instance
Note: Remember to import the appropriate classes from the HAPI FHIR model –see more details on resource population below, in the section "Resource Model."

```java
import org.hl7.fhir.r4.model.|ResourceClass|;    // For each resource class
import org.hl7.fhir.r4.model.DateType;             // For Dates
import org.hl7.fhir.r4.model.Enumerations;         // For Enumerated data types
import org.hl7.fhir.instance.model.api.IIdType;    // For Resource ID
import ca.uhn.fhir.rest.api.MethodOutcome;         // For Outcome of Creation
```

In order to update an existing instance of a FHIR resource, you need to follow these steps:

1. **Load the existing resource instance from the server using any of the techniques described in our previous "Read" section.**
2. **Modify or add the needed resource attributes.**
3. **Invoke the update method on the server.**
4. **Process the server response (outcome).**

```java
// Step 1 Load the resource instance from the server  
|ResourceClass| MyResourceName = client.read().resource(|ResourceClass|.class).withId("|ResourceId|").execute();  

// Step 2 Modify the resource instance (add or set elements)  
{  
    |ResourceClass| |MyResourceName| = new |ResourceClass|();  
    |MyResourceName|.|setElementName|(|ElementValue|);  
    // ...  
    |MyResourceName|.|addElementName|()  
                     .|setElementName|(|ElementValue|) 
      // Or      
      .|addElementName|()..;  
}  

// Step 3 Invoke the update method on the server  
MethodOutcome outcome = client.update()  
      .resource(|MyResourceName|)  
      .execute();  

// Step 4 Process the server's response  
String version = outcome.getId().getVersionIdPart();  
System.out.println("Updated resource, got version: " + version);  
```

**You need to replace:**
- `|ResourceClass|`: The name of the resource class. Example: Patient
- `|setElementName|`, `|ElementValue|`: For non-repeating elements: the name and value - data type valid - of the element to fill in the resource.
- `|addElementName|`: For repeating elements: the name of the element to add to the resource or element. Not only when they actually repeat, but IF the cardinality is 0/1...n.
- `|MyResourceName|`: The variable holding the instance you are creating.
- `|ResourceId|`: The resource id for the resource instance you need to update.

![Example Icon](./images/example-icon.png)

### Example #J-6: Update a Practitioner Resource Instance
We will update the resource for the new Practitioner resource, adding the photo.

Note: This example is too long to include here, so you can download it from the gist link above.

![Micro-Assignment Icon](./images/micro-assignment-icon.png)

## Micro Assignment #J-6: Update the Patient resource created in #J-5
This is the new patient information:
- Patient Address: 3600 Papineau Avenue, Montreal, Quebec (H2K 4J5), Canada
- Patient Phone #: 613-555-5555
