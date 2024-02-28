Conditional Operations
Conditional create and update in HAPI FHIR involve including the `conditional()` method when creating or updating the resource.

There are two methods for representing the condition: by URL (specifying the condition like when you do this in the bundle.entry.request.method of a transaction, or using a structured criterion, actually a Where clause (specifying the search condition using the properties of the elements in the resource).

![Example Icon](./images/example-icon.png)

## Example #J-9: Conditional Operation - Match by Url
```java
MethodOutcome outcome = client.create()  
         .resource(newPatient)  
         .conditionalByUrl("Patient?identifier=http://testpatient.id/mrn%7C199999999")  
         .execute();  
```

## Example #J-10: Conditional Operation - Match by Criteria
```java
MethodOutcome outcome = client.create()  
         .resource(newPatient)  
         .conditional()  
         .where(Patient.IDENTIFIER.exactly()
         .systemAndIdentifier("http://testpatient.id/mrn", "19999999")))  
         .execute();  
```
Remember to process the error condition and outcome (you should always process both anyway!) because three different conditions may occur:

- **Created**: The resource was created, so outcome.getCreated() will return non-null and you can call outcome.getId() to retrieve the id for the new resource (if needed).
- **Found**: The resource was found, so outcome.getCreated() will return null.
- **Precondition Failed**: There was an error condition because more than one instance of the resource existed (Error 412).

## Example #J-11: Conditional Create - Controlled Outcome
```java
try{ 
    MethodOutcome outcome = client.create()  
    .resource(newPatient)  
    .conditionalByUrl("Patient?identifier=http://testpatient.id/mrn%7C99999999")  
    .execute();  
         if (outcome.getCreated() != null)  
         {  
            IIdType id = outcome.getId();  
            {  
               System.out.println("Created patient, got ID: " + id);  
            }  
         }   
       else  
         { 
             System.out.println  
             ("The patient already exists:" + outcome.getId());  
         }  
      }  
catch(Exception ex)  
      {  
            System.out.println(  
            "Error Creating the Resource:" + ex.toString());  
        }        
```

![Micro-Assessment](./images/micro-assignment-icon.png)

## Micro Assignment #J-10: Use conditional create for a patient resource instance
Create a patient resource if it doesn't exist, searching by identifier: Value (you need to create a new one) system=http://central.patient.id/ident.
