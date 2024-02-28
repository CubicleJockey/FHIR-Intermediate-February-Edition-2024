# Delete a Resource
How to delete an existing instance of a resource
Although it is rarely used, some libraries allow deletion of resources. Server-specific processing will determine if the deletion is processed or rejected.

Note: Remember to import the appropriate classes from the HAPI FHIR API.

```java
import ca.uhn.fhir.context.FhirContext;
import ca.uhn.fhir.rest.client.api.IGenericClient;
import org.hl7.fhir.instance.model.api.IBaseOperationOutcome;
import ca.uhn.fhir.model.primitive.IdDt;
```

In order to delete an existing instance of a FHIR resource, you need to follow these steps:

![Receipe Icon](./images/recipe-icon.png)

## Recipe # J-8: Delete a Resource Instance
1. **Invoke the delete method on the server.**
2. **Process the server response (outcome).**

```java
// Step 1 Invoke the delete method on the server  
IBaseOperationOutcome resp = client.delete()
    .resourceById(new IdDt("|ResourceClass|", "|ResourceId|")).execute();  

// Step 2 Process the server's response  
if (resp != null) {  
    System.out.println(resp.toString());  
}
```

**You need to replace:**
- `|ResourceClass|`: The name of the resource class. Example: Patient
- `|ResourceId|`: The resource id for the resource instance you need to delete

![Example Icon](./images/example-icon.png)

### Example #J-7: Delete Practitioner Resource Instance
We will delete the resource for our Practitioner

```java
IBaseOperationOutcome resp = client.delete().resourceById(new IdDt("Practitioner", "79281")).execute();  
```

![Micro-Assignment](./images/micro-assignment-icon.png)

## Micro Assignment #J-7: Delete the Patient resource created previously
