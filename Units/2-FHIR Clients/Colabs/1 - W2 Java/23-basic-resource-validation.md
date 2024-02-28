# Basic Resource Valdiation

![Recipe Icon](./images/recipe-icon.png)

## Recipe #J-14: Basic Resource Validation
In this recipe, the resource you want to validate should be in the string variable `content`:

```java
import ca.uhn.fhir.context.FhirContext;  
import ca.uhn.fhir.validation.*;  
import org.hl7.fhir.r4.model.OperationOutcome;  

FhirValidator val = ctx.newValidator();  
IValidatorModule module1 = new SchemaBaseValidator(ctx);  
val.registerValidatorModule(module1);  

ValidationResult result = val.validateWithResult(content);  
if (result.isSuccessful()) {  
    System.out.println("Validation passed");  
} else {  
    System.out.println("Validation failed");  
} 

List<SingleValidationMessage> messages = result.getMessages();  
for (SingleValidationMessage next : messages) {  
    System.out.println("Message:");  
    System.out.println(" * Location: " + next.getLocationString());  
    System.out.println(" * Severity: " + next.getSeverity());  
    System.out.println(" * Message : " + next.getMessage());  
}  

// As an operation resource  
OperationOutcome oo = (OperationOutcome) result.toOperationOutcome();  
String results = ctx.newXmlParser().setPrettyPrint(true).encodeResourceToString(oo);  
System.out.println(results);  
```

![Micro-Assessment](./images/micro-assignment-icon.png)

## Micro Assignment #J-14: Validate a Patient Resource
Create a program to read and validate the attached PATIENT.XML file against the FHIR R4 schema. It has only one error. Check that your program reports this error.
